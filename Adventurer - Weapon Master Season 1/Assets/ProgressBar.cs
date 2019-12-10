using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    private Cart cart;
    private Image bar;
    private TextMeshProUGUI progress;
    void Start()
    {
        cart = FindObjectOfType<Cart>();
        bar = GetComponent<Image>();
        progress = transform.Find("ProgressText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = cart.currentProgress;
        progress.text = ((int)(bar.fillAmount*100)).ToString() + " %";
    }
}
