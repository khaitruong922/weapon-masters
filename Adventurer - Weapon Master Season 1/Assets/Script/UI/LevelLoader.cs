﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public TextMeshProUGUI progressText;
    public void LoadLevel ()
    {
        StartCoroutine("LoadAsynchronously");
        
    }
    public void MapSelect(int mapNumber){
        PlayerPrefs.SetInt("MapNumber",mapNumber);
    }
    public void CharSelect(int charNumber){
        PlayerPrefs.SetInt("CharNumber",charNumber);
    }

    IEnumerator LoadAsynchronously ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("MapNumber")+1);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
    public void Back(){
        SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
    }
}