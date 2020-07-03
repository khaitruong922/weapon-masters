using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HideImageAwake : MonoBehaviour
{
    Image image;
    void Awake(){
        image=GetComponent<Image>();
        image.enabled = false;
    }
}