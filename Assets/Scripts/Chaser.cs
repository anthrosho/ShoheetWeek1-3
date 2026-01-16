using UnityEngine;
using UnityEngine.InputSystem;

public class Chaser : MonoBehaviour
{
    public Camera gameCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        transform.position = currentMousePosition;

        Vector3 convertedMousePosition = gameCamera.ScreenToWorldPoint(currentMousePosition);
        convertedMousePosition.z = 0;
        transform.position = convertedMousePosition;



    }
}
