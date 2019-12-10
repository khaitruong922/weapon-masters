using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowData : MonoBehaviour
{
   public TextMeshProUGUI data;
   public string dataType="Level";
   private void Start()
   {
       switch(dataType)
       {
           case "Level":
           {
               data.text="Level: " + PersistentData.Instance.level.ToString();
               break;
           }
           case "Name":
           {
                data.text="Name: "+ PersistentData.Instance.playerName;
                break;
           }
       }
   }
}
