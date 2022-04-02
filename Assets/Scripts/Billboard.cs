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

// Class for creating local billboards (i.e., objects that always face the camera).
public class Billboard : MonoBehaviour
{
    // The Camera used for the billboard.
    [SerializeField]
    [Tooltip("The camera used for the billboard.")]
    private Camera m_camera;
    new public Camera camera { get { return m_camera; } set { m_camera = value; } }

    // Reset function for initializing the Billboard.
    void Reset()
    {
        // Set the camera to the main camera.
        camera = Camera.main;
        // Provide a warning if a main camera was not found.
        if (camera == null) { Debug.LogWarning("[" + gameObject.name + "][Billboard]: Did not find a main Camera."); }
    }

    // Start is called before the first frame update.
    void Start()
    {
        // If the camera is not set.
        if (camera == null)
        {
            // Set the camera to the main camera.
            camera = Camera.main;
            // Provide an error if a main camera was not found.
            if (camera == null) { Debug.LogError("[" + gameObject.name + "][Billboard]: Did not find a main Camera."); }
            // Provide a warning that the camera was set to the main camera.
            else { Debug.LogWarning("[" + gameObject.name + "][Billboard]: Set camera to main Camera."); }
        }
    }

    // Update is called once per frame.
    void Update()
    {
        // If the Billboard is properly configured.
        if (camera != null)
        {
            // Billboard the local object.
            transform.LookAt(transform.position + (camera.transform.rotation * Vector3.forward), (camera.transform.rotation * Vector3.up));
        }
        // Provide an error if the camera was not found.
        if (camera == null) { Debug.LogError("[" + gameObject.name + "][Billboard]: Missing the Camera."); }
    }
}
