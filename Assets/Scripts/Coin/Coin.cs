using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 1;
    private GameObject target;

    void Update()
    {
        //Cogemos al player de la partida
        target = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            // Añadimos punto al LevelManage
            Level_Manager._LEVEL_MANAGER.GainCoin();
            Destroy(this.gameObject);
        }
    }
}
