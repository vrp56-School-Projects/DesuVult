using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour, IOnLook
{

    [SerializeField] private MeshRenderer Renderer;
    [SerializeField] private SkinnedMeshRenderer EnemyRenderer;
    [SerializeField] private bool PlayerLooking = false;

    public float MaxOutlineWidth = 1f;
    public Color color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        if (Renderer == null)
            Renderer = GetComponent<MeshRenderer>();

        if (Renderer == null)
            EnemyRenderer = GetComponent<SkinnedMeshRenderer>();


    }

    void Update()
    {
        if (PlayerLooking)
        {
            ShowOutline();
        }
        else
        {
            HideOutline();
        }
        PlayerLooking = false;
    }

    public void ShowOutline()
    {
        if (Renderer != null)
        {
            Renderer.material.SetFloat("_Outline", MaxOutlineWidth);
            Renderer.material.SetColor("_OutlineColor", color);
        }
        if (EnemyRenderer != null)
        {
            EnemyRenderer.material.SetFloat("_Outline", MaxOutlineWidth);
            EnemyRenderer.material.SetColor("_OutlineColor", color);
        }
        
    }

    public void HideOutline()
    {
        if (Renderer != null)
        {
            Renderer.material.SetFloat("_Outline", 0f);
            Renderer.material.SetColor("_OutlineColor", Color.black);
        }
        if (EnemyRenderer != null)
        {
            EnemyRenderer.material.SetFloat("_Outline", 0f);
            EnemyRenderer.material.SetColor("_OutlineColor", Color.black);
        }

    }

    public void OnLook()
    {
        PlayerLooking = true;
    }
}
