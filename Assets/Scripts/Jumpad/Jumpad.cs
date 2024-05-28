using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{

    public GameObject player;
    S_X_Bot s_X_Bot;
    [SerializeField, Range(10, 15)]private float reboundForce;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            s_X_Bot.Rebotar(transform.up, reboundForce);
        }
    }
}
