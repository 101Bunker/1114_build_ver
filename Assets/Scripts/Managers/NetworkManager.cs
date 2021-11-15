using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public Image img;

    IEnumerator GetRequest(string URL, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    public void GetPosts()
    {
        StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/posts", (UnityWebRequest req) =>
         {
             if (req.isNetworkError || req.isHttpError)
                 Debug.Log($"{req.error} : {req.downloadHandler.text}");
             else
                 Debug.Log(req.downloadHandler.text);
         }));
    }

    public void DownLoadImage(string URL)
    {
        StartCoroutine(ImageRequest(URL, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError) Debug.Log($"{req.error} : {req.downloadHandler.text}");
            else
            {
                // Get the texture out using a helper downloadhandler
                Texture2D texture = DownloadHandlerTexture.GetContent(req);
                // save it into the Image UI's sprite
                img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }));
    }

    IEnumerator ImageRequest(string URL, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(URL))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }
}