
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    public float speed = 0.02f;
    public float xMax;
    public float xMin;
    public Camera gameCamera;


    public Vector3 hidePosition;
    public float hideDistance;

    public float waitDuration;

    private float timePassed = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moverXPos = transform.position;
        moverXPos.x += speed * Time.deltaTime;

        transform.position = moverXPos;

        //Screen.width;
        //gameCamera.WorldToScreenPoint(//somerandomvector);

        //set xMax to wherever is too far to the right for the player to see
        Vector3 screenTransformPosition = gameCamera.WorldToScreenPoint(transform.position);
        xMax = Screen.width;

        //set xMin to wherever is too far to the left for the player to see
        xMin = 0;


        if (xMax < screenTransformPosition.x)
        {
            speed *= -1;
        }

        if (xMin > screenTransformPosition.x)
        {
            speed *= -1;
        }


    }
}
