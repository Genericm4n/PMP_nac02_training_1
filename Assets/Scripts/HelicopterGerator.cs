using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterGerator : MonoBehaviour {

    // declarando variavel publica com o prefab do  helicoptero
    public GameObject heliPrefab;
    // variavel float com o delay para gerar um novo helicoptero
    public float delay = 2.0f;

	void Start ()
    {
        InvokeRepeating("GerateHelicopter", delay, delay);
	}
	
	void GerateHelicopter()
    {
        GameObject helicopter = Instantiate(heliPrefab);

        int randomNum = Random.Range(0, 4);

        helicopter.transform.eulerAngles = Vector3.up * randomNum * 90;
	}
}
