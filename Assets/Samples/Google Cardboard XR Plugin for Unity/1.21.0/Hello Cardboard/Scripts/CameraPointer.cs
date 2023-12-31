//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    [SerializeField] private GameObject _pointer = null;
    [SerializeField] private float _maxDistancePointer = 10.0f;


    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    private readonly string interactableTag = "Interactable";
    private float gazePointerScaleSize = 0.025f;

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += Instance_OnGazeSelection;
    }

    private void Instance_OnGazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);

        // Sumar puntos del PuntajeScript
        PuntajeScript puntajeScript = gameObject.GetComponent<PuntajeScript>();
        if(puntajeScript != null)
        {
            puntajeScript.SumarPuntaje(1);
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);
                GazeManager.Instance.StartGazeSelection();
            }

            if(hit.transform.gameObject.CompareTag(interactableTag))
            {
                PointerOnGaze(hit.point);
            } else
            {
                PointerOutGaze();
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }

    private void PointerOnGaze(Vector3 hitPoint)
    {
        float scaleFactor = gazePointerScaleSize * Vector3.Distance(hitPoint, transform.position);
        _pointer.transform.localScale = Vector3.one * scaleFactor;

        _pointer.transform.parent.position = hitPoint;
    }

    private void PointerOutGaze()
    {
        _pointer.transform.localScale = Vector3.one * 0.1f;
        _pointer.transform.parent.transform.localPosition = new Vector3(0,0, _maxDistancePointer);
        _pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeManager.Instance.CancelGazeSelection();
    }
}