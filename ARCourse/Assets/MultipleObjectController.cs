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

    private GameObject selectedItem;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        this.selectedItem = arRaycastManager.raycastPrefab;
    }

    public void SetSelectedItem(GameObject selectedItem)
    {
        Debug.Log("selectedItem: " + selectedItem.name.ToString());
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

        if (Input.GetTouch(0).phase == TouchPhase.Began && arRaycastManager.Raycast(touchPoint, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(selectedItem, hitPose.position, hitPose.rotation);
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
