using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    private bool IsSelected;
    private MeshRenderer meshRender;
    private Color originColor;

    public bool Selected
    {
        get
        {
            return this.IsSelected;
        }
        set
        {
            IsSelected = value;
            UpdateMaterialColor();
        }
    }

    private void Awake()
    {
        meshRender = GetComponent<MeshRenderer>();
        if (!meshRender)
        {
            meshRender = this.gameObject.AddComponent<MeshRenderer>();
        }

        originColor = meshRender.material.color;
    }
    
    private void UpdateMaterialColor()
    {
        if (IsSelected)
        {
            meshRender.material.color = Color.gray;
        }
        else
        {
            meshRender.material.color = originColor;
        }
    }
}
