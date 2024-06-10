using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePortalInside : MonoBehaviour
{
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField] private Transform m_portalSurfaceTransform;
    [SerializeField] private Renderer m_portalRenderer;

    [SerializeField] private Material m_portalTransparentMaterial;
    [SerializeField] private Material m_portalOpaqueMaterial;

    [SerializeField] private bool isPortalOn = true;
    [SerializeField] private float m_distanceToHide = 6f;

    public Transform CameraTransform
    {
        get => m_cameraTransform;
        set { m_cameraTransform = value; }
    }

    // Update is called once per frame
    void Update()
    {
        var portalAngle = new Vector3(m_portalSurfaceTransform.forward.x, 0.0f, m_portalSurfaceTransform.forward.z);
        var cameraAngle = new Vector3(m_cameraTransform.forward.x, 0.0f, m_cameraTransform.forward.z);
        var horizDiffAngle = Vector3.Angle(portalAngle, cameraAngle);
        float distance = Vector3.Distance(m_cameraTransform.position, m_portalSurfaceTransform.position);
        
        if ((distance < m_distanceToHide && (horizDiffAngle < 145 && horizDiffAngle > 100)) || (distance < m_distanceToHide && (horizDiffAngle < 90 && horizDiffAngle > 45)))
        {
            Debug.Log("Portal is OPAQUE");
            if (isPortalOn)
            {
                isPortalOn = false;
                m_portalRenderer.material = m_portalOpaqueMaterial;
            }

        }
        else
        {
            Debug.Log("Portal is TRANSPARENT");
            if (!isPortalOn)
            {
                isPortalOn = true;
                m_portalRenderer.material = m_portalTransparentMaterial;
            }
        }
    }
}
