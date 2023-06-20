using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    [SerializeField]
    string loadNewGameSceneName;

    public void LoadNewGameScene()
    {
        SceneManager.LoadScene(loadNewGameSceneName);
    }
}
