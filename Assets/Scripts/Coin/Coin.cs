using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 1;
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            // A�adimos punto al LevelManage
            Level_Manager._LEVEL_MANAGER.GainCoin();
            Destroy(this.gameObject);
        }
    }
}
