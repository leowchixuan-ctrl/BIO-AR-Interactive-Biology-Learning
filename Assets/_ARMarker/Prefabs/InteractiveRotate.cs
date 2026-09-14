using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls; // Added to help read the touch controls properly

public class InteractiveRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 0.2f;

    [Header("Zoom Settings (Multipliers)")]
    public float touchZoomSpeed = 0.005f;
    public float mouseZoomSpeed = 0.05f;
    public float minZoom = 0.5f;  // 0.5 = Half the starting size
    public float maxZoom = 3.0f;  // 3.0 = Triple the starting size

    private Camera mainCamera;
    private Vector3 localMeshCenter;
    private bool hasMesh = false;

    private Vector3 startingScale;
    private float currentZoomMultiplier = 1.0f;

    void Start()
    {
        mainCamera = Camera.main;

        // Remember the exact scale the model started at
        startingScale = transform.localScale;

        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            localMeshCenter = meshFilter.sharedMesh.bounds.center;
            hasMesh = true;
        }
    }

    void Update()
    {
        int activeTouches = 0;
        TouchControl touch0 = null;
        TouchControl touch1 = null;

        // 1. Count how many fingers are ACTUALLY pressing the screen right now
        if (Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.isPressed)
                {
                    if (activeTouches == 0) touch0 = touch;
                    else if (activeTouches == 1) touch1 = touch;
                    activeTouches++;
                }
            }
        }

        // 2. MOBILE TOUCH CONTROLS
        if (activeTouches == 1)
        {
            // One Finger = Rotate
            RotateLikeTrackball(touch0.delta.ReadValue());
        }
        else if (activeTouches == 2)
        {
            // Two Fingers = Zoom (Pinch)
            Vector2 t0Pos = touch0.position.ReadValue();
            Vector2 t0Delta = touch0.delta.ReadValue();
            Vector2 t1Pos = touch1.position.ReadValue();
            Vector2 t1Delta = touch1.delta.ReadValue();

            Vector2 t0Prev = t0Pos - t0Delta;
            Vector2 t1Prev = t1Pos - t1Delta;

            float prevMagnitude = (t0Prev - t1Prev).magnitude;
            float currentMagnitude = (t0Pos - t1Pos).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            ScaleModel(difference * touchZoomSpeed);
        }
        // 3. PC MOUSE CONTROLS
        else if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                RotateLikeTrackball(Mouse.current.delta.ReadValue());
            }

            float scroll = Mouse.current.scroll.ReadValue().y;
            if (scroll != 0)
            {
                ScaleModel(scroll * mouseZoomSpeed);
            }
        }
    }

    void RotateLikeTrackball(Vector2 delta)
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 trueCenter = hasMesh ? transform.TransformPoint(localMeshCenter) : transform.position;

        transform.RotateAround(trueCenter, mainCamera.transform.up, -delta.x * rotationSpeed);
        transform.RotateAround(trueCenter, mainCamera.transform.right, delta.y * rotationSpeed);
    }

    void ScaleModel(float increment)
    {
        currentZoomMultiplier += increment;
        currentZoomMultiplier = Mathf.Clamp(currentZoomMultiplier, minZoom, maxZoom);
        transform.localScale = startingScale * currentZoomMultiplier;
    }
}