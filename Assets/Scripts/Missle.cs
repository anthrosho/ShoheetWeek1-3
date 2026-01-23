using UnityEngine;

public class Missle : MonoBehaviour
{
    public Transform startValue;
    public Transform endvalue;
    public float progress = 0;
    public Vector3 output;
    public float duration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        progress += Time.deltaTime / duration; 
        output = Vector3.Lerp(startValue.position, endvalue.position, progress);
        transform.position = output;
    }
}
