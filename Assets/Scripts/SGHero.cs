using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UniRx;
using UniRx.Triggers;

public class SGHero : SGCharacter {

	// Use this for initialization
	override protected void Start () {
        gameObject.FixedUpdateAsObservable().Subscribe(_ =>
        {
            float h = CnInputManager.GetAxis("Horizontal");
            float v = CnInputManager.GetAxis("Vertical");

            transform.Translate(new Vector3(h, v, 0f) * currentMoveSpeed * Time.deltaTime);
        });

        base.Start();
	}
}
