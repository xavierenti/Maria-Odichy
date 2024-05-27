using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER;


    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _GAME_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LevelPassed()
    {
        Sceen_Manager._SCEEN_MANAGER.ExitGame();
    }

    public void PlayerHasDied()
    {
        Sceen_Manager._SCEEN_MANAGER.ReloadLevel();
        
    }
}
