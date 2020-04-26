using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{

    MeshRenderer Renderer;

    public float MaxOutlineWidth;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<MeshRenderer>();
    }

    public void ShowOutline()
    {
        Renderer.material.SetFloat("_Outline", MaxOutlineWidth);
    }

    public void HideOutline()
    {
        Renderer.material.SetFloat("_Outline", 0f);
    }
}
