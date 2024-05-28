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
        //agarramos para saber quien es el player
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Llamamos al rebote del comando de SXBot
        if (collision.gameObject == player)
        {
            s_X_Bot.Rebotar(transform.up, reboundForce);
        }
    }
}
