using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlacePortal : MonoBehaviour
{
    public Action<Portal> ActionPortalPlaced;

    [SerializeField] private Portal m_portalObject;
    [SerializeField] private ARRaycastManager m_arRaycastManager;
    [SerializeField] private ARPlaneManager m_arPlaneManager;
    [SerializeField] private GameObject m_cameraObject;

    private List<ARRaycastHit> arHits = new List<ARRaycastHit>();
    private bool spawnedOnce = false;

    private void Awake()
    {
        if (m_arRaycastManager == null)
        {
            m_arRaycastManager = GetComponent<ARRaycastManager>();
        }
        if(m_arPlaneManager == null)
        {
            m_arPlaneManager = GetComponent<ARPlaneManager>();
        }
    }

    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
    }

    private void OnFingerDown(Finger finger)
    {
        if (finger.index != 0)
            return;

        if(!spawnedOnce && m_arRaycastManager.Raycast(finger.currentTouch.screenPosition, arHits, TrackableType.PlaneWithinPolygon))
        {
            if(arHits != null && arHits.Count > 0)
            {
                ARRaycastHit hit = arHits[0];

                Portal instPortalObj = SpawnPortal(hit);
                DeactivateUselessPlanes(hit);
                ActionPortalPlaced.Invoke(instPortalObj);

                RotatePortalToCamera(hit, instPortalObj);
                
            }
        }
    }

    private void RotatePortalToCamera(ARRaycastHit hit, Portal instPortalObj)
    {
        if (m_arPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp)
        {
            Vector3 spawnedObjPosition = instPortalObj.transform.position;
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 direction = cameraPosition - spawnedObjPosition;
            Vector3 targetRotation = Quaternion.LookRotation(direction).eulerAngles;
            Vector3 scaleTargetRotation = Vector3.Scale(targetRotation, instPortalObj.transform.up.normalized);
            Quaternion targetQuaternion = Quaternion.Euler(scaleTargetRotation);
            instPortalObj.transform.rotation = instPortalObj.transform.rotation * targetQuaternion;

        }
    }

    private Portal SpawnPortal(ARRaycastHit hit)
    {
        Pose pose = hit.pose;
        Portal instPortalObj = Instantiate(m_portalObject, pose.position, Quaternion.identity);
        instPortalObj.CameraTransform = m_cameraObject.transform;
        spawnedOnce = true;
        return instPortalObj;
    }
    private void DeactivateUselessPlanes(ARRaycastHit hit)
    {
        foreach (var plane in m_arPlaneManager.trackables)
        {
            if (plane.trackableId != hit.trackableId)
            {
                plane.gameObject.SetActive(false);
            }
        }
        m_arPlaneManager.enabled = false;
    }
}
