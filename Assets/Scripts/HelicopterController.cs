using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour {

    #region Variables
    // chamando o script do Follow Target
    FollowTarget followTarget;

    public Rigidbody rb;
    public GameObject explosionPrefab;

    public float forceTorque = 200.0f;
    #endregion

    private void Start ()
    {
        // chamando o componente do Follow Target a partir da variavel criada
        followTarget = GetComponent<FollowTarget>();
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Waypoint"))
        {
            WaypointController waypoint = col.GetComponent<WaypointController>();
            WaypointController waypointP = waypoint.waypointP;
            followTarget.target = waypointP.transform;
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Projectile"))
        {
            Destroy(c.gameObject);

            rb.isKinematic = false;
            rb.useGravity = true;

            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            rb.AddTorque(Vector3.up * Random.Range(0,5));
            rb.AddTorque(Vector3.right * forceTorque);
        }
        else if (c.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);

            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        }
    }
}