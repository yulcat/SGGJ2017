using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unity.Linq;

public class SGMonster : SGBCharacter {

    public enum SGE_MONSTER_ACTION_STATE
    {
        TRACE_DESTINATION,  //목적지로 이동
        TRACE_HERO          //영웅을 향해 이동
    }

    SGE_MONSTER_ACTION_STATE actionStage = SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION;
    public SGE_MONSTER_ACTION_STATE GetActionState { get { return actionStage; } }

    public float sightLength;

    float distanceToHero = Mathf.Infinity;   //영웅과의 거리

	// Use this for initialization
	override protected void Start () {

        gameObject.UpdateAsObservable().Subscribe(_ =>
        {
            float distanceToHero = Vector3.Distance(transform.position, SGGameManager.Instance.hero.transform.position);

            if (distanceToHero <= sightLength)
                actionStage = SGE_MONSTER_ACTION_STATE.TRACE_HERO;
            else
                actionStage = SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION;

        });

        gameObject.FixedUpdateAsObservable().Subscribe(_ => {
            Vector3 destination = Vector3.zero;

            if (actionStage == SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION)
                destination = SGGameManager.Instance.CurrentMonsterDestination.position;
            else
                destination = SGGameManager.Instance.hero.transform.position;

            transform.position = Vector2.MoveTowards(transform.position,
                destination, Time.deltaTime * currentMoveSpeed);
        });

        base.Start();
	}
}
