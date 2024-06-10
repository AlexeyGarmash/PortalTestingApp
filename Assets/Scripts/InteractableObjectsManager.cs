using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalTest.Objects
{
    public class InteractableObjectsManager : MonoBehaviour
    {
        [SerializeField] private List<InteractableObject> m_objectsToInteract;

        public void SwitchInteractAbility(bool canInteract)
        {
            m_objectsToInteract.ForEach(obj => obj.CanInteract = canInteract);
        }
    }
}

