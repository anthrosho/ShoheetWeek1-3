using UnityEngine;

public class Pulser : MonoBehaviour

{
    public AnimationCurve curve;
    public float duration;
    public float output;
    private float progress = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime / duration;
        output = curve.Evaluate(progress);


        transform.localScale = Vector3.one *output;

        if(progress > 1f)
        {
            progress = 0f;

        }

    }
}

