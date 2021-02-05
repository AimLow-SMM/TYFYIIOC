using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    public void NextSceneOnClick()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
