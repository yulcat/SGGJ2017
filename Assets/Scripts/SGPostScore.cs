using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.Networking;

public class SGPostScore : SGSingleton<SGPostScore> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PostScore()
    {
        var httpClient = SGHttpClient.Instance;

        ReqStageResult req = new ReqStageResult();
        req.nickname = SGGameData.Instance.GameNickname;
        req.score = SGGameData.Instance.GameScore.ToString();

        string stringJson = JsonMapper.ToJson(req);

        httpClient.PostJson(stringJson, ResponseCallback);
    }

    public void ResponseCallback(long resCode, UnityWebRequest response)
    {
        string data = response.downloadHandler.text;
        ResStageResult res = JsonUtility.FromJson<ResStageResult>(data);

        Debug.Log("점수를 업데이트 했습니다.");
    }
}



    [Serializable]
    public class ReqStageResult
    {
        public string protocol = "ReqStageResult";
        public string nickname = "";
        public string score = "";
    }

    [Serializable]
    public class ResStageResult
    {
        public string protocol;
        public string resultCode;
        public string message;
        public int score;
        public int isBestScore;
        public int bestScore;
    }
