using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGCameraTarget : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.position = SGGameManager.Instance.hero.transform.position;
	}
}
