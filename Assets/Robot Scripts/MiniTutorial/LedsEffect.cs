using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;
using Unity.Robotics.UrdfImporter.Control;
using UnityEditor.ShaderGraph;
using System;
using UnityEngine.InputSystem.Composites;
using UnityEngine.Rendering;

public class LedsEffect : MonoBehaviour
{

    public InputActionReference bButton;

    public InputActionReference aButton;

    public InputActionReference mainTrigger;

    public InputActionReference secondaryTrigger;
    public InputActionProperty moveAction;

    public InputActionProperty moveAround;
    public InputActionReference yButton;
    public InputActionReference pumpTrigger;



    [SerializeField] public Button AButton;
    [SerializeField] public Button BButton;
    [SerializeField] public Button MainButton;
    [SerializeField] public Button SecondaryButton;


    [SerializeField] public Button LeftButton;
    [SerializeField] public Button RightButton;

    [SerializeField] public Button reset;
    [SerializeField] public Button pumpOn;
    [SerializeField] public Button pumpOff;


    [SerializeField] public Button Up;
    [SerializeField] public Button Down;
    [SerializeField] public Button Left;
    [SerializeField] public Button Right;
    private Color theColor = Color.green;
    private Color AwakeColor = Color.red;
    private ColorBlock col;

    private  int pressedCounts = 0;




    void OnEnable()
    {
        if (moveAction.action != null)
            moveAction.action.Enable();

        if (moveAround.action != null)
            moveAround.action.Enable();

        if (bButton.action != null)
            bButton.action.Enable();

        if (aButton.action != null)
            aButton.action.Enable();



        if (mainTrigger.action != null)
            mainTrigger.action.Enable();

        if (secondaryTrigger.action != null)
            secondaryTrigger.action.Enable();

        if (yButton.action != null)
            yButton.action.Enable();


        if (pumpTrigger.action != null)
            pumpTrigger.action.Enable();

    }

    void OnDisable()
    {
        if (bButton.action != null)
            bButton.action.Disable();
      
        if (aButton.action != null)
            aButton.action.Disable();
      
        if (mainTrigger.action != null)
            mainTrigger.action.Disable();

        if (secondaryTrigger.action != null)
            secondaryTrigger.action.Disable();

        if (yButton.action != null)
            yButton.action.Disable();


        if (pumpTrigger.action != null)
            pumpTrigger.action.Disable();

            
        if (moveAround.action != null)
            moveAround.action.Disable();
    }
    void Awake()
    {

        initColor( AwakeColor);


    }


    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        float horizontal = input.x;

           Vector2 input2 = moveAround.action.ReadValue<Vector2>();

        float hor = input2.x;
        float ver = input2.y;

        {
            if (horizontal > 0.5f)
            {
                setColorThumbstick(moveAction, LeftButton, col, theColor);
            }
            if (horizontal < -0.5f)
            {
                setColorThumbstick(moveAction, RightButton, col, theColor);
            }
                if (hor > 0.5f)
            {
                setColorThumbstick( moveAround, Right, col, theColor);
            }
            if (hor < -0.5f)
            {
                setColorThumbstick( moveAround,Left , col, theColor);
            }
                if (ver> 0.5f)
            {
                setColorThumbstick( moveAround, Up, col, theColor);
            }
            if (ver < -0.5f)
            {
                setColorThumbstick( moveAround, Down, col, theColor);
            }

        }


        setColor(aButton, AButton, col, theColor);
        setColor(bButton, BButton, col, theColor);

        setColor(mainTrigger, MainButton, col, theColor);
        setColor(secondaryTrigger, SecondaryButton, col, theColor);


        setColor(yButton, reset, col, theColor);
        setPumpColor(pumpTrigger,pumpOn,pumpOff,col,theColor );
        //   if (aButton.action.IsPressed())
        //     {
        //         theColor.normalColor = Color.green;
        //               AButton.colors = theColor;



        //     }
        //       if (bButton.action.IsPressed())
        //     {
        //         theColor.normalColor = Color.green;
        //               AButton.colors = theColor;



        //     }



    }


    void initColor( Color co)
    {
       Button button = GetComponent<Button>();
       ColorBlock color = GetComponent<Button>().colors;
        color.normalColor = co;
        button.colors = color;
    }
    void setColorProperties(Button button,ColorBlock color, Color co)
    {
            color.normalColor = co;
            color.highlightedColor = Color.white;
            color.pressedColor = Color.white;
            color.disabledColor = Color.white;
            color.colorMultiplier = 5.0f;
            button.colors = color;
    }
    void setColor(InputActionReference buttonController, Button button, ColorBlock color, Color co)
    {
        if (buttonController.action.IsPressed())
        {
        setColorProperties(button, color, co);

        }
    }


    void setColorThumbstick(InputActionProperty buttonController, Button button, ColorBlock color, Color co)
    {
        if (buttonController.action.IsPressed())
        {
          setColorProperties(button, color, co);

        }
    }

    void setPumpColor(InputActionReference buttonController, Button button1,Button button2, ColorBlock color, Color co)
    {

        
            if (buttonController.action.WasPressedThisFrame())
            {
                pressedCounts++;
                Debug.Log(pressedCounts);

            }
       
            if(pressedCounts == 1)
        {
            
             setColorProperties(button1, color, co);
}

        if (pressedCounts == 2)
        {
              setColorProperties(button2, color, co);


        }
        else
        {
            
        if (pressedCounts >= 2)
        {
         setColorProperties(button1, color, co);
             setColorProperties(button2, color, co);
  
  pressedCounts = 0;



        }

  
        
       
        
    }
}



}




