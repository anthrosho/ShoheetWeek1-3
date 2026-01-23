using UnityEngine;
using UnityEngine.InputSystem;

public class RotateMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        bool leftArrowHeld = Keyboard.current.leftArrowKey.isPressed;
        bool rightArrowHeld = Keyboard.current.rightArrowKey.isPressed;
        bool upArrowHeld = Keyboard.current.upArrowKey.isPressed;
        bool downArrowHeld = Keyboard.current.downArrowKey.isPressed;
        if (leftArrowHeld)
        {
            transform.eulerAngles += transform.forward * rotateSpeed * Time.deltaTime;
        }
        if (rightArrowHeld)
        {
            transform.eulerAngles -= transform.forward * rotateSpeed * Time.deltaTime;
        }
        if (upArrowHeld)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        if (downArrowHeld)
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;
        }
    }
}
