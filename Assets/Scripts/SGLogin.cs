using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using LitJson;

public class SGLogin : MonoBehaviour {
    public Text nicknameText;
    public static string nickname = "";

    public GameObject popup;

    public void OnclickLogin()
    {
        SGGameData.Instance.GameNickname = "";
        SGGameData.Instance.GameScore = 0;

        var httpClient = SGHttpClient.Instance;

        ReqLogin req = new ReqLogin();
        req.nickname = nicknameText.text;
        SGGameData.Instance.GameNickname = req.nickname;

        string stringJson = JsonMapper.ToJson(req);

        httpClient.PostJson(stringJson, ResponseCallback);
    }

    void ResponseCallback(long resCode, UnityWebRequest response)
    {
        string data = response.downloadHandler.text;
        ResLogin res = JsonUtility.FromJson<ResLogin>(data);

        if (res.resultCode == "2")
        {
            popup.SetActive(true);
            // 아이디 확인 팝업
            //SceneManager.LoadScene("stage1");
        }
        else
        {
            SGGameData.Instance.GameNickname = res.nickname;
            nickname = res.nickname;

            if (SGGameData.Instance.inifinityMode)
            {
                SceneManager.LoadScene("stage4");
            }
            else
            {
                SceneManager.LoadScene("stage1");
            }

                
        }
    }
}

[Serializable]
public class ReqLogin
{
    public string protocol = "ReqLogin";
    public string nickname = "";
}

[Serializable]
public class ResLogin
{
    public string protocol;
    public string resultCode;
    public string message;
    public string nickname;
}


