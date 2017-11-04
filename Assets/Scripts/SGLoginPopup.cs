using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Linq;
using UnityEngine.SceneManagement;

public class SGLoginPopup : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        gameObject.Child("Text").GetComponent<Text>().text = SGGameData.Instance.GameNickname + " 는 이미 사용중인 닉네임 입니다. \n어떻게 사용하시겠습니까?";	
	}

    public void OnClickUseNickName()
    {
        SceneManager.LoadScene("stage1");
    }
}
