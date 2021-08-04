using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectController : MonoBehaviour
{
    [SerializeField]
    private Slider scaleSlider;

    [SerializeField]
    private Slider rotationSlider;

    private float min = .1f;
    private float max = 10f;

    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arObject;
    private float scale;
    private float angle;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

        scaleSlider.onValueChanged.AddListener(OnScaleSliderValueChanged);
        rotationSlider.onValueChanged.AddListener(OnRotationSliderValueChanged);
    }
    
    private void OnScaleSliderValueChanged(float value)
    {
        scale = value * (1f - 0.01f) + 0.01f;
        arObject.transform.localScale = Vector3.one * scale;

        Debug.Log("Scale: " + arObject.transform.localScale.ToString());
    }
    
    private void OnRotationSliderValueChanged(float value)
    {
        angle = value * (max - min) + min;
        arObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Debug.Log("Rotate: " + arObject.transform.rotation.eulerAngles.y.ToString());
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
                Debug.Log("init Scale: " + arObject.transform.localScale.ToString());
                Debug.Log("init Rotate: " + arObject.transform.rotation.eulerAngles.y.ToString());
            }
            else
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
