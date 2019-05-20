using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    #region Variables
    // criando variaveis que chame componentes "GameObject" e "Tranform
    public GameObject projPrefab;    // projetil
    public Transform spawnPoint;    // ponto onde saira o projetil na torre
    #endregion

	private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projPrefab, spawnPoint.position, projPrefab.transform.rotation);
        }
    }
}
