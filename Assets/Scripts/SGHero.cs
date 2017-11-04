using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UniRx;
using UniRx.Triggers;
using Unity.Linq;

public class SGHero : SGCharacter {

    Animator myAnimator;
	// Use this for initialization
	override protected void Start () {
        myAnimator = gameObject.Child("Body").GetComponent<Animator>();

        gameObject.FixedUpdateAsObservable().Subscribe(_ =>
        {
            float h = CnInputManager.GetAxis("Horizontal");
            float v = CnInputManager.GetAxis("Vertical");

            RotateToLookup(new Vector3(h, v, 0f));

            if (h != 0f && v != 0f)
                myAnimator.SetBool("Moving", true);
            else
                myAnimator.SetBool("Moving", false);

            transform.Translate(new Vector3(h, v, 0f) * currentMoveSpeed * Time.deltaTime);
        });

        base.Start();
	}
    void RotateToLookup(Vector3 target)
    {
        Debug.Log(target);
        Vector3 myPos = gameObject.Child("Body").transform.up;
        gameObject.Child("Body").transform.up = Vector3.Slerp(myPos, target, 1f);
    }

}
