using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityToColor : MonoBehaviour
{
    [Tooltip("The target GameObject to color.")]
    public GameObject targetToColor;

    [Tooltip("The minimum distance for the full color to be applied to the target.")]
    public float minDistance = 0.1f;

    [Tooltip("The maximum distance for any color to be applied to the target.")]
    public float maxDistance = 0.2f;

    [Tooltip("Whether to show the lines representing the mathematical calculations.")]
    public bool showCalculations = false;

    // The last amount of color added to the target.
    private Color addedColor;

    // Start is called before the first frame update
    void Start()
    {
        // No color initialled added.
        addedColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        // Check that the target to color exists.
        if (targetToColor != null)
        {
            // Check that both objects have mesh renderers.
            if (targetToColor.GetComponent<MeshRenderer>() && GetComponent<MeshRenderer>())
            {
                // Get the target's material.
                Material targetMaterial = targetToColor.GetComponent<MeshRenderer>().material;

                // Get the target's color.
                Color targetColor = targetMaterial.color;

                // Subtract the last added color.
                targetColor -= addedColor;

                // Get the current object's material.
                Material currentMaterial = GetComponent<MeshRenderer>().material;

                // Get the current color.
                Color currentColor = currentMaterial.color;

                // Get the current distance between the target and the current object.
                float currentDistance = Vector3.Distance(targetToColor.transform.position, transform.position);

                // Calculate an unbounded scalar based on the current, min, and max distances.
                float scalar = (currentDistance - minDistance) / (maxDistance - minDistance);

                // Clamp the scalar between 0 and 1.
                scalar = Mathf.Clamp(scalar, 0.0f, 1.0f);

                // Inverse the scalar.
                scalar = 1.0f - scalar;

                // Use the scalar to calculate the amount of color to add.
                addedColor.r = currentColor.r * scalar;
                addedColor.g = currentColor.g * scalar;
                addedColor.b = currentColor.b * scalar;
                addedColor.a = currentColor.a * scalar;

                // Add the color to the target's color.
                targetColor += addedColor;

                // Set the target's color back to the material.
                targetMaterial.color = targetColor;

                // Show the calculations.
                if (showCalculations)
                {
                    // Show the current distance between the target and the current object as a line of the added color.
                    Debug.DrawLine(targetToColor.transform.position, transform.position, addedColor, Time.deltaTime, false);
                }
            }
        }
    }
}
