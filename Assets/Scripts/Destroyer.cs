using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // definicao de variavel
    public float delay = 0.5f;  // tempo necessario para o prefab da explosao ser destruido

    private void Start()
    {
        // destruir o prefab da explosao
        Destroy(gameObject, delay);
    }
}