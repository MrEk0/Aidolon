using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class ServerController : MonoBehaviour
{
    //Based on https://www.owasp.org/index.php/Certificate_and_Public_Key_Pinning#.Net
    private class AcceptAllCertificatesSignedWithASpecificKeyPublicKey : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void SendData(string url, Dictionary<string,string> analyticsEvents, Action<string> onComplete, Action<string> onError)
    {
        var body = ServerUtilities.Decoding(JSONSerializeHelper.SerializeToJson(analyticsEvents));

        var headers = new Dictionary<HttpRequestHeader, string>
        {
            { HttpRequestHeader.ContentType, "application/json" },
        };
        
        var routine = Post(url, headers, body, result =>
        {
            if (!JSONSerializeHelper.TryDeserializeFromJson<string>(result, out var response))
            {
                onError($"Can't deserialize {result}");
                return;
            }

            onComplete(response);
        }, onError);

        StartCoroutine(routine);
    }

    private IEnumerator Post(string requestUrl, Dictionary<HttpRequestHeader, string> headers, byte[] body, Action<string> onComplete, Action<string> onError)
    {
        var request = new UnityWebRequest(requestUrl, UnityWebRequest.kHttpVerbPOST);

        request.uploadHandler = body.Length > 0 ? new UploadHandlerRaw(body) : null;
        request.downloadHandler = new DownloadHandlerBuffer();

        foreach (var pair in headers)
            request.SetRequestHeader(pair.Key.ToHeaderString(), pair.Value);

        request.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (!string.IsNullOrEmpty(request.error))
            {
                onError(request.error);
            }
            else
            {
                onComplete(request.downloadHandler.text);
            }
        }
    }
}
