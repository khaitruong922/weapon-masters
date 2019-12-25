using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    void Awake(){
        audioMixer.SetFloat("Volume",PlayerPrefs.GetFloat("Volume",0));
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("Volume",Mathf.Log10(volume)*20);
        PlayerPrefs.Save();
    }
    public void ResetPlayer(){
        PersistentData.Instance.level = 1;
        PersistentData.Instance.Save();
        PersistentData.Instance.Load();
    }
}
