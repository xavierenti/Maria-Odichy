using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputManager : MonoBehaviour
{
    private PlayerImput playerImput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerImput = PlayerImput._PLAYER_IMPUT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
