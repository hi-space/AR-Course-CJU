using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class _MultipleObjectController : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager arRaycastManager;

    [SerializeField]
    Camera arCamera;

    _ARObject selectedObject;
    GameObject selectedItem;

    private static List<ARRaycastHit> arHits = new List<ARRaycastHit>();
    private static RaycastHit physicsHit;

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

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = touch.position;

        if (IsPointOverUIObject(touchPosition))
            return;


        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out physicsHit))
            {
                selectedObject = physicsHit.transform.GetComponent<_ARObject>();
                if (selectedObject)
                {
                    _ARObject[] objects = FindObjectsOfType<_ARObject>();
                    foreach (_ARObject obj in objects)
                    {
                        obj.Selected = (obj == selectedObject);
                    }
                }
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            selectedObject.Selected = false;
        }

        if (arRaycastManager.Raycast(touchPosition, arHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = arHits[0].pose;
            if (!selectedObject)
            {
                var obj = Instantiate(selectedItem, hitPose.position, hitPose.rotation);
                selectedObject = obj.AddComponent<_ARObject>() as _ARObject;
            }
            else if (selectedObject.Selected)
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
