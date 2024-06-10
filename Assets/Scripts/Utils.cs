using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Utils
{
    public static IEnumerator DownloadImage(string url, Action<Texture2D> onSuccess, Action<string> onError, int attempts = 3)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        int currentAttempt = 0;
        while (currentAttempt < attempts)
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                onError.Invoke($"Error while load image: {url}. Error message: {request.error}");
                currentAttempt++;
            } else
            {
                Texture2D resultTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                onSuccess.Invoke(resultTexture);
                break;
            }
        }
    }
}
