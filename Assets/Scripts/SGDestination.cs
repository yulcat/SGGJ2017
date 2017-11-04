using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGDestination : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.OnTriggerEnter2DAsObservable().Where(_ => _.GetComponent<SGMonster>() != null)
            .Subscribe(_ => {
                _.GetComponent<SGMonster>().AttackToBase();
            });
	}
}
