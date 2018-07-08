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

    private Coroutine disableHandCrt;

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
        if (gameObject.transform.parent != h.transform)
        {
            for(int i=0; i<renderers.Length; i++)
            {
                MeshRenderer mr = renderers[i];
                mr.material = selectedMat;
            }
        }

        h.GetComponentInChildren<HandAnimation_v2>().GrabLargePose();
    }

    void OnHandHoverEnd(Hand h)
    {
        for(int i=0; i<renderers.Length; i++)
        {
            MeshRenderer mr = renderers[i];
            mr.material = originalMats[i];
        }

        if (!h.GetStandardInteractionButtonDown())
            h.GetComponentInChildren<HandAnimation_v2>().NaturalPose();
    }

    void OnAttachedToHand(Hand h) 
    {
        h.GetComponentInChildren<HandAnimation_v2>().GrabSmallPose();

        disableHandCrt = StartCoroutine(DisableHand(h));
    }

    void OnDetachedFromHand(Hand h)
    {
        HandAnimation_v2 handAnim = h.GetComponentInChildren<HandAnimation_v2>();
        Renderer[] renderers = handAnim.gameObject.GetComponentsInChildren<Renderer>();
        
        if (disableHandCrt != null)
            StopCoroutine(disableHandCrt);

        foreach (Renderer r in renderers)
            r.enabled = true;

        handAnim.NaturalPose();
    }

    IEnumerator DisableHand(Hand h)
    {
        yield return new WaitForSeconds(0.5f);

        HandAnimation_v2 handAnim = h.GetComponentInChildren<HandAnimation_v2>();
        foreach (Renderer r in handAnim.gameObject.GetComponentsInChildren<Renderer>())
            r.enabled = false;
    }
}