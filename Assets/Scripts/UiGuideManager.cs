using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;

namespace PortalTest.UiObjects
{
    public class UiGuideManager : MonoBehaviour
    {
        private const string KEY_UI_GUIDE_PREF = "sh_ui_g";
        private const int SHOW_GUIDE = 1;
        private const int HIDE_GUIDE = 0;

        [SerializeField] private List<GuideStepObject> m_stepsList;
        [SerializeField] private ARPlaneManager m_arPlaneManager;
        [SerializeField] private PlacePortal m_placePortal;

        private int currentStepIndex = 0;
        private Portal portalInstance;

        private void Awake()
        {
            HideAllSteps(); 
            if(CheckGuidesToActivate())
            {
                EnableGuideStep();
                m_arPlaneManager.planesChanged += OnPlanesChanged;
                m_placePortal.ActionPortalPlaced += OnPortalPlaced;
            }
        }

        private void OnPortalPlaced(Portal portal)
        {
            portalInstance = portal;
            NextStep();
            portalInstance.PortalEntered += OnPortalEntered;
        }

        private void OnPortalEntered()
        {
            NextStep();
            DisableGuides();
            portalInstance.PortalEntered -= OnPortalEntered;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            NextStep();
            m_arPlaneManager.planesChanged -= OnPlanesChanged;
        }

        private void NextStep()
        {
            currentStepIndex++;
            EnableGuideStep();
        }
        private void EnableGuideStep()
        {
            
            for (int i = 0; i < m_stepsList.Count; i++)
            {
                if(i != currentStepIndex)
                {
                    m_stepsList[i].gameObject.SetActive(false);
                }
            }
            if (currentStepIndex >= m_stepsList.Count)
            {
                currentStepIndex = m_stepsList.Count - 1;
                m_stepsList[currentStepIndex].gameObject.SetActive(false);
            }
            else
            {
                m_stepsList[currentStepIndex].gameObject.SetActive(true);
            }
        }

        private void HideAllSteps()
        {
            m_stepsList.ForEach(step => step.gameObject.SetActive(false));
        }

        private void DisableGuides()
        {
            PlayerPrefs.SetInt(KEY_UI_GUIDE_PREF, HIDE_GUIDE);
        }

        private bool CheckGuidesToActivate()
        {
            int showGuides = PlayerPrefs.GetInt(KEY_UI_GUIDE_PREF, SHOW_GUIDE);
            return showGuides == SHOW_GUIDE ? true : false;
        }

    }
}

