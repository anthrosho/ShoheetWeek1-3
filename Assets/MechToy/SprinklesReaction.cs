using UnityEngine;

public class SprinklesReaction : MonoBehaviour
{
    // booleans for the objects
    public bool present;
    public bool stick;
    public bool feed;

    // Positions

    public Vector3 presentPosition;
    public Vector3 stickPosition;  
    public Vector3 feedPosition;
    
    private Transform currentImage;
    Vector3 startPosition;
    public Vector3 imageVisiblePosition = Vector3.zero;
    public Vector3 imageHiddenPosition = new Vector3(0, -10000f, 0);



    // Default

    public Vector3 defaultPosition;

    // Lerp movement 
    public float lerpSpeed = 5f;

    // Image transforms 

    public Transform neutralSprinkles;
    public Transform happySprinkles;
    public Transform grumpySprinkles;

    //Emotions
    public bool isNeutral = true;
    public bool isHappy;
    public bool isGrumpy;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultPosition = transform.localPosition;

        currentImage = neutralSprinkles;
        currentImage.localPosition = imageVisiblePosition;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime * lerpSpeed);
        UpdateImagePositions();
    }

    // EMOTIONS

    public void HappySprinkles()
    {
        isHappy = true;
        isGrumpy = false;
        isNeutral = false;
    }
    public void NeutralSprinkles()
    {
        isHappy = false;
        isGrumpy = false;
        isNeutral = true;
    }

    public void GrumpySprinkles()
    {
        isHappy = false;
        isGrumpy = true;
        isNeutral = false;
    }
    void UpdateImagePositions()
    {
        Vector3 SprinklesNeutral = imageHiddenPosition;
        Vector3 SprinklesHappy = imageHiddenPosition;
        Vector3 SprinklesGrumpy = imageHiddenPosition;

        if (isNeutral) SprinklesNeutral = imageVisiblePosition;
        if (isHappy) SprinklesHappy = imageVisiblePosition;
        if (isGrumpy) SprinklesGrumpy = imageVisiblePosition;

        neutralSprinkles.localPosition = SprinklesNeutral;
        happySprinkles.localPosition = SprinklesHappy;
        grumpySprinkles.localPosition = SprinklesGrumpy;

    }

}


