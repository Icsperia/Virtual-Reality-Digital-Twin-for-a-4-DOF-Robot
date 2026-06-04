using UnityEngine;
using System;
using System.IO;
using TMPro;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
public class JSONReader: MonoBehaviour
{
        string jsonPath = "Assets/Robot Scripts/Camera Scripts/controlls.json";
        [SerializeField] private TextMeshProUGUI txt;


void Start()
    {
        string jsonString = File.ReadAllText(jsonPath);

        string allDescriptions = " ";
        Controllers controllersInJson = JsonUtility.FromJson<Controllers>(jsonString);
       if(controllersInJson != null)
        {
        
       
        if(txt != null)
        {

            if (controllersInJson != null)
            {

                 if (controllersInJson.RightController != null)
            {
                allDescriptions += "RIGHT CONTROLLER FOR ROBOT MOVEMENT:\n";
                foreach (var controller in controllersInJson.RightController)
                {
                    
                    allDescriptions += $"Thumbstick{controller.ThumbStick[0]} : {controller.ThumbStick[1]} \n";
                      allDescriptions += $"Thumbstick{controller.ThumbStick[2]} : {controller.ThumbStick[3]}\n";
                    
                    allDescriptions += $"Buton {controller.button1[0]} : {controller.button1[1]}\n";
                    allDescriptions += $"Buton {controller.button2[0]} : {controller.button2[1]}\n";
                    allDescriptions += $"FrontTrigger {controller.FrontTrigger[0]} : {controller.FrontTrigger[1]}\n";
                    allDescriptions += $"SideTrigger {controller.SideTrigger[0]} : {controller.SideTrigger[1]}\n";
                }
            }
        }
            if (controllersInJson.LeftController != null)
            {
                allDescriptions += "\nLEFT CONTROLLER PLAYER AND ROBOT CONTROLS:\n";
                foreach (var controller in controllersInJson.LeftController)
                {
                    allDescriptions += $"Thumbstick {controller.ThumbStick[0]}\n";
                    allDescriptions += $"Buton {controller.button1[0]} : {controller.button1[1]}\n";
                    allDescriptions += $"Buton {controller.button2[0]} : {controller.button2[1]}\n";
                    allDescriptions += $"FrontTrigger {controller.FrontTrigger[0]} : {controller.FrontTrigger[1]}\n";
                    allDescriptions += $"SideTrigger {controller.SideTrigger[0]} : {controller.SideTrigger[1]}\n";
                }
            }

        txt.text =allDescriptions;
            }
       
        } else
        {
            Debug.Log("Nu este ok");
        }


        }
 
  

[System.Serializable]
public class Controller
{
    public string [] ThumbStick;
    public string [] button1;
    public string [] button2;
    public string [] FrontTrigger;
    public string [] SideTrigger;

}
[System.Serializable]
public class Controllers
{
    public Controller [] RightController;
    public Controller [] LeftController;

}
}
