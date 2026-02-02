using UnityEngine;
using UnityEngine.InputSystem;

public class SprinklesReaction : MonoBehaviour
{
    // booleans for the objects
    public bool present;
    public bool stick;
    public bool feed;

 

    private Transform currentImage;
    Vector3 startPosition;
    public Vector3 imageVisiblePosition = Vector3.zero;
    public Vector3 imageHiddenPosition = new Vector3(0, -10000f, 0);

    // Default
    public Vector3 defaultPosition;

    // Lerp movement 
    public float lerpSpeed = 5f;

    // Default Image transforms 
    public Transform neutralSprinkles;
    public Transform happySprinkles;
    public Transform grumpySprinkles;

    // Mouse Hover Image transforms
    public Transform SprinkleHoldPresent;
    public Transform SprinkleEating;
    public Transform SprinkleLookStick;

    // Manual interactive object positions and sizes
    public Vector3 presentObjectPosition;
    public Vector2 presentObjectSize;
    public Vector3 stickObjectPosition;
    public Vector2 stickObjectSize;
    public Vector3 feedObjectPosition;
    public Vector2 feedObjectSize;

    // Emotions
    public bool isNeutral = true;
    public bool isHappy;
    public bool isGrumpy;

    // Camera reference
    public Camera gameCamera;

    void Start()
    {

        defaultPosition = transform.localPosition;

        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;


        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;
        
        NeutralSprinkles();
        
    }

   



    void Update()
    {
        // Smoothly move the sprinkle parent to its default position
        transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime * lerpSpeed);

        CheckMouseOverObjects();
        UpdateImagePositions();
    }

    void CheckMouseOverObjects()
    {
        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = gameCamera.ScreenToWorldPoint(currentMousePosition);
        worldMousePosition.z = 0f;

        // Reset hover flags
        present = false;
        stick = false;
        feed = false;

        // Hide all hover images by default
        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;

        // --- Present Hover ---
        if (worldMousePosition.x >= presentObjectPosition.x - presentObjectSize.x / 2f &&
            worldMousePosition.x <= presentObjectPosition.x + presentObjectSize.x / 2f &&
            worldMousePosition.y >= presentObjectPosition.y - presentObjectSize.y / 2f &&
            worldMousePosition.y <= presentObjectPosition.y + presentObjectSize.y / 2f)
        {
            present = true;
            stick = false;
            feed = false;

            SprinkleHoldPresent.localPosition = imageVisiblePosition;

            // Hide default images while hovering
            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;

            HappySprinkles();
            return;
        }

        // --- Stick Hover ---
        if (worldMousePosition.x >= stickObjectPosition.x - stickObjectSize.x / 2f &&
            worldMousePosition.x <= stickObjectPosition.x + stickObjectSize.x / 2f &&
            worldMousePosition.y >= stickObjectPosition.y - stickObjectSize.y / 2f &&
            worldMousePosition.y <= stickObjectPosition.y + stickObjectSize.y / 2f)
        {
            present = false;
            stick = true;
            feed = false;

            SprinkleLookStick.localPosition = imageVisiblePosition;

            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;

            GrumpySprinkles();
            return;
        }

        // --- Feed Hover ---
        if (worldMousePosition.x >= feedObjectPosition.x - feedObjectSize.x / 2f &&
            worldMousePosition.x <= feedObjectPosition.x + feedObjectSize.x / 2f &&
            worldMousePosition.y >= feedObjectPosition.y - feedObjectSize.y / 2f &&
            worldMousePosition.y <= feedObjectPosition.y + feedObjectSize.y / 2f)
        {
            present = false;
            stick = false;
            feed = true;

            SprinkleEating.localPosition = imageVisiblePosition;

            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;

            NeutralSprinkles();
            return;
        }

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

        if (isNeutral)
        {
            SprinklesNeutral = imageVisiblePosition;
        }

        if (isHappy)
        {
            SprinklesHappy = imageVisiblePosition;
        }

        if (isGrumpy)
        {
            SprinklesGrumpy = imageVisiblePosition;
        }

        neutralSprinkles.localPosition = SprinklesNeutral;
        happySprinkles.localPosition = SprinklesHappy;
        grumpySprinkles.localPosition = SprinklesGrumpy;
    }
}
