using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletObject;

    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            GameObject obj = Instantiate(bulletObject);
            obj.transform.position = cameraTransform.position;
            obj.GetComponent<Rigidbody>().AddForce(cameraTransform.forward * 100f);
        }
    }
}
