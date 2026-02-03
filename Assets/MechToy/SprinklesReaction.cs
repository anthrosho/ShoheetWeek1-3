using UnityEngine;
using UnityEngine.InputSystem;

public class SprinklesReaction : MonoBehaviour
{
    public bool present;
    public bool stick;
    public bool feed;

    public Vector3 defaultPosition;
    public Vector3 imageVisiblePosition = Vector3.zero;
    public Vector3 imageHiddenPosition = new Vector3(0, -10000f, 0);

    public Transform neutralSprinkles;
    public Transform happySprinkles;
    public Transform grumpySprinkles;

    public Transform SprinkleHoldPresent;
    public Transform SprinkleEating;
    public Transform SprinkleLookStick;

    public Vector3 presentObjectPosition;
    public Vector2 presentObjectSize;
    public Vector3 stickObjectPosition;
    public Vector2 stickObjectSize;
    public Vector3 feedObjectPosition;
    public Vector2 feedObjectSize;

    public Camera gameCamera;

    public float lerpSpeed = 5f;

    public bool isHovering = false;

    public int currentEmotion = 0;

    void Start()
    {
        defaultPosition = transform.localPosition;

        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;

        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;

        currentEmotion = 0;
        UpdateImagePositions();
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime * lerpSpeed);

        CheckMouseOverObjects();
        UpdateImagePositions();
    }

    void CheckMouseOverObjects()
    {
        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = gameCamera.ScreenToWorldPoint(currentMousePosition);
        worldMousePosition.z = 0f;

        present = false;
        stick = false;
        feed = false;
        isHovering = false;

        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;

        if (worldMousePosition.x >= presentObjectPosition.x - presentObjectSize.x / 2f &&
            worldMousePosition.x <= presentObjectPosition.x + presentObjectSize.x / 2f &&
            worldMousePosition.y >= presentObjectPosition.y - presentObjectSize.y / 2f &&
            worldMousePosition.y <= presentObjectPosition.y + presentObjectSize.y / 2f)
        {
            present = true;
            isHovering = true;
            SprinkleHoldPresent.localPosition = imageVisiblePosition;
            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;
            return;
        }

        if (worldMousePosition.x >= stickObjectPosition.x - stickObjectSize.x / 2f &&
            worldMousePosition.x <= stickObjectPosition.x + stickObjectSize.x / 2f &&
            worldMousePosition.y >= stickObjectPosition.y - stickObjectSize.y / 2f &&
            worldMousePosition.y <= stickObjectPosition.y + stickObjectSize.y / 2f)
        {
            stick = true;
            isHovering = true;
            SprinkleLookStick.localPosition = imageVisiblePosition;
            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;
            return;
        }

        if (worldMousePosition.x >= feedObjectPosition.x - feedObjectSize.x / 2f &&
            worldMousePosition.x <= feedObjectPosition.x + feedObjectSize.x / 2f &&
            worldMousePosition.y >= feedObjectPosition.y - feedObjectSize.y / 2f &&
            worldMousePosition.y <= feedObjectPosition.y + feedObjectSize.y / 2f)
        {
            feed = true;
            isHovering = true;
            SprinkleEating.localPosition = imageVisiblePosition;
            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;
            return;
        }

        if (!isHovering)
        {
            if (present) currentEmotion = 1;
            if (stick) currentEmotion = 2;
            if (feed) currentEmotion = 0;
        }
    }

    void UpdateImagePositions()
    {
        if (isHovering)
        {
            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;
            return;
        }

        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;

        if (currentEmotion == 0) neutralSprinkles.localPosition = imageVisiblePosition;
        if (currentEmotion == 1) happySprinkles.localPosition = imageVisiblePosition;
        if (currentEmotion == 2) grumpySprinkles.localPosition = imageVisiblePosition;
    }
}

