using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MapSelect");
        PlayClickSound();
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
        PlayClickSound();
    }
    public void PlayClickSound(){
        AudioManager.Instance.Play("Click");
    }
}