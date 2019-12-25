using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowData : MonoBehaviour
{
   public TextMeshProUGUI data;
   public string preText;
   public string dataType="Level";
   private void Start()
   {
       switch(dataType)
       {
           case "Level":
           {
               data.text=preText + PersistentData.Instance.level.ToString();
               break;
           }
           case "Name":
           {
                data.text=preText+ PersistentData.Instance.playerName;
                break;
           }
       }
   }
}
