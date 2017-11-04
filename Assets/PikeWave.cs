using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 6);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * -7.0f * Time.deltaTime);
        
    }
}
