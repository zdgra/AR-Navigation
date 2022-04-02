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

// Class for creating X-Ray cameras.
public class XRayCamera : MonoBehaviour
{
    // Update is called once per frame.
    void Update()
    {
        // Find the main camera.
        Camera mainCamera = Camera.main;
        // Provide a warning if a main camera was not found.
        if (mainCamera == null) { Debug.LogError("[" + gameObject.name + "][XRayCamera]: Did not find a main Camera."); }

        // Find the local X-Ray camera.
        Camera xRayCamera = GetComponent<Camera>();
        // Provide a warning if a local camera was not found.
        if (xRayCamera == null) { Debug.LogError("[" + gameObject.name + "][XRayCamera]: Did not find a local Camera."); }

        // Ensure both cameras are properly set.
        if (mainCamera != null && xRayCamera != null)
        {
            // Copy the field of view from the main camera to the X-Ray camera.
            xRayCamera.fieldOfView = mainCamera.fieldOfView;
        }
    }
}
