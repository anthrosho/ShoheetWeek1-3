using UnityEngine;
using UnityEngine.InputSystem;

public class SprinklesReaction : MonoBehaviour
{
    public Vector3 defaultPosition;
    public Vector3 imageVisiblePosition = Vector3.zero;
    public Vector3 imageHiddenPosition = new Vector3(0, -10000f, 0);

    public Transform neutralSprinkles;
    public Transform happySprinkles;
    public Transform grumpySprinkles;

    public Transform SprinkleHoldPresent;
    public Transform SprinkleEating;
    public Transform SprinkleLookStick;
    public Transform SprinklesAboutToFeed;

    public Vector3 presentObjectPosition;
    public Vector2 presentObjectSize;

    public Vector3 stickObjectPosition;
    public Vector2 stickObjectSize;

    public Vector3 feedObjectPosition;
    public Vector2 feedObjectSize;
    public Vector2 feedApproachSize;

    public Camera gameCamera;
    public float lerpSpeed = 5f;

    public bool hoveringPresent;
    public bool hoveringStick;
    public bool hoveringFeed;
    public bool approachingFeed;

    public int currentEmotion = 0;

    public Transform sprinklesPetting;
    public Vector3 pettingLocalPosition;
    public Vector2 pettingAreaSize;

    public Vector3 normalScale = Vector3.one;
    public Vector3 squishScale = new Vector3(1.2f, 0.8f, 1f);
    public AnimationCurve squishCurve;
    public float squishSpeed = 3f;

    float squishTime = 0f;
    bool isPetting = false;

    void Start()
    {
        defaultPosition = transform.localPosition;

        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;

        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;
        SprinklesAboutToFeed.localPosition = imageHiddenPosition;

        sprinklesPetting.localPosition = imageHiddenPosition;
        sprinklesPetting.localScale = normalScale;

        currentEmotion = 0;
        UpdateDefaultImages();
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime * lerpSpeed);

        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = gameCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        // Reset petting flag
        isPetting = false;

        // Petting detection relative to fixed local position of petting image
        Vector3 pettingWorldPos = transform.position + pettingLocalPosition;
        if (mouseWorld.x >= pettingWorldPos.x - pettingAreaSize.x / 2f &&
            mouseWorld.x <= pettingWorldPos.x + pettingAreaSize.x / 2f &&
            mouseWorld.y >= pettingWorldPos.y - pettingAreaSize.y / 2f &&
            mouseWorld.y <= pettingWorldPos.y + pettingAreaSize.y / 2f)
        {
            isPetting = true;
        }

        // Petting behavior
        if (isPetting)
        {
            neutralSprinkles.localPosition = imageHiddenPosition;
            happySprinkles.localPosition = imageHiddenPosition;
            grumpySprinkles.localPosition = imageHiddenPosition;

            sprinklesPetting.localPosition = pettingLocalPosition;

            squishTime += Time.deltaTime * squishSpeed;
            if (squishTime > 1f) squishTime = 1f;
            float curveValue = squishCurve.Evaluate(squishTime);
            sprinklesPetting.localScale = Vector3.Lerp(normalScale, squishScale, curveValue);
        }
        else
        {
            sprinklesPetting.localPosition = imageHiddenPosition;
            sprinklesPetting.localScale = normalScale;

            squishTime -= Time.deltaTime * squishSpeed;
            if (squishTime < 0f) squishTime = 0f;
        }

        CheckMouseZones(mouseWorld);
        UpdateDefaultImages();
    }

    void CheckMouseZones(Vector3 mouseWorld)
    {
        hoveringPresent = false;
        hoveringStick = false;
        hoveringFeed = false;
        approachingFeed = false;

        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;
        SprinklesAboutToFeed.localPosition = imageHiddenPosition;

        if (mouseWorld.x >= feedObjectPosition.x - feedApproachSize.x / 2f &&
            mouseWorld.x <= feedObjectPosition.x + feedApproachSize.x / 2f &&
            mouseWorld.y >= feedObjectPosition.y - feedApproachSize.y / 2f &&
            mouseWorld.y <= feedObjectPosition.y + feedApproachSize.y / 2f)
        {
            approachingFeed = true;
            SprinklesAboutToFeed.localPosition = imageVisiblePosition;
        }

        if (mouseWorld.x >= feedObjectPosition.x - feedObjectSize.x / 2f &&
            mouseWorld.x <= feedObjectPosition.x + feedObjectSize.x / 2f &&
            mouseWorld.y >= feedObjectPosition.y - feedObjectSize.y / 2f &&
            mouseWorld.y <= feedObjectPosition.y + feedObjectSize.y / 2f)
        {
            hoveringFeed = true;
            approachingFeed = false;
            SprinklesAboutToFeed.localPosition = imageHiddenPosition;
            SprinkleEating.localPosition = imageVisiblePosition;
            currentEmotion = 0;
            return;
        }

        if (mouseWorld.x >= presentObjectPosition.x - presentObjectSize.x / 2f &&
            mouseWorld.x <= presentObjectPosition.x + presentObjectSize.x / 2f &&
            mouseWorld.y >= presentObjectPosition.y - presentObjectSize.y / 2f &&
            mouseWorld.y <= presentObjectPosition.y + presentObjectSize.y / 2f)
        {
            hoveringPresent = true;
            SprinkleHoldPresent.localPosition = imageVisiblePosition;
            currentEmotion = 1;
            return;
        }

        if (mouseWorld.x >= stickObjectPosition.x - stickObjectSize.x / 2f &&
            mouseWorld.x <= stickObjectPosition.x + stickObjectSize.x / 2f &&
            mouseWorld.y >= stickObjectPosition.y - stickObjectSize.y / 2f &&
            mouseWorld.y <= stickObjectPosition.y + stickObjectSize.y / 2f)
        {
            hoveringStick = true;
            SprinkleLookStick.localPosition = imageVisiblePosition;
            currentEmotion = 2;
            return;
        }
    }

    void UpdateDefaultImages()
    {
        if (isPetting) return; // hide default images while petting

        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;

        if (hoveringPresent) return;
        if (hoveringStick) return;
        if (hoveringFeed) return;
        if (approachingFeed) return;

        if (currentEmotion == 0) neutralSprinkles.localPosition = imageVisiblePosition;
        if (currentEmotion == 1) happySprinkles.localPosition = imageVisiblePosition;
        if (currentEmotion == 2) grumpySprinkles.localPosition = imageVisiblePosition;
    }
}
