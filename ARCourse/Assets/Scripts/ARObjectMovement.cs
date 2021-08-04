﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectMovement : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arObject;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (!arObject)
            {
                arObject = Instantiate(arRaycastManager.raycastPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}