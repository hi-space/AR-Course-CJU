using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleObjectController : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager arRaycastManager;

    [SerializeField]
    Camera arCamera;

    private GameObject selectedItem;
    static List<ARRaycastHit> arHits = new List<ARRaycastHit>();
    RaycastHit hitObj;

    ARObject selectedObject;
    bool isTouching;

    private void Awake()
    {
        this.selectedItem = arRaycastManager.raycastPrefab;
    }

    public void SetSelectedItem(GameObject selectedItem)
    {
        this.selectedItem = selectedItem;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touchPoint = Input.GetTouch(0).position;
        if (IsPointOverUIObject(touchPoint))
            return;

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out hitObj))
            {
                selectedObject = hitObj.transform.GetComponent<ARObject>();
                if (selectedObject)
                {
                    ARObject[] objects = FindObjectsOfType<ARObject>();
                    foreach (ARObject obj in objects)
                    {
                        obj.IsSelected = (obj == selectedObject);
                    }
                }
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            selectedObject.IsSelected = false;
        }

        if (arRaycastManager.Raycast(touchPoint, arHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = arHits[0].pose;
            if (!selectedObject)
            {
                var obj = Instantiate(selectedItem, hitPose.position, hitPose.rotation);
                selectedObject = obj.AddComponent<ARObject>() as ARObject;
            }
            else if (selectedObject.IsSelected)
            {
                selectedObject.transform.position = hitPose.position;
                selectedObject.transform.rotation = hitPose.rotation;
            }
        }
    }

    bool IsPointOverUIObject(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
