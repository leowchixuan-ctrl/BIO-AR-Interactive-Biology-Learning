using UnityEngine;
using UnityEngine.EventSystems; // Uses the UI system instead of the XR input!

public class OrganInteract : MonoBehaviour, IDragHandler
{
    [Header("Target Setup")]
    public Transform modelSpawnPoint;

    private bool isRotateMode = true;

    public void EnableRotateMode()
    {
        isRotateMode = true;
    }

    public void EnableZoomMode()
    {
        isRotateMode = false;
    }

    // This automatically catches ALL swipes and mouse drags across the UI
    public void OnDrag(PointerEventData eventData)
    {
        if (modelSpawnPoint == null) return;

        Vector2 swipeDelta = eventData.delta;

        if (isRotateMode)
        {
            modelSpawnPoint.Rotate(Vector3.up, -swipeDelta.x * 0.2f, Space.World);
            modelSpawnPoint.Rotate(Vector3.right, swipeDelta.y * 0.2f, Space.World);
        }
        else
        {
            float scaleAmount = swipeDelta.y * 0.005f;
            Vector3 newScale = modelSpawnPoint.localScale + new Vector3(scaleAmount, scaleAmount, scaleAmount);

            // Prevent it from shrinking to nothing or growing too massive
            newScale.x = Mathf.Clamp(newScale.x, 0.2f, 3f);
            newScale.y = Mathf.Clamp(newScale.y, 0.2f, 3f);
            newScale.z = Mathf.Clamp(newScale.z, 0.2f, 3f);

            modelSpawnPoint.localScale = newScale;
        }
    }
}