using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectController : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arObject;

    private float scale = 0.1f;
    private float angle = 0.0f;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    public void UpdateScale(float sliderValue)
    {
        scale = sliderValue;

        if (arObject)
        {
            arObject.transform.localScale = Vector3.one * scale;
        }        
    }

    public void UpdateRotation(float sliderValue)
    {
        angle = sliderValue;

        if (arObject)
        {
            // arObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            arObject.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        var touchPos = Input.GetTouch(0).position;
        if (!IsPointOverUIObject(touchPos) && arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (!arObject)
            {
                arObject = Instantiate(arRaycastManager.raycastPrefab, hitPose.position, hitPose.rotation);

                arObject.transform.localScale = Vector3.one * scale;
                arObject.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
            else
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }            
        }
    }

    private bool IsPointOverUIObject(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
