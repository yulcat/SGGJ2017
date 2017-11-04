using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class SGHttpClient : SGSingleton<SGHttpClient>
{
    public const long SUCCESS = 200;
    public const long NOT_FOUND = 404;
    protected const string url = "http://52.79.80.98:5000";
    //protected const string url = "http://127.0.0.1:5000";

    public enum HttpRequestType { GET, POST }

    public delegate void ResponseCallback(long resCode, UnityWebRequest response);

    public void PostJson(string jsonString, ResponseCallback callback)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(WaitRequest(request, callback));
    }

    public static IEnumerator WaitRequest(UnityWebRequest request, ResponseCallback callback)
    {
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.LogError(request.error);
        }

        callback(request.responseCode, request);

        request.Dispose();
    }
}