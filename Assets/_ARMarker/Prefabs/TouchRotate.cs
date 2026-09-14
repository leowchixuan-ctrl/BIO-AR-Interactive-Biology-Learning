using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    // Adjust this to make the rotation more or less sensitive
    public float rotationSpeed = 0.5f;

    void Update()
    {
        // Check if there is exactly one finger touching the screen
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the finger is moving
            if (touch.phase == TouchPhase.Moved)
            {
                // Rotate the object based on how the finger moves across the screen
                // Swiping left/right rotates around the Y axis (up)
                transform.Rotate(Vector3.up, -touch.deltaPosition.x * rotationSpeed, Space.World);

                // Swiping up/down rotates around the X axis (right)
                transform.Rotate(Vector3.right, touch.deltaPosition.y * rotationSpeed, Space.World);
            }
        }
    }
}