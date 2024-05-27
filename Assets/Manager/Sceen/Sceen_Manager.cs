using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceen_Manager : MonoBehaviour
{
    public static Sceen_Manager _SCEEN_MANAGER;

    private void Awake()
    {
        if (_SCEEN_MANAGER != null && _SCEEN_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _SCEEN_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
