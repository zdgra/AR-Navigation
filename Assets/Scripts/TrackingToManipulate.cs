using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires that the object already has a collider.
[RequireComponent(typeof(Collider))]
// Adds a Rigidbody to the object if one doesn't already exist.
[RequireComponent(typeof(Rigidbody))]
public class TrackingToManipulate : MonoBehaviour
{
    [Tooltip("A scalable GameObject that can be used to highlight potential manipulations.")]
    public GameObject highlight;

    // A list of the current collisions.
    private List<Collider> collisions;

    // The object currently indicated.
    private Transform indication;

    // The object currently selected.
    private Transform selection;

    // The selected object's original parent.
    private Transform originalParent;

    // Start is called before the first frame update
    void Start()
    {
        // Create the list of current collisions.
        collisions = new List<Collider>();

        // Disable the highlight object, if it exists.
        if (highlight != null)
        {
            highlight.SetActive(false);
        }

        // Make the attached collider a trigger, if it exists.
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().isTrigger = true;
        }

        // Disable gravity and indicate that physics should not affect our cursor.
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // FixedUpdate is called once per physics frame (before the graphical frame)
    private void FixedUpdate()
    {
        // Reset the list of collisions.
        collisions.Clear();
    }

    // OnTriggerStay is called every physics frame for every other collider touching the current one.
    private void OnTriggerStay(Collider other)
    {
        // Add each collider to the list of collisions.
        collisions.Add(other);
    }

    // Update is called once per frame
    void Update()
    {
        // Check that there are collisions and no selection.
        if (collisions.Count > 0 && selection == null)
        {
            // Find the closest collision.
            Collider closestCollision;

            // Which is easy if there is only one collision.
            if (collisions.Count == 1)
            {
                closestCollision = collisions[0];
            }
            // Otherwise...
            else
            {
                // Find the closest distance.
                float closestDistance = Mathf.Infinity;

                // And track the index of the closest collision.
                int closestIndex = 0;

                // For each collision
                for (int i = 0; i < collisions.Count; i++)
                {
                    // Calculate its distance.
                    float distance = Vector3.Distance(collisions[i].transform.position, transform.position);

                    // And check if it is currently the closest.
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestIndex = i;
                    }
                }

                // Set the closest collision.
                closestCollision = collisions[closestIndex];
            }

            // Activate the highlight object.
            highlight.SetActive(true);

            // Move, rotate, and scale the highlight to match the bounds of the collision.
            highlight.transform.position = closestCollision.bounds.center;
            highlight.transform.rotation = closestCollision.transform.rotation;
            highlight.transform.localScale = closestCollision.bounds.size;

            // Keep track of what object is being indicated by the highlight.
            indication = closestCollision.transform;
        }
        // If there are no collisions or there is a selection.
        else
        {
            // Deactivate the highlight.
            highlight.SetActive(false);

            // Reset the indication.
            indication = null;
        }
    }

    // Public function called by a Unity event or another script.
    public void Grab()
    {
        // If something is indicated.
        if (indication != null)
        {
            // Set it as the selection.
            selection = indication;

            // Store its original parent.
            originalParent = selection.parent;

            // Set the current object as its new parent.
            selection.parent = transform;
        }
    }

    // Public function called by a Unity event or another script.
    public void Release()
    {
        // If something is selected.
        if (selection != null)
        {
            // Restore its original parent.
            selection.parent = originalParent;

            // Clear the selection.
            selection = null;
        }
    }
}
