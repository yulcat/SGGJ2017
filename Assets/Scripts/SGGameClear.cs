using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SGGameClear : MonoBehaviour {

	public GameObject GameClearText;
	public GameObject TimeText;
	public Text TimeTextUI;
	public GameObject HPText;
	public Text HPTextUI;
	public GameObject ScoreText;
	public Text ScoreTextUI;

	public Text GamestageText;

	public GameObject nextbtn;

	// Use this for initialization

	public int stagenumber = SGGameManager.Instance.CurrentStageNum;

	void Start () {
		GameClearText.SetActive(false);
		TimeText.SetActive(false);
		HPText.SetActive(false);
		ScoreText.SetActive(false);
		nextbtn.SetActive(false);
		texts();
		StartCoroutine(Clear());

	
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GamestagetextUI(int stagenumber){
		GamestageText.text = SGGameStart.Instance.DescriptionText.text;
	}

	IEnumerator Clear(){
		SGSoundManager.Instance.PlaySounds(7);
		yield return new WaitForSeconds(0.7f);
		GameClearText.SetActive(true);
		SGSoundManager.Instance.PlaySounds(7);
		yield return new WaitForSeconds(0.7f);
		TimeText.SetActive(true);
		SGSoundManager.Instance.PlaySounds(7);
		yield return new WaitForSeconds(0.7f);
		HPText.SetActive(true);
		SGSoundManager.Instance.PlaySounds(7);
		yield return new WaitForSeconds(0.7f);
		SGSoundManager.Instance.PlaySounds(7);
		ScoreText.SetActive(true); // 여기서 최종 점수를 서버로 보낼 것 
		yield return new WaitForSeconds(0.7f);
		if (stagenumber != 5){ 
			SGSoundManager.Instance.PlaySounds(7);
			nextbtn.SetActive(true);
		else
			SceneManager.LoadScene("ending");// 스테이지 5를 깨면 엔딩씬으로 보내자 
		}
	}

	public void nextscenebtn(){

	 SceneManager.LoadScene(stagenumber+1);

	}

	public void texts(){

		TimeTextUI.text = "남은 시간 : "  ; //남은 시간 추가할 것 
		HPTextUI.text = "남은 HP :"; // 남은 hp 추가 할것 
		ScoreTextUI.text = "점수 : "; // 점수 계산해서 보여줄 것 

	}

	
}
