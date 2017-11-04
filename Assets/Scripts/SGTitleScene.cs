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

		creditui.SetActive(false);
		StartCoroutine(Blink());
		
	}
	
    public void GotoLogin()
    {
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
	
}
