using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SGTitleScene : MonoBehaviour {

	public GameObject creditui;

	public GameObject starttext;

	// Use this for initialization
	void Start () {

        SGGameData.Instance.GameNickname = "";
        SGGameData.Instance.GameScore = 0;
        creditui.SetActive(false);
		StartCoroutine(Blink());
		
	}
	
    public void GotoLogin()
    {
        SGGameData.Instance.inifinityMode = false;
        SceneManager.LoadScene("LogIn");
    }
	 
	public void opencredit(){

		creditui.SetActive(true);
		SGSoundManager.Instance.PlayButtonSound();
	}

	public void closecredit(){

	 	creditui.SetActive(false);
		SGSoundManager.Instance.PlayButtonSound();

	}

	IEnumerator Blink(){
		while(true)
			{
				starttext.SetActive(true);
				yield return new WaitForSeconds(0.5f);
				starttext.SetActive(false);
				yield return new WaitForSeconds(0.5f);

			}
	}	
	public void Toinfinity(){
        SGGameData.Instance.inifinityMode = true;
		SceneManager.LoadScene("LogIn");
	}
}
