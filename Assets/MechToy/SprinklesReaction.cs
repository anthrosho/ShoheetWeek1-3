using UnityEngine;
using UnityEngine.InputSystem;

public class SprinklesReaction : MonoBehaviour
{
    public Camera gameCamera;

    public Vector3 imageVisiblePosition = Vector3.zero;
    public Vector3 imageHiddenPosition = new Vector3(0, -10000f, 0);

    public float lerpSpeed = 6f;

    public Transform neutralSprinkles;
    public Transform happySprinkles;
    public Transform grumpySprinkles;

    public Transform sprinkleHoldPresent;
    public Transform sprinkleEating;
    public Transform sprinkleLookStick;
    public Transform sprinkleAboutToFeed;
    public Transform sprinklePetting;

    public bool isNeutral = true;
    public bool isHappy;
    public bool isGrumpy;

    public bool present;
    public bool stick;
    public bool feed;

    Transform currentSprinkle;

    public Vector3 petHeadOffset;
    public Vector2 petAreaSize;

    public Vector3 normalScale = Vector3.one;
    public Vector3 squishScale = new Vector3(1.2f, 0.8f, 1f);
    public AnimationCurve squishCurve;

    float squishTime;
    bool isPetting;

    public Vector3 feedObjectPosition;
    public float feedNearDistance = 1.5f;
    public float feedTouchDistance = 0.6f;

    void Start()
    {
        HideAll();

        currentSprinkle = neutralSprinkles;
        currentSprinkle.localPosition = imageVisiblePosition;
        currentSprinkle.localScale = normalScale;
    }

    void Update()
    {
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = gameCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        isPetting = false;

        Vector3 headWorld = currentSprinkle.position + petHeadOffset;

        if (mouseWorld.x >= headWorld.x - petAreaSize.x / 2f &&
            mouseWorld.x <= headWorld.x + petAreaSize.x / 2f &&
            mouseWorld.y >= headWorld.y - petAreaSize.y / 2f &&
            mouseWorld.y <= headWorld.y + petAreaSize.y / 2f)
        {
            isPetting = true;
        }

        float distanceToFeed =
            Mathf.Abs(mouseWorld.x - feedObjectPosition.x) +
            Mathf.Abs(mouseWorld.y - feedObjectPosition.y);

        if (isPetting)
        {
            SwitchTo(sprinklePetting);
            IncreaseSquish();
        }
        else
        {
            DecreaseSquish();

            if (feed && distanceToFeed <= feedTouchDistance)
            {
                SwitchTo(sprinkleEating);
            }
            else if (feed && distanceToFeed <= feedNearDistance)
            {
                SwitchTo(sprinkleAboutToFeed);
            }
            else if (present)
            {
                SwitchTo(sprinkleHoldPresent);
            }
            else if (stick)
            {
                SwitchTo(sprinkleLookStick);
            }
            else
            {
                if (isHappy)
                {
                    SwitchTo(happySprinkles);
                }

                if (isGrumpy)
                {
                    SwitchTo(grumpySprinkles);
                }

                if (isNeutral)
                {
                    SwitchTo(neutralSprinkles);
                }
            }
        }

        float curveValue = squishCurve.Evaluate(squishTime);

        currentSprinkle.localScale = Vector3.Lerp(
            normalScale,
            squishScale,
            curveValue
        );
    }

    void IncreaseSquish()
    {
        squishTime += Time.deltaTime * lerpSpeed;
        if (squishTime > 1f)
        {
            squishTime = 1f;
        }
    }

    void DecreaseSquish()
    {
        squishTime -= Time.deltaTime * lerpSpeed;
        if (squishTime < 0f)
        {
            squishTime = 0f;
        }
    }

    void SwitchTo(Transform target)
    {
        if (currentSprinkle == target)
        {
            return;
        }

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

        sprinkleHoldPresent.localPosition = imageHiddenPosition;
        sprinkleEating.localPosition = imageHiddenPosition;
        sprinkleLookStick.localPosition = imageHiddenPosition;
        sprinkleAboutToFeed.localPosition = imageHiddenPosition;
        sprinklePetting.localPosition = imageHiddenPosition;
    }

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
}
