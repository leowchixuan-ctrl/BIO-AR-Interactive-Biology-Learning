using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // You can adjust this speed from the Unity Inspector
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotates the object around its Y-axis (up) at a consistent speed
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}