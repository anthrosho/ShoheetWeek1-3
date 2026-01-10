using Unity.VisualScripting;
using UnityEngine;

public class SquareMove : MonoBehaviour
{

    public float moveSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = newPosition.y + 0.1f;
            transform.position = newPosition;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = newPosition.y - 0.1f;
            transform.position = newPosition;

        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 newPosition = transform.position;
            newPosition.x = newPosition.x + 0.1f;
            transform.position = newPosition;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 newPosition = transform.position;
            newPosition.x = newPosition.x - 0.1f;
            transform.position = newPosition;



        }


    }
}


