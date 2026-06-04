using System;
using System.Data.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ResetPositions : MonoBehaviour
{
    public ArticulationBody verticalArm;
    //public ArticulationBody horizontalArm;
    public ArticulationBody rotativeBase;

    public ArticulationBody upDownSegment;

    public ArticulationBody noozle;

    public InputActionReference joystickButton;

    void Start()
    {

    }

    void OnEnable()
    {
        if (joystickButton != null)
            joystickButton.action.Enable();
    }


    void FixedUpdate()
    {


        if (joystickButton.action.IsPressed())
        {

            resetPosition(verticalArm);
            resetPosition(rotativeBase);
            resetPosition(noozle);
            resetPosition(upDownSegment);

        }

    }

    void resetPosition(ArticulationBody source)
    {
        var drive = source.xDrive;
        drive.target = 0.0f;
        source.xDrive = drive;

    }
}