using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager _LEVEL_MANAGER;

    [Header("Stars count")]
    [SerializeField] private int MaxCoins = 0;

    private int actualCoins = 0;

    private void Awake()
    {
        _LEVEL_MANAGER = this;
    }

    private void Update()
    {
        if (actualCoins >= MaxCoins)
        {
            GameManager._GAME_MANAGER.LevelPassed();
        }
    }

    public void AppendStar() => actualCoins++;
}
