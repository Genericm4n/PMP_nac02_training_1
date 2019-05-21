using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    #region Variables

    // chamando o script do Follow Target
    private FollowTarget followTarget;

    // em vez de chamar o componente Rigibody no Start atraves de uma variavel, usar uma variavel publica colocando o prefab com o Rigibody
    public Rigidbody rb;

    // game object publico com o prefab da explosao
    public GameObject explosionPrefab;

    // variavel float com o valor do torque (movimento de rotacao) a ser adicionado no helicoptero ao cair
    public float forceTorque = 200.0f;

    #endregion Variables

    private void Start()
    {
        // chamando o componente do Follow Target a partir da variavel criada
        followTarget = GetComponent<FollowTarget>();
    }

    private void OnTriggerEnter(Collider col)
    {
        // detectando colisao com o waypoint
        if (col.CompareTag("Waypoint"))
        {
            WaypointController waypoint = col.GetComponent<WaypointController>();
            WaypointController waypointP = waypoint.waypointP;
            followTarget.target = waypointP.transform;
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        // detectando colisao com o projetil
        if (c.gameObject.CompareTag("Projectile"))
        {
            // destruir o projetil apos colisao
            Destroy(c.gameObject);

            // controle de como a fisica age sobre o rigibody
            rb.isKinematic = false;
            // controle de como a gravidade age sobre o rigibody
            rb.useGravity = true;

            // instanciando o prefab da explosao na posicao do helicoptero, com a sua devida rotacao
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            rb.AddTorque(Vector3.up * Random.Range(0, 5));
            rb.AddTorque(Vector3.right * forceTorque);
        }
        // detectando colisao com o chao
        else if (c.gameObject.CompareTag("Floor"))
        {
            // destruir o helicoptero
            Destroy(gameObject);

            // instanciar o prefab da explosao na posicao do helicoptero, com sua devida rotacao
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        }
    }
}