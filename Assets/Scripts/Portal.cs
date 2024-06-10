using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using PortalTest.Objects;
using System;

public class Portal : MonoBehaviour
{
    public Action PortalEntered { get; set; }

    [SerializeField] private ScriptableRendererFeature m_renderAlwaysFeature;
    [SerializeField] private HidePortalInside m_portalInside;
    [SerializeField] private Transform m_CameraTransform;
    [SerializeField] private Transform m_portalWindowObject;
    [SerializeField] private InteractableObjectsManager m_interactableManager;

    private bool wasInFront;
    private bool inPortalDimension;
    private bool hasCollided;
    private bool portalEnteredFirstTime;

    public Transform CameraTransform
    {
        get => m_CameraTransform;
        set 
        { 
            m_CameraTransform = value;
            m_portalInside.CameraTransform = value;
        }
    }
    
    public bool InPortalDimension
    {
        get => inPortalDimension;
        set
        {
            inPortalDimension = value;
        }
    }

    private void Start()
    {
        SetFullRender(false);
    }

    public void SetFullRender(bool alwaysRender)
    {
        m_renderAlwaysFeature.SetActive(alwaysRender);
        EnableDisableObject(alwaysRender);
        if(!portalEnteredFirstTime && PortalEntered != null && alwaysRender)
        {
            PortalEntered.Invoke();
            portalEnteredFirstTime = true;
        }
    }

    private void EnableDisableObject(bool alwaysRender)
    {
        m_interactableManager.SwitchInteractAbility(alwaysRender);
    }

    private bool GetIsInFront()
    {
        Vector3 cameraPosition = m_CameraTransform.position + m_CameraTransform.forward * Camera.main.nearClipPlane;
        Vector3 inversePos = m_portalWindowObject.InverseTransformPoint(cameraPosition);
        return inversePos.z >= 0.15 ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != m_CameraTransform)
            return;
        wasInFront = GetIsInFront();
        hasCollided = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform != m_CameraTransform)
            return;
        hasCollided = false;
    }

    private void CheckColliding()
    {
        if (!hasCollided)
            return;
        bool isInFront = GetIsInFront();
        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inPortalDimension = !inPortalDimension;
            SetFullRender(inPortalDimension);
        }
        wasInFront = isInFront;
    }

    private void Update()
    {
        CheckColliding();
    }

    private void OnDestroy()
    {
        SetFullRender(true);
    }

}
