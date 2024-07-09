using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNewGame()
    {
        MainManager.Instance.StartNew();
        SceneManager.LoadScene(1);
    }

    public void StartLoad()
    {
        MainManager.Instance.LoadFiles();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
    // Update is called once per frame

}
