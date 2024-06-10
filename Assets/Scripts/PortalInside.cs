using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInside : MonoBehaviour
{
    [SerializeField] private Portal m_portalWindow;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TRIGGER EXIT INSIDE PORTAL"  + other.gameObject.name);
        if(other.transform == m_portalWindow.CameraTransform)
        {
            m_portalWindow.SetFullRender(false);
            m_portalWindow.InPortalDimension = false;
        }
    }
}
