using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGMonsterAttack : MonoBehaviour
{
    public float stun = 0f;
    System.Guid guid;
    // Use this for initialization
    void OnEnable()
    {
        float damage = GetComponentInParent<SGMonster>().attackDamage;
        guid = new System.Guid();
        Observable.Merge(gameObject.OnTriggerEnter2DAsObservable(), gameObject.OnTriggerStay2DAsObservable()).Where(_ => _.GetComponent<SGHero>() != null || _.GetComponent<SGDestination>() != null).
            Subscribe(_ =>
            {
                SGGameManager.Instance.hero.AnyDamage(damage, guid, stun);
            });
            SGSoundManager.Instance.PlaySounds(1);
    }
}
