using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region Variables

    // criando variaveis que chame componentes "GameObject" e "Tranform
    public GameObject projPrefab;    // projetil

    public Transform spawnPoint;    // ponto onde saira o projetil na torre

    #endregion Variables

    private void Update()
    {
        // input para acionar o projetil na cena
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projPrefab, spawnPoint.position, projPrefab.transform.rotation);
        }
    }
}