using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCTitleScene : MonoBehaviour {

	public GameObject creditui;

	public GameObject starttext;

	// Use this for initialization
	void Start () {

		creditui.SetActive(false);
		StartCoroutine(Blink());
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("main"); // 나중에 씬 이름 변경
		}
		
	}
	 
	public void opencredit(){

		creditui.SetActive(true);
	}

	public void closecredit(){

	 	creditui.SetActive(false);

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
