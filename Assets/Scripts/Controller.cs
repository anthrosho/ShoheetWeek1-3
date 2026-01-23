using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool leftOsJeld = Mouse.current.leftButton.isPressed; 
        if(leftIsHeld)
        {
            Debug.Log("Left mouse is held");
            
        }
        bool leftIsPressed = Mouse.current.leftButton.wasPressedThisFrame;
        if (leftIsPressed)
        {
            Debug.Log("Left mouse is released");
        }
    }
}
