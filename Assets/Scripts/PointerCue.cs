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

// Class for pointing a local object at a target object.
public class PointerCue : MonoBehaviour
{
    // The GameObject pointed at.
    [SerializeField]
    [Tooltip("The GameObject pointed at.")]
    private GameObject m_Target;
    new public GameObject Target { get { return m_Target; } set { m_Target = value; } }

    // Update is called once per frame.
    void Update()
    {
        // If the PointerCue is properly configured.
        if (m_Target != null)
        {
            // Point the local object at the target.
            transform.LookAt(m_Target.transform.position, (m_Target.transform.rotation * Vector3.up));
        }
        // Provide an error if the target was not found.
        if (m_Target == null) { Debug.LogError("[" + gameObject.name + "][PointerCue]: Missing the Target."); }
    }
}
