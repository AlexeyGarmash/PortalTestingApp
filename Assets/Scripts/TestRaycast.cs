using PortalTest.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TestRaycast : MonoBehaviour
{
    [SerializeField] private Transform m_cameraTransform;

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

    private void OnFingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0)
            return;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition), out RaycastHit hit, 5f))
        {
            InteractableObject interactableObject;
            if (hit.collider.gameObject.TryGetComponent<InteractableObject>(out interactableObject))
            {
                interactableObject.Interact();
            }
        }
    }
}
