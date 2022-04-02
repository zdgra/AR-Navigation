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
using UnityEngine.Events;

// Class for detecting when an object is waved out-and-in of view.
public class WaveDetector : MonoBehaviour
{
    // The event handler for a Vuforia tracked target.
    [SerializeField]
    [Tooltip("The event handler for a Vuforia tracked target.")]
    private DefaultTrackableEventHandler m_EventHandler;
    public DefaultTrackableEventHandler EventHandler { get { return m_EventHandler; } set { m_EventHandler = value; } }

    // The maximum duration in seconds that out-and-in waves are detected.
    [SerializeField]
    [Tooltip("The maximum duration in seconds that out-and-in waves are detected.")]
    private float m_MaxDuration = 2.5f;
    public float MaxDuration { get { return m_MaxDuration; } set { m_MaxDuration = value; } }

    // Add some space to the inspector.
    [Space()]
    // The event called when a wave is detected.
    [Tooltip("The event called when a wave is detected.")]
    public UnityEvent OnWaveDetected;

    // Whether the tracked target is currently found.
    private bool m_TargetFound = false;

    // Whether the tracked target is currently lost.
    private bool m_TargetLost = true;

    // How long the tracked target has been lost.
    private float m_TimeLost = 0.0f;

    // Reset function for setting the WaveDetector defaults.
    void Reset()
    {
        // Attempt to find a local event handler.
        m_EventHandler = GetComponent<DefaultTrackableEventHandler>();
        // Provide a warning if a local event handler was not found.
        if (m_EventHandler == null) { Debug.LogWarning("[" + gameObject.name + "][WaveDetector]: Did not find a local DefaultTrackableEventHandler."); }
    }

    // Handle when the target is found.
    public void TargetFound()
    {
        // Keep track that the target is found.
        m_TargetFound = true;
        // If the target was lost less than the maximum wave duration.
        if (m_TimeLost <= m_MaxDuration)
        {
            // Call all of the OnWaveDetected events.
            OnWaveDetected.Invoke();
        }
        // The target is no longer lost.
        m_TargetLost = false;
        m_TimeLost = 0.0f;
    }

    // Handle when the target is lost.
    public void TargetLost()
    {
        // Keep track that the target is lost.
        m_TargetLost = true;
        // The target is no longer found.
        m_TargetFound = false;
    }

    // Start is called before the first frame update.
    void Start()
    {
        // Ensure the event handler exists.
        if (m_EventHandler != null)
        {
            // Add listeners for OnTargetFound and OnTargetLost.
            m_EventHandler.OnTargetFound.AddListener(delegate { TargetFound(); });
            m_EventHandler.OnTargetLost.AddListener(delegate { TargetLost(); });
        }
        // Provide an error if the event handler was not found.
        else { Debug.LogError("[" + gameObject.name + "][WaveDetector]: The Event Handler (DefaultTrackableEventHandler) is not set."); }
    }

    // Update is called once per frame.
    void Update()
    {
        // If the target is currently lost.
        if (m_TargetLost)
        {
            // Account for how long the target has been lost.
            m_TimeLost += Time.deltaTime;
        }
    }
}
