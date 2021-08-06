using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    public bool IsSelected = false;
    MeshRenderer meshRenderer;
    Material originMaterial;
    Material selectedMaterial;
    Color originColor;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (!meshRenderer)
        {
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        }

        originMaterial = meshRenderer.material;
        originColor = meshRenderer.material.color;
        //selectedMaterial = Resources.Load("Materials/Selected.mat", typeof(Material)) as Material;
    }

    public void Update()
    {
        if (IsSelected)
        {
            //meshRenderer.material = selectedMaterial;
            meshRenderer.material.color = Color.gray;
        }
        else
        {
            meshRenderer.material.color = originColor;
            //meshRenderer.material = originMaterial;
        }
    }

    public void ActiveToggle()
    {
        IsSelected = !IsSelected;

        if (IsSelected)
        {
            Debug.Log("Selected: " + this.name);
            meshRenderer.material.color = Color.gray;
        }
        else
        {
            Debug.Log("Unselected: " + this.name);
        }   
    }

    public void Selected()
    {
        IsSelected = true;
        meshRenderer.material.color = Color.gray;
    }

    public void UnSelected()
    {
        IsSelected = false;
    }
}
