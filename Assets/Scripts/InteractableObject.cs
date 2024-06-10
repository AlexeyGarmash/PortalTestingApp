using UnityEngine;

namespace PortalTest.Objects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] private InteractableGuideItem m_guideItem;
        [SerializeField] private IteractableObjectType m_objectType;

        public bool CanInteract { get; set; }
        public void Interact()
        {
            if (CanInteract)
            {
                DoInteract();
            }            
        }

        protected virtual void DoInteract()
        {
            if (m_guideItem != null)
            {
                m_guideItem.gameObject.SetActive(false);
                if (InteractableGuideManager.Instance != null)
                {
                    InteractableGuideManager.Instance.DisableGuides();
                }
            }
            if (InteractableObjectsClickCounter.Instance != null)
            {
                InteractableObjectsClickCounter.Instance.RegisterClickOnObject(m_objectType);
            }
        }
    }
}

