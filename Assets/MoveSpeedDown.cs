using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<SGCharacter>().DownMoveSpeed(0.5f);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<SGCharacter>().BackMoveSpeed();
    }
}
