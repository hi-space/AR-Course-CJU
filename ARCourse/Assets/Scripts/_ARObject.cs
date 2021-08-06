using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ARObject : MonoBehaviour
{
    private bool IsSelected = false;
    MeshRenderer meshRenderer;
    Color originColor;

    public bool Selected
    {
        get
        {
            return this.IsSelected;
        }

        set
        {
            IsSelected = value;
            UpdateMaterialColor(IsSelected);
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        }

        originColor = meshRenderer.material.color;
    }
    
    private void UpdateMaterialColor(bool IsSelected)
    {
        if (IsSelected)
        {
            meshRenderer.material.color = Color.gray;
        }
        else
        {
            meshRenderer.material.color = originColor;
        }
    }
}
