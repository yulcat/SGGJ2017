using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unity.Linq;

public class SGMonster : SGCharacter {

    public enum SGE_MONSTER_ACTION_STATE
    {
        TRACE_DESTINATION,  //목적지로 이동
        TRACE_HERO,          //영웅을 향해 이동
        ATTACK_TO_HERO,
        ATTACK_TO_BASE
    }

    SGE_MONSTER_ACTION_STATE actionState = SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION;
    public SGE_MONSTER_ACTION_STATE GetActionState { get { return actionState; } }

    public float sightLength;

    float distanceToHero = Mathf.Infinity;   //영웅과의 거리
    Animator myAnimator;

	// Use this for initialization
	override protected void Start () {
        myAnimator = gameObject.GetComponentInChildren<Animator>();
        gameObject.UpdateAsObservable().Subscribe(_ =>
        {
            float distanceToHero = Vector3.Distance(transform.position, SGGameManager.Instance.hero.transform.position);



            if (distanceToHero <= sightLength)
            {
                actionState = SGE_MONSTER_ACTION_STATE.TRACE_HERO;
                if(distanceToHero <= 1f)
                {
                    actionState = SGE_MONSTER_ACTION_STATE.ATTACK_TO_HERO;
                }
            }
            else if (actionState != SGE_MONSTER_ACTION_STATE.ATTACK_TO_BASE)
                actionState = SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION;
            
            if (GetAliveState == SGE_ALIVE_STATE.DEAD)
            {
                Destroy(gameObject);
            }        
        });

        gameObject.FixedUpdateAsObservable().Subscribe(_ => {

            if(actionState == SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION || actionState == SGE_MONSTER_ACTION_STATE.TRACE_HERO)
            {
                Vector3 destination = Vector3.zero;

                if (actionState == SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION)
                    destination = SGGameManager.Instance.CurrentMonsterDestination.position;
                else if (actionState == SGE_MONSTER_ACTION_STATE.TRACE_HERO)
                    destination = SGGameManager.Instance.hero.transform.position;

                RotateToLookup((transform.position - destination).normalized);

                transform.position = Vector2.MoveTowards(transform.position,
                    destination, Time.deltaTime * currentMoveSpeed);
            }
            else
            {
                myAnimator.SetTrigger("Attack");
            }

        });

        base.Start();
	}

    void RotateToLookup(Vector3 target)
    {
        Vector3 myPos = transform.up;
        transform.up = Vector3.Slerp(myPos, target, 1f);
    }

    public void AttackToBase()
    {
        actionState = SGE_MONSTER_ACTION_STATE.ATTACK_TO_BASE;
    }

    public override void AnyDamage(float damage)
    {
        base.AnyDamage(damage);
        myAnimator.SetTrigger("Hit");
    }
}
