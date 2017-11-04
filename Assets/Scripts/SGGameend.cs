using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SGGameend : SGSingleton<SGGameend> {

	[SerializeField]
	public int userscore; //추후에
	[SerializeField]
	public string username; //추후에
	public Text usrscoretext;
	public Text usrnametext;

	// Use this for initialization
	void Start () {
		usrnametext.text = "유저 닉네임 : " + username;
		usrscoretext.text = "유저 스코어 : " + userscore;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Tologinbtn(){
		SceneManager.LoadScene("login");


	}
}
