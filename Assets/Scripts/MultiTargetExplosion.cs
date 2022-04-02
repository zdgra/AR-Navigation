/*
*   Copyright (C) 2021 University of Central Florida, created by Dr. Ryan P. McMahan.
*
*   This program is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*   Primary Author Contact:  Dr. Ryan P. McMahan <rpm@ucf.edu>
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for creating an explosion diagram using the front side of a Vuforia MultiTarget.
public class MultiTargetExplosion : MonoBehaviour
{
    // The left half of the front side of the MultiTarget.
    [SerializeField]
    [Tooltip("The left half of the front side of the MultiTarget.")]
    private GameObject m_LeftFront;
    public GameObject LeftFront { get { return m_LeftFront; } set { m_LeftFront = value; } }

    // The right half of the front side of the MultiTarget.
    [SerializeField]
    [Tooltip("The right half of the front side of the MultiTarget.")]
    private GameObject m_RightFront;
    public GameObject RightFront { get { return m_RightFront; } set { m_RightFront = value; } }

    // The duration of the explosion animation in seconds.
    [SerializeField]
    [Tooltip("The duration of the explosion animation in seconds.")]
    private float m_AnimationDuration = 1.0f;
    public float AnimationDuration { get { return m_AnimationDuration; } set { m_AnimationDuration = value; } }

    // How long the explosion animation has been playing in seconds.
    private float m_AnimationTime = 0.0f;

    // Whether the diagram is currently exploded.
    private bool m_Exploded = false;

    // Whether the explosion animation is currently playing.
    private bool m_Exploding = false;

    // Whether the reverse explosion animation is currently playing.
    private bool m_Unexploding = false;

    // The original local positions of the two halves.
    private Vector3 m_LeftOriginalPosition;
    private Vector3 m_RightOriginalPosition;

    // The exploded local positions of the two halves.
    private Vector3 m_LeftExplodedPosition;
    private Vector3 m_RightExplodedPosition;

    // Starts the explosion animation if not already exploded or exploding.
    public void Explode()
    {
        // Ensure not already exploded and not currently exploding or unexploding.
        if (!m_Exploded && !m_Exploding && !m_Unexploding)
        {
            // Begin the exploding animation.
            m_Exploding = true;
            m_AnimationTime = 0.0f;
        }
    }

    // Starts the reverse explosion animation if exploded and not unexploding.
    public void Unexplode()
    {
        // Ensure exploded and not currently exploding or unexploding.
        if (m_Exploded && !m_Exploding && !m_Unexploding)
        {
            // Beging the unexploding animation.
            m_Unexploding = true;
            m_AnimationTime = 0.0f;
        }
    }

    // Starts the appropriate explosion animation based on whether currently exploded or not.
    public void Switch()
    {
        // If not currently exploded, explode.
        if (!m_Exploded) { Explode(); }
        // If currently exploded, unexplode.
        else { Unexplode(); }
    }

    // Start is called before the first frame update.
    void Start()
    {
        // Ensure the left front object exists.
        if (m_LeftFront != null)
        {
            // Store the original local position.
            m_LeftOriginalPosition = m_LeftFront.transform.localPosition;
            // Calculate the exploded local position based on the original position and the width of the front.
            m_LeftExplodedPosition = m_LeftFront.transform.localPosition;
            m_LeftExplodedPosition.x -= m_LeftFront.transform.localScale.x;
        }
        // Provide an error if the left front object was not found.
        else { Debug.LogError("[" + gameObject.name + "][MultiTargetExplosion]: The Left Front (GameObject) is not set."); }
        // Ensure the right front object exists.
        if (m_RightFront != null)
        {
            // Store the original local position.
            m_RightOriginalPosition = m_RightFront.transform.localPosition;
            // Calculate the exploded local position based on the original position and the width of the front.
            m_RightExplodedPosition = m_RightFront.transform.localPosition;
            m_RightExplodedPosition.x += m_RightFront.transform.localScale.x;
        }
        // Provide an error if the right front object was not found.
        else { Debug.LogError("[" + gameObject.name + "][MultiTargetExplosion]: The Right Front (GameObject) is not set."); }
    }

    // Update is called once per frame.
    void Update()
    {
        // If the exploding animation is currently playing.
        if (m_Exploding)
        {
            // Account for how long the animation has been playing.
            m_AnimationTime += Time.deltaTime;
            // If the animation has been playing longer than its duration.
            if (m_AnimationTime > m_AnimationDuration)
            {
                // Set the animation time to the duration.
                m_AnimationTime = m_AnimationDuration;
                // The exploding animation will no longer be playing and the diagram will be exploded.
                m_Exploding = false;
                m_Exploded = true;
            }
            // Ensure the left front object exists.
            if (m_LeftFront != null)
            {
                // Linearly interpolate the left front object from its original position to its target exploded position based on the animation time and duration.
                m_LeftFront.transform.localPosition = Vector3.Lerp(m_LeftOriginalPosition, m_LeftExplodedPosition, m_AnimationTime / m_AnimationDuration);
            }
            // Provide an error if the left front object was not found.
            else { Debug.LogError("[" + gameObject.name + "][MultiTargetExplosion]: The Left Front (GameObject) is not set."); }
            // Ensure the right front object exists.
            if (m_RightFront != null)
            {
                // Linearly interpolate the right front object from its original position to its target exploded position based on the animation time and duration.
                m_RightFront.transform.localPosition = Vector3.Lerp(m_RightOriginalPosition, m_RightExplodedPosition, m_AnimationTime / m_AnimationDuration);
            }
            // Provide an error if the right front object was not found.
            else { Debug.LogError("[" + gameObject.name + "][MultiTargetExplosion]: The Right Front (GameObject) is not set."); }
        }
        // If the unexploding animation is currently playing.
        else if (m_Unexploding)
        {
            // Account for how long the animation has been playing.
            m_AnimationTime += Time.deltaTime;
            // If the animation has been playing longer than its duration.
            if (m_AnimationTime > m_AnimationDuration)
            {
                // Set the animation time to the duration.
                m_AnimationTime = m_AnimationDuration;
                // The reverse exploding animation will no longer be playing and the diagram will not be exploded.
                m_Unexploding = false;
                m_Exploded = false;
            }
            // Ensure the left front object exists.
            if (m_LeftFront != null)
            {
                // Linearly interpolate the left front object from its target exploded position to its original position based on the animation time and duration.
                m_LeftFront.transform.localPosition = Vector3.Lerp(m_LeftExplodedPosition, m_LeftOriginalPosition, m_AnimationTime / m_AnimationDuration);
            }
            // Provide an error if the left front object was not found.
            else { Debug.LogError("[" + gameObject.name + "][MultiTargetExplosion]: The Left Front (GameObject) is not set."); }
            // Ensure the right front object exists.
            if (m_RightFront != null)
            {
                // Linearly interpolate the right front object from its target exploded position to its original position based on the animation time and duration.
                m_RightFront.transform.localPosition = Vector3.Lerp(m_RightExplodedPosition, m_RightOriginalPosition, m_AnimationTime / m_AnimationDuration);
            }
            // Provide an error if the right front object was not found.
            else { Debug.LogError("[" + gameObject.name + "][MultiTargetExplosion]: The Right Front (GameObject) is not set."); }
        }
    }
}
