using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Three possible rotation states.
public enum RotateState
{
    none,
    clockwise,
    counterclockwise
}

public class RotateToSwitch : MonoBehaviour
{
    [Tooltip("The objects to display within the CoCube in sequence.")]
    public List<GameObject> interiorObjects;

    [Tooltip("Whether to show the lines representing the mathematical calculations. The blue line represents the world's forward vector. The red line represents the world's right vector. The green line represents the world's left vector. The white line represents the object's current projected rotation.")]
    public bool showCalculations = false;

    // The index of the current object to display.
    private int index;

    // The current rotation state.
    private RotateState rotateState = RotateState.none;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the index to the first object.
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the cross product of the world's forward and left vectors.
        Vector3 worldLeftCross = Vector3.Cross(Vector3.forward, Vector3.left).normalized;

        // Calculate the cross product of the world's forward and right vectors.
        Vector3 worldRightCross = Vector3.Cross(Vector3.forward, Vector3.right).normalized;

        // Project the current vector onto the world plane. 
        // Using the up vector because Vuforia lays targets down and their forward vectors become up vectors.
        Vector3 projectedVector = Vector3.ProjectOnPlane(transform.up, worldRightCross).normalized;

        // Determine the cross product of the projected and forward vectors.
        Vector3 forwardCross = Vector3.Cross(Vector3.forward, projectedVector).normalized;

        // Determine the cross product of the projected and left vectors.
        Vector3 leftCross = Vector3.Cross(projectedVector, Vector3.left).normalized;

        // Determine the cross product of the projected and right vectors.
        Vector3 rightCross = Vector3.Cross(projectedVector, Vector3.right).normalized;

        // Check if the current rotation is between the forward and left rotations.
        if (forwardCross == worldLeftCross && leftCross == worldLeftCross)
        {
            // Decrement index if last state was counterclockwise.
            if (rotateState == RotateState.counterclockwise)
            {
                index--;
            }

            // Update rotate state.
            rotateState = RotateState.clockwise;
        }
        // Check if the current rotation is between the forward and right rotations.
        else if (forwardCross == worldRightCross && rightCross == worldRightCross)
        {
            // Increment index if last state was clockwise.
            if (rotateState == RotateState.clockwise)
            {
                index++;
            }

            // Update rotate state.
            rotateState = RotateState.counterclockwise;
        }
        // Otherwise the current rotation is between the left and right rotations.
        else
        {
            // Update rotate state.
            rotateState = RotateState.none;
        }

        // Disable all the interior objects.
        for (int i = 0; i < interiorObjects.Count; i++)
        {
            if (interiorObjects[i] != null)
            {
                interiorObjects[i].SetActive(false);
            }
        }

        // Show the current interior object.
        if(0 <= index && index < interiorObjects.Count && interiorObjects[index] != null)
        {
            interiorObjects[index].SetActive(true);
        }

        // Show the calculations.
        if (showCalculations)
        {
            // Show the world's forward vector as a blue line.
            Debug.DrawRay(transform.position, Vector3.forward * 0.1f, Color.blue);

            // Show the world's right vector as a red line.
            Debug.DrawRay(transform.position, Vector3.right * 0.1f, Color.red);

            // Show the world's left vector as a green line.
            Debug.DrawRay(transform.position, Vector3.left * 0.1f, Color.green);

            // Show the projected vector as a white line.
            Debug.DrawRay(transform.position, projectedVector * 0.1f, Color.white);
        }
    }
}
