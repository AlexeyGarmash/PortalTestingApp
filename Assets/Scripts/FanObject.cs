using DG.Tweening;
using UnityEngine;

namespace PortalTest.Objects
{
    public class FanObject : InteractableObject
    {
        [SerializeField] private Animator m_fanAnimator;


        private bool isEnabled;

        private void Awake()
        {
            m_fanAnimator.enabled = false;
        }

        protected override void DoInteract()
        {
            base.DoInteract();
            m_fanAnimator.enabled = !isEnabled;
            isEnabled = !isEnabled;
        }
    }
}