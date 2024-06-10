using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public static DebugText Instance;

    [SerializeField] private TMP_Text m_textDebug;


    private void Awake()
    {
        Instance = this;
    }

    public void SetDebugText(string text)
    {
        m_textDebug.text = text;
    }
}
