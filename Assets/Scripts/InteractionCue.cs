/*
*   Copyright (C) 2020 University of Central Florida, created by Dr. Ryan P. McMahan.
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

public class InteractionCue : MonoBehaviour
{
    // The GameObject that will serve as the interaction cue. Should initially be disabled. Should not have any physics-related components.
    [SerializeField]
    [Tooltip("The GameObject that will serve as the interaction cue. Should initially be disabled. Should not have any physics-related components.")]
    GameObject m_Cue;
    public GameObject Cue { get { return m_Cue; } set { m_Cue = value; } }

    // The Transform that serves as the starting position and rotation for the interaction cue.
    [SerializeField]
    [Tooltip("The Transform that serves as the starting position and rotation for the interaction cue.")]
    Transform m_StartingTransform;
    public Transform StartingTransform {  get { return m_StartingTransform; } set { m_StartingTransform = value; } }

    // The Transform that serves as the ending position and rotation for the interaction cue.
    [SerializeField]
    [Tooltip("The Transform that serves as the ending position and rotation for the interaction cue.")]
    Transform m_EndingTransform;
    public Transform EndingTransform { get { return m_EndingTransform; } set { m_EndingTransform = value; } }

    // The seconds that the interaction cue takes to move from the starting transform to the ending transform.
    [SerializeField]
    [Tooltip("The seconds that the interaction cue takes to move from the starting transform to the ending transform.")]
    float m_Duration = 2.0f;
    public float Duration { get { return m_Duration; } set { m_Duration = value; } }

    // Whether the interaction cue is currently active.
    [SerializeField]
    [Tooltip("Whether the interaction cue is currently active.")]
    bool m_Active = false;
    public bool Active { get { return m_Active; } set { m_Active = value; } }

    // How many seconds have passed during the current interaction cue transition.
    private float m_Time = 0.0f;





    // Start is called before the first frame update
    void Start()
    {
        // Ensure the interaction cue GameObject is disabled.
        m_Cue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If the interaction cue is active.
        if (m_Active)
        {
            // Ensure the interaction cue GameObject is active.
            m_Cue.SetActive(true);

            // Calculate and update the position of the interaction cue.
            Vector3 position = Vector3.Lerp(m_StartingTransform.position, m_EndingTransform.position, m_Time/m_Duration);
            m_Cue.transform.position = position;

            // Calculate and update the rotation of the interaction cue.
            Quaternion rotation = Quaternion.Slerp(m_StartingTransform.rotation, m_EndingTransform.rotation, m_Time/m_Duration);
            m_Cue.transform.rotation = rotation;

            // Update the amount of time that has passed.
            m_Time += Time.deltaTime;

            // Reset the time by the duration.
            m_Time %= m_Duration;
        }
        // If the interaction cue is not active.
        else
        {
            // Ensure the interaction cue GameObject is disabled.
            m_Cue.SetActive(false);

            // Reset the time.
            m_Time = 0.0f;
        }
    }
}
