using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private GameObject target;
    private S_X_Bot playerDieScript;

    //Llamamos al player para saber donde esta
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerDieScript = target.GetComponent<S_X_Bot>();
    }
    //Comprobamos la colision i mandamos el mensaje de que el player ha muerto
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            playerDieScript.Die();
        }
    }
}
