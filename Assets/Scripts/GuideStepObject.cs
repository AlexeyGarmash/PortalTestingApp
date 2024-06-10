using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

namespace PortalTest.UiObjects
{
    public class GuideStepObject : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_mainCard;
        [SerializeField] private Ease m_easeType;
        [SerializeField] private float m_fadeDuration = 1f;
        [SerializeField] private float m_fadeValue = 0.5f;

        private void Start()
        {
            FadeAnimate();
        }

        private void FadeAnimate()
        {
            m_mainCard.DOFade(m_fadeValue, m_fadeDuration).SetEase(m_easeType).SetLoops(-1, LoopType.Yoyo);
        }
    }
}

