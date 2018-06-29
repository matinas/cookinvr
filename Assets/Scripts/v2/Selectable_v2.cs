using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Selectable_v2 : MonoBehaviour {

    public Material selectedMat;
    
    private List<Material> originalMats;

    private MeshRenderer[] renderers;

    void Awake()
    {
        originalMats = new List<Material>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in renderers)
            originalMats.Add(mr.material);
    }

    void OnDestroy()
    {
        originalMats = null;
    }

    void OnHandHoverBegin(Hand h)
    {
        Debug.Log("Hand hover begin!");
        
        if (gameObject.transform.parent != h.transform)
        {
            for(int i=0; i<renderers.Length; i++)
            {
                MeshRenderer mr = renderers[i];
                mr.material = selectedMat;
            }
        }
    }

    void OnHandHoverEnd(Hand h)
    {
        Debug.Log("Hand hover end!");

        for(int i=0; i<renderers.Length; i++)
        {
            MeshRenderer mr = renderers[i];
            mr.material = originalMats[i];
        }
    }
}