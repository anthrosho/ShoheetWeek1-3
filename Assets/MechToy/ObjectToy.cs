using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectToy : MonoBehaviour
{
    public Camera gameCamera;
    public Vector2 hoverAreaSize = new Vector2(1f, 1f);
    public Vector3 normalScale = Vector3.one;
    public Vector3 expandScale = new Vector3(1.2f, 1.2f, 1f);
    public float hoverSpeed = 3f;
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.05f;

    private float lerpAmount = 0f;
    private bool isHovering = false;

    void Update()
    {
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = gameCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        if (mouseWorld.x >= transform.position.x - hoverAreaSize.x / 2f &&
            mouseWorld.x <= transform.position.x + hoverAreaSize.x / 2f &&
            mouseWorld.y >= transform.position.y - hoverAreaSize.y / 2f &&
            mouseWorld.y <= transform.position.y + hoverAreaSize.y / 2f)
        {
            isHovering = true;
        }
        else
        {
            isHovering = false;
        }

        if (isHovering)
        {
            lerpAmount += Time.deltaTime * hoverSpeed;
            if (lerpAmount > 1f) lerpAmount = 1f;
        }
        else
        {
            lerpAmount -= Time.deltaTime * hoverSpeed;
            if (lerpAmount < 0f) lerpAmount = 0f;
        }

        Vector3 baseScale = Vector3.Lerp(normalScale, expandScale, lerpAmount);

        // Passive heartbeat pulse
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        Vector3 pulseVector = new Vector3(pulse, pulse, 0f);
        transform.localScale = baseScale + pulseVector;
    }
}
