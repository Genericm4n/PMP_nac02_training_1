using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    #region Variables

    [Header("Target")]
    public Transform target;        // target baseado em algum objeto na cena

    public string targetTag;        // string que recebe uma tag
    public bool searchProximity;    // permite que o target mais proximo seja seguido

    [Header("Moviment")]
    public float velMove = 3.0f;    // velocidade de movimentacao do objeto que seguira o target

    [Header("Rotation")]
    public float velRot = 3.0f;     // velocidade de rotacao do objeto que seguira o target

    public bool lookAt = false;     // faz o objeto rotacionar no sentido do target

    #endregion Variables

    private void Update()
    {
        SearchTarget();             // funcao responsavel por encontrar o target a ser seguido
        Move();                     // funcao responsavel pela movimentacao do objeto que seguira o target
        Rotate();                   // funcao responsavel pela rotacao do objeto que seguira o target
    }

    private void SearchTarget()
    {
        // validacao se ja nao tem um alvo definido e se ha uma tag definida
        if (targetTag == "" || (!searchProximity && target != null))
        {
            // encerrando o metodo
            return;
        }

        // logica de procurar o target atraves de tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        Transform possibleTarget = null;

        // criacao de um loop para fazer a verificacao do Array
        foreach (GameObject checkTarget in targets)
        {
            float checkDist = Vector3.Distance(checkTarget.transform.position, transform.position);

            if (possibleTarget == null || checkDist < Vector3.Distance(possibleTarget.transform.position, transform.position))
            {
                possibleTarget = checkTarget.transform;
            }
        }
        // se for colocado o objeto dentro da variavel publica target
        if (possibleTarget != null)
        {
            target = possibleTarget;
        }
    }

    private void Move()
    {
        // se a variavel "lookAt" estiver marcada, ou, se nao possuimos uma informacao de target, movimentar o objeto em linha reta
        if (lookAt || target == null)
        {
            // movimentacao em linha reta
            transform.Translate(Vector3.forward * velMove * Time.deltaTime);
        }
        // caso contrario, se houver um target
        else if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * velMove * Time.deltaTime, Space.World);
        }
    }

    private void Rotate()
    {
        if (lookAt && target != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(target.position - transform.position),
                Time.deltaTime * velRot);
        }
    }
}