using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceSwitcher : MonoBehaviour
{
    private ARFaceManager arFaceManager;
    private Material currentMaterial;

    private void Awake()
    {
        arFaceManager = GetComponent<ARFaceManager>();
        currentMaterial = arFaceManager.facePrefab.GetComponent<MeshRenderer>().material;
    }
    
    public void UpdateFaceMaterial(Material material)
    {
        currentMaterial = material;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ARFace face in arFaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = currentMaterial;
        }
    }
}
