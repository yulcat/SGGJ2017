using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGTrap : MonoBehaviour {

    bool trigger = false;

	// Use this for initialization
	void Start () {
        /*
        gameObject.OnTriggerEnter2DAsObservable()
            .Where(_=> _.GetComponent<SGBCharacter>() != null)
            .SelectMany(_ => Observable.Interval(System.TimeSpan.FromSeconds(1)).Select(_2 => _))
            .TakeUntil(gameObject.OnTriggerExit2DAsObservable())
            .RepeatUntilDestroy(this)
            .Subscribe(_ => {
                _.GetComponent<SGBCharacter>().AnyDamage(5);
            });
          */  

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        StartCoroutine(Damage(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        StopCoroutine(Damage(collision));
    }

    IEnumerator Damage(Collider2D collision)
    {
        while(trigger)
        {
            yield return new WaitForSeconds(1);
            collision.GetComponent<SGBCharacter>().AnyDamage(5);
        }
    }


}
