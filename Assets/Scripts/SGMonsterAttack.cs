using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGMonsterAttack : MonoBehaviour
{

    System.Guid guid;
    // Use this for initialization
    void OnEnable()
    {
        guid = new System.Guid();
        gameObject.OnTriggerEnter2DAsObservable().Where(_ => _.GetComponent<SGHero>() != null).
            Subscribe(_ =>
            {
                _.GetComponent<SGHero>().AnyDamage(5f, guid);
            });
    }
}
