using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGuideManager : MonoBehaviour
{
    public static InteractableGuideManager Instance;

    private const string KEY_INTERACTABLE_PREF = "sh_inter";
    private const int SHOW_INTER_GUIDE = 1;
    private const int HIDE_INTER_GUIDE = 0;

    [SerializeField] private List<InteractableGuideItem> m_guidesList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        CheckGuidesToActivate();
    }

    private void CheckGuidesToActivate()
    {
        int showInteractableGuides = PlayerPrefs.GetInt(KEY_INTERACTABLE_PREF, SHOW_INTER_GUIDE);
        if(showInteractableGuides == HIDE_INTER_GUIDE)
        {
            m_guidesList.ForEach(guide => guide.gameObject.SetActive(false));
        }
    }

    public void DisableGuides()
    {
        PlayerPrefs.SetInt(KEY_INTERACTABLE_PREF, HIDE_INTER_GUIDE);
    }
}
