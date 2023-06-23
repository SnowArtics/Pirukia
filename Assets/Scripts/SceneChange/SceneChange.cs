using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    string loadNewGameSceneName;

    [SerializeField]
    string loadTitleSceneName;

    public void LoadNewGameScene()
    {
        SceneManager.LoadScene(loadNewGameSceneName);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(loadTitleSceneName);
    }
}
