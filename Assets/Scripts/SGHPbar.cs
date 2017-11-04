using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Linq;

public class SGHPbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Slider>().maxValue = SGGameManager.Instance.hero.maxHP;


    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Slider>().value = SGGameManager.Instance.hero.GetCurrentHP;
        gameObject.Child("Text").GetComponent<Text>().text = SGGameManager.Instance.hero.GetCurrentHP + "/" + SGGameManager.Instance.hero.maxHP;

    }
}
