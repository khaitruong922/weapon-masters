using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float totalTime = 120f;
    private float remainingTime;
    private int minutes;
    private int seconds; 
    private TextMeshProUGUI timer;
    private void Start() {
        remainingTime = totalTime;
        timer = GetComponent<TextMeshProUGUI>();
}
    private void Update() {
        remainingTime-=Time.deltaTime;
        minutes = (int)(remainingTime/60);
        seconds = (int)(remainingTime%60);
        string timeString = string.Format("{0:00}:{1:00}",minutes,seconds);
        timer.SetText(timeString);
        if(remainingTime<0){
            remainingTime=0;
            GameHandler.Instance.Defeat();       
        }
    }   
}
