using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunslingerCooldown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string skillKey= "Q";
    private GunslingerAbility player;
    private Image cooldownFill;
    public TextMeshProUGUI number;
    public TextMeshProUGUI key;
    // Update is called once per frame
    void Start(){
        player = FindObjectOfType<GunslingerAbility>();
        cooldownFill = GetComponent<Image>();
        key.text = skillKey;
    }
    void Update()
    {
        switch(skillKey)
            {
            case "Q":
            {
                cooldownFill.fillAmount = player.qCooldownLeft/player.qCooldown;
                if(player.qCooldownLeft>0)
                { // convert float to int
                    number.text =((int) player.qCooldownLeft+1).ToString();
                }
                else number.text = "";
                break;
            }
            case "E":
            {
                cooldownFill.fillAmount = player.eCooldownLeft/player.eCooldown;
                if(player.eCooldownLeft>0)
                {
                    number.text = ((int)player.eCooldownLeft+1).ToString();
                }
                else number.text = "";
                break;

            }
            case "R":
            {
                cooldownFill.fillAmount = player.rCooldownLeft/player.rCooldown;
                if(player.rCooldownLeft>0)
                {
                    number.text = ((int)player.rCooldownLeft+1).ToString();
                }
                else number.text = "";
                break;

            }
    }

}
}