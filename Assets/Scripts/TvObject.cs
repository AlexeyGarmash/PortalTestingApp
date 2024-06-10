using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace PortalTest.Objects
{
    public class TvObject : InteractableObject
    {
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private GameObject m_tvScreen;

        private bool isTvOn;

        protected override void DoInteract()
        {
            base.DoInteract();
            m_tvScreen.SetActive(!isTvOn);
            if (isTvOn)
            {
                m_videoPlayer.Stop();
            }
            else
            {
                m_videoPlayer.Play();
            }
            isTvOn = !isTvOn;
        }
    }
}