using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float horizontalSpeed = 0.02f;
    public float verticalSpeed = 0.02f;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;
    public Camera gameCamera;


    private float timePassed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moverXPos = transform.position;
        Vector3 moverYPos = transform.position;
        moverXPos.x += horizontalSpeed * Time.deltaTime;
        moverYPos.y -= verticalSpeed * Time.deltaTime;
        

        transform.position = moverXPos;
        transform.position = moverYPos;


        Vector3 screenTransformPosition = gameCamera.WorldToScreenPoint(transform.position);
        xMax = Screen.width;
        yMax = Screen.height;

        //set xMin to wherever is too far to the left for the player to see
        xMin = 0;
        yMin = 0;




        if (xMax < screenTransformPosition.x)
        {
            horizontalSpeed *= -1;
        }

        if (xMin > screenTransformPosition.x)
        {
            horizontalSpeed *= +1;
            
        }

        if (yMax < screenTransformPosition.x)
        {
            verticalSpeed *= -1;
        }

        if (yMin > screenTransformPosition.x)
        {
            verticalSpeed *= -1;
        }

    }
}
