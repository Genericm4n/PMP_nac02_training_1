using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointController : MonoBehaviour {

    #region Variables
    [Header("Waypoints")]
    [Tooltip("Lista of all Waypoints")]

    private WaypointController[] waypoints;     // array com os Waypoints presentes na cena, deteccao dos Scripts
    private int indexActual = -1;               // index atual corresponde a identidade do Waypoint selecionado

    internal WaypointController waypointA;      // waypoint anterior ao atual
    internal WaypointController waypointP;      // waypoint posterior ao atual
    #endregion
    private void Start ()
    {
        // funcao que carrega outras funcoes que serao responsaveis por identificar o waypoint anterior e posterior
        LoadWaypointSystem();
	}

    private void LoadWaypointSystem()
    {
        FreshWaypointActual();          // responsavel por pegar o id do waypoint atual
        FreshWaypoints();               // responsavel por ordernar as os waypoints dentro de uma array
        LinkWaypoint();                 // funcionamento para decteccao do waypoint anterior e posterior
    }

    private void FreshWaypointActual()
    {
        indexActual = GetIdWaypoint(gameObject.name);     // funcao que transformara todos os nomes dos waypoints em um id
    }

    private int GetIdWaypoint(string name)
    {
        name = name.Replace("waypoint (", "");     
        name = name.Replace(")", "");

        int id = -1;

        try
        {
            // transformar uma string em uma int, e retirara -1 de seu valor, tornando o id "0" inviavel
            id = int.Parse(name) - 1;
        }
        catch (Exception)
        {
            Debug.LogError("Ouch! An error ocurred. Make sure Waypoint has a correct default name, 'waypoint (number)'.");
        }
        return id;
    }

    private void FreshWaypoints()
    {
        // componente que encontrara os objectos com o "WaypointController" associado na cena
        waypoints = FindObjectsOfType<WaypointController>();
        // organizar em order crescente os waypoints dentro do Array
        waypoints = waypoints.OrderBy(object0 => GetIdWaypoint(object0.name)).ToArray();
    }

    private void LinkWaypoint()
    {
        int indexA = indexActual - 1;       // propriedade do waypoint anterior
        int indexP = indexActual + 1;       // propriedade do waypoint posterior

        // funcoes para a definicao do waypoint anterior e posterior, e seus index
        DefineWaypoint(ref waypointA, indexA);      
        DefineWaypoint(ref waypointP, indexP);
    }

    private void DefineWaypoint(ref WaypointController waypoint, int index)
    {
        // se o index for maior que 0, ele será igual ao seu valor subtraido de 1
        if (index < 0)
        {
            // Length = comprimento do Array
            index = waypoints.Length - 1;
        }
        // caso contrario, obrigatoriamente sera igual a 0
        else if (index == waypoints.Length)
        {
            index = 0;
        }

        // determinacao do index do waypoint dentro do Array
        waypoint = waypoints[index];
    }

    private void OnDrawGizmos()
    {
        // necessario carregar o sistema de waypoint
        LoadWaypointSystem();

        // waypoint tem que ser igual a "null" para que assim seja fechado o ciclo
        if (waypointP != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, waypointP.transform.position);
        }
    }
}
