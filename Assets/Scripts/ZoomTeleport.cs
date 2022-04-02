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

// Class for enabling transitions by physically zooming in.
public class ZoomTeleport : MonoBehaviour
{
    // The AR camera used for the scene.
    [SerializeField]
    [Tooltip("The AR camera used for the scene.")]
    private Camera m_ARCamera;
    public Camera ARCamera { get { return m_ARCamera; } set { m_ARCamera = value; } }

    // The World-in-Miniature (WIM) initially seen in AR.
    [SerializeField]
    [Tooltip("The World-in-Miniature (WIM) initially seen in AR.")]
    private Transform m_WIM;
    public Transform WIM { get { return m_WIM; } set { m_WIM = value; } }

    // The distance threshold for activating the zoom.
    [SerializeField]
    [Tooltip("The distance threshold for activating the zoom.")]
    private float m_Distance = 0.15f;
    public float Distance { get { return m_Distance; } set { m_Distance = value; } }

    // The duration used for the zoom transition.
    [SerializeField]
    [Tooltip("The duration used for the zoom transition.")]
    private float m_Duration = 2.0f;
    public float Duration { get { return m_Duration; } set { m_Duration = value; } }

    // The scale factor to use for the zoom transition.
    [SerializeField]
    [Tooltip("The scale factor to use for the zoom transition.")]
    private float m_ZoomScale = 1000.0f;
    public float ZoomScale { get { return m_ZoomScale; } set { m_ZoomScale = value; } }

    // Whether currently zooming.
    private bool m_Zooming = false;
    // Time lapsed for current zoom.
    private float m_ZoomTime = 0.0f;
    // The original scale of the WIM.
    private Vector3 m_OriginalScale;
    // The current scale factor.
    private float m_ScaleFactor;

    // Reset function for initializing the class.
    void Reset()
    {
        // Set the main camera as the AR camera.
        m_ARCamera = Camera.main;
        // Provide a warning if a main camera was not found.
        if (m_ARCamera == null) { Debug.LogWarning("[" + gameObject.name + "][ZoomTeleport]: Did not find a main camera to use as the AR Camera."); }
    }

    // Update is called once per frame.
    void Update()
    {
        // If the class if properly configured.
        if (m_ARCamera != null & m_WIM != null)
        {
            // Determine whether the WIM is within the viewpoint teleportation threshold. 
            if (Vector3.Distance(m_ARCamera.transform.position, m_WIM.position) < m_Distance && !m_Zooming)
            {
                // Start zooming.
                m_Zooming = true;
                m_ZoomTime = 0.0f;

                // Track the original scale.
                m_OriginalScale = m_WIM.localScale;

                // Set the current scale factor and velocity.
                m_ScaleFactor = 0.0f;
            }
            // If zooming
            else if (m_Zooming)
            {
                // Track amount of time spent zooming.
                m_ZoomTime += Time.deltaTime;

                // Determine if the duration has been passed.
                if (m_ZoomTime > m_Duration)
                {
                    m_ZoomTime = m_Duration;
                }

                // Scale the WIM to create the zoom effect.
                m_ScaleFactor = Mathf.Lerp(0.0f, m_ZoomScale, m_ZoomTime / (m_Duration * (m_Duration - m_ZoomTime + 1.0f) * (m_Duration - m_ZoomTime + 1.0f)));

                // Calculate the new local scale.
                Vector3 localScale = m_OriginalScale * m_ScaleFactor;
                // Set the new local scale.
                m_WIM.localScale = localScale;
            }
        }
        // Provide an error if the AR camera was not found.
        if (m_ARCamera == null) { Debug.LogError("[" + gameObject.name + "][ZoomTeleport]: Missing the AR Camera."); }
        // Provide an error if the WIM was not found.
        if (m_WIM == null) { Debug.LogError("[" + gameObject.name + "][ZoomTeleport]: Missing the WIM."); }
    }
}
