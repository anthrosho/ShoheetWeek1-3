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
    public Transform SprinklePetting;

    public Vector3 presentObjectPosition;
    public Vector2 presentObjectSize;

    public Vector3 stickObjectPosition;
    public Vector2 stickObjectSize;

    public Vector3 feedObjectPosition;
    public Vector2 feedObjectSize;
    public Vector2 feedApproachSize;

    public Vector3 petHeadOffset;
    public Vector2 petAreaSize;

    public Camera gameCamera;
    public float lerpSpeed = 5f;

    public Vector3 normalScale = Vector3.one;
    public Vector3 squishScale = new Vector3(1.2f, 0.8f, 1f);
    public AnimationCurve squishCurve;

    public bool hoveringPresent;
    public bool hoveringStick;
    public bool hoveringFeed;
    public bool approachingFeed;
    public bool isPetting;
    public bool feed;

    public int currentEmotion = 0;
    float squishTime;
    Transform currentSprinkle;

    void Start()
    {
        defaultPosition = transform.localPosition;

        HideAll();

        currentSprinkle = neutralSprinkles;
        currentSprinkle.localPosition = imageVisiblePosition;
        currentSprinkle.localScale = normalScale;
        currentEmotion = 0;
        UpdateDefaultImages();
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime * lerpSpeed);

        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = gameCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        hoveringPresent = false;
        hoveringStick = false;
        hoveringFeed = false;
        approachingFeed = false;
        isPetting = false;

        // Reset all hover images
        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;
        SprinklesAboutToFeed.localPosition = imageHiddenPosition;
        SprinklePetting.localPosition = imageHiddenPosition;

        // Check petting first
        Vector3 headWorld = currentSprinkle.position + petHeadOffset;
        if (mouseWorld.x >= headWorld.x - petAreaSize.x / 2f &&
            mouseWorld.x <= headWorld.x + petAreaSize.x / 2f &&
            mouseWorld.y >= headWorld.y - petAreaSize.y / 2f &&
            mouseWorld.y <= headWorld.y + petAreaSize.y / 2f)
        {
            isPetting = true;
            SwitchTo(SprinklePetting);
            IncreaseSquish();
        }
        else
        {
            DecreaseSquish();

            // Check Feed interaction
            float distanceToFeed =
                Mathf.Abs(mouseWorld.x - feedObjectPosition.x) +
                Mathf.Abs(mouseWorld.y - feedObjectPosition.y);

            if (feed && distanceToFeed <= feedObjectSize.x / 2f)
            {
                hoveringFeed = true;
                SprinklesAboutToFeed.localPosition = imageHiddenPosition;
                SprinkleEating.localPosition = imageVisiblePosition;
                currentEmotion = 0;
            }
            else if (feed && distanceToFeed <= feedApproachSize.x / 2f)
            {
                approachingFeed = true;
                SprinklesAboutToFeed.localPosition = imageVisiblePosition;
            }
            // Check Present
            else if (mouseWorld.x >= presentObjectPosition.x - presentObjectSize.x / 2f &&
                     mouseWorld.x <= presentObjectPosition.x + presentObjectSize.x / 2f &&
                     mouseWorld.y >= presentObjectPosition.y - presentObjectSize.y / 2f &&
                     mouseWorld.y <= presentObjectPosition.y + presentObjectSize.y / 2f)
            {
                hoveringPresent = true;
                SprinkleHoldPresent.localPosition = imageVisiblePosition;
                currentEmotion = 1;
            }
            // Check Stick
            else if (mouseWorld.x >= stickObjectPosition.x - stickObjectSize.x / 2f &&
                     mouseWorld.x <= stickObjectPosition.x + stickObjectSize.x / 2f &&
                     mouseWorld.y >= stickObjectPosition.y - stickObjectSize.y / 2f &&
                     mouseWorld.y <= stickObjectPosition.y + stickObjectSize.y / 2f)
            {
                hoveringStick = true;
                SprinkleLookStick.localPosition = imageVisiblePosition;
                currentEmotion = 2;
            }
        }

        UpdateDefaultImages();

        float curveValue = squishCurve.Evaluate(squishTime);
        currentSprinkle.localScale = Vector3.Lerp(normalScale, squishScale, curveValue);
    }

    void IncreaseSquish()
    {
        squishTime += Time.deltaTime * lerpSpeed;
        if (squishTime > 1f) squishTime = 1f;
    }

    void DecreaseSquish()
    {
        squishTime -= Time.deltaTime * lerpSpeed;
        if (squishTime < 0f) squishTime = 0f;
    }

    void SwitchTo(Transform target)
    {
        if (currentSprinkle == target) return;

        HideAll();
        currentSprinkle = target;
        currentSprinkle.localPosition = imageVisiblePosition;
        currentSprinkle.localScale = normalScale;
    }

    void HideAll()
    {
        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;

        SprinkleHoldPresent.localPosition = imageHiddenPosition;
        SprinkleEating.localPosition = imageHiddenPosition;
        SprinkleLookStick.localPosition = imageHiddenPosition;
        SprinklesAboutToFeed.localPosition = imageHiddenPosition;
        SprinklePetting.localPosition = imageHiddenPosition;
    }

    void UpdateDefaultImages()
    {
        neutralSprinkles.localPosition = imageHiddenPosition;
        happySprinkles.localPosition = imageHiddenPosition;
        grumpySprinkles.localPosition = imageHiddenPosition;

        if (hoveringPresent) return;
        if (hoveringStick) return;
        if (hoveringFeed) return;
        if (approachingFeed) return;
        if (isPetting) return;

        if (currentEmotion == 0) neutralSprinkles.localPosition = imageVisiblePosition;
        if (currentEmotion == 1) happySprinkles.localPosition = imageVisiblePosition;
        if (currentEmotion == 2) grumpySprinkles.localPosition = imageVisiblePosition;
    }
}
