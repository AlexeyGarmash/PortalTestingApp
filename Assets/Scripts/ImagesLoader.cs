using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesLoader : MonoBehaviour
{
    [SerializeField] private List<ImagePoster> m_imagesUrl;


    private void Start()
    {
        LoadImages();
    }

    private void LoadImages()
    {
        m_imagesUrl.ForEach(action => action.LoadImage());
    }
}
