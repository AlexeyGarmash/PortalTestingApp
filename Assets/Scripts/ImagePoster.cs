using UnityEngine;

public class ImagePoster : MonoBehaviour
{
    [SerializeField] private string m_imageUrl;
    [SerializeField] private Renderer m_imageRenderer;

    public void LoadImage()
    {
        StartCoroutine(Utils.DownloadImage(m_imageUrl, OnSuccessLoadImage, OnError));
    }

    private void OnError(string errorMessage)
    {
        
    }

    private void OnSuccessLoadImage(Texture2D texture)
    {
        m_imageRenderer.material.SetTexture("_BaseMap", texture);
    }
}
