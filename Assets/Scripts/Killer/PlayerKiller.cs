using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private GameObject target;
    private S_X_Bot playerDieScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerDieScript = target.GetComponent<S_X_Bot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            playerDieScript.Die();
        }
    }
}
