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
	void OnEnable () {
        SetData("", "0");


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetData(string userName, string userScore)
    {
        usrnametext.text = "유저 닉네임 : " + userName;
        usrscoretext.text = "유저 스코어 : " + userScore;
    }

	public void Tologinbtn(){
		SceneManager.LoadScene("LogIn");


	}
}
