using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGMonsterAttack : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        gameObject.OnTriggerEnter2DAsObservable().Where(_ => _.GetComponent<SGHero>() != null).
            Subscribe(_ => {
                _.GetComponent<SGHero>().AnyDamage(5f);                
            });
	}
}
