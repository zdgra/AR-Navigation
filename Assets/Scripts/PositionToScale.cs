using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToScale : MonoBehaviour
{
    [Tooltip("The origin that defines the relative position.")]
    public Transform origin;

    [Tooltip("The scale factor to multiply the relative position by.")]
    public float scaleFactor = 10.0f;

    [Tooltip("Whether to show the lines representing the mathematical calculations. The red line represents the origin's X axis. The green line represents the origin's Y axis. The blue line represents the origin's Z axis. The white line represents the object's relative position.")]
    public bool showCalculations = false;

    // The original scale of the local object.
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        // Save the original scale of the local object.
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Check that the origin is visible.
        if (origin.gameObject.activeInHierarchy)
        {
            // Calculate the current position relative to the origin's position and rotation.
            Vector3 relativePosition = Quaternion.Inverse(origin.rotation) * (transform.position - origin.position);

            // Set the scale based on its original value and its current relative position.
            transform.localScale = originalScale + (relativePosition * scaleFactor);

            // Show the calculations.
            if (showCalculations)
            {
                // Show the origin's X-axis as a red line.
                Debug.DrawRay(origin.position, Vector3.right, Color.red, Time.deltaTime, false);

                // Show the origin's Y-axis as a green line.
                Debug.DrawRay(origin.position, Vector3.up, Color.green, Time.deltaTime, false);

                // Show the origin's Z-axis as a blue line.
                Debug.DrawRay(origin.position, Vector3.forward, Color.blue, Time.deltaTime, false);

                // Show the relative position as a white line.
                Debug.DrawRay(origin.position, relativePosition, Color.white, Time.deltaTime, false);
            }
        }
    }
}
