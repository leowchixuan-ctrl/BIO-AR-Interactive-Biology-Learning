using UnityEngine;
using UnityEngine.InputSystem;

public class DirectOrganInteract : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform modelSpawnPoint;

    [Header("Rotation")]
    public float rotateSpeed = 0.5f;

    [Header("Zoom")]
    public float zoomSpeed = 0.005f;

    public float minZoomMultiplier = 0.5f;
    public float maxZoomMultiplier = 8f;

    private Transform spawnedOrgan;

    private Vector3 originalScale;
    private float zoomMultiplier = 1f;

    void Update()
    {
        if (modelSpawnPoint == null)
            return;

        // Find the organ spawned by ARDisplayManager
        if (spawnedOrgan == null)
        {
            if (modelSpawnPoint.childCount > 0)
            {
                spawnedOrgan =
                    modelSpawnPoint.GetChild(0);

                // Remember THIS organ's original scale
                originalScale =
                    spawnedOrgan.localScale;

                zoomMultiplier = 1f;

                Debug.Log(
                    "Found organ: " +
                    spawnedOrgan.name +
                    " Original Scale: " +
                    originalScale);
            }
            else
            {
                return;
            }
        }

        int touchCount = 0;

        // --------------------------
        // PHONE TOUCH INPUT
        // --------------------------

        if (Touchscreen.current != null)
        {
            foreach (var touch
                     in Touchscreen.current.touches)
            {
                if (touch.press.isPressed)
                    touchCount++;
            }

            // One finger = rotate
            if (touchCount == 1)
            {
                Vector2 swipeDelta =
                    Touchscreen.current
                    .touches[0]
                    .delta
                    .ReadValue();

                spawnedOrgan.Rotate(
                    Vector3.up,
                    -swipeDelta.x * rotateSpeed,
                    Space.World);

                spawnedOrgan.Rotate(
                    Vector3.right,
                    swipeDelta.y * rotateSpeed,
                    Space.World);
            }

            // Two fingers = zoom
            else if (touchCount == 2)
            {
                var touch0 =
                    Touchscreen.current.touches[0];

                var touch1 =
                    Touchscreen.current.touches[1];

                Vector2 pos0 =
                    touch0.position.ReadValue();

                Vector2 pos1 =
                    touch1.position.ReadValue();

                Vector2 delta0 =
                    touch0.delta.ReadValue();

                Vector2 delta1 =
                    touch1.delta.ReadValue();

                float previousDistance =
                    ((pos0 - delta0) -
                     (pos1 - delta1)).magnitude;

                float currentDistance =
                    (pos0 - pos1).magnitude;

                float difference =
                    currentDistance -
                    previousDistance;

                zoomMultiplier +=
                    difference * zoomSpeed;

                zoomMultiplier =
                    Mathf.Clamp(
                        zoomMultiplier,
                        minZoomMultiplier,
                        maxZoomMultiplier);

                spawnedOrgan.localScale =
                    originalScale *
                    zoomMultiplier;
            }
        }

        // --------------------------
        // MOUSE TESTING
        // --------------------------

        if (touchCount == 0 &&
            Mouse.current != null &&
            Mouse.current.leftButton.isPressed)
        {
            Vector2 mouseDelta =
                Mouse.current.delta.ReadValue();

            spawnedOrgan.Rotate(
                Vector3.up,
                -mouseDelta.x * rotateSpeed,
                Space.World);

            spawnedOrgan.Rotate(
                Vector3.right,
                mouseDelta.y * rotateSpeed,
                Space.World);
        }
    }
}