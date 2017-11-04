using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unity.Linq;

public class SGMonster : SGCharacter
{

    public enum SGE_MONSTER_ACTION_STATE
    {
        IDLE,
        TRACE_DESTINATION,  //목적지로 이동
        TRACE_HERO,          //영웅을 향해 이동
        ATTACK_TO_HERO,
        ATTACK_TO_BASE
    }
    public enum MONSTER_TYPE
    {
        ZOMBIE,
        WHEEL,
        BIG
    }

    public MONSTER_TYPE monsterType = MONSTER_TYPE.ZOMBIE;

    public float sightLength;

    float distanceToHero = Mathf.Infinity;   //영웅과의 거리
    Animator myAnimator;
    Rigidbody2D body;
    IMonsterPattern pattern;

    // Use this for initialization
    override protected void Start()
    {
        switch (monsterType)
        {
            case MONSTER_TYPE.ZOMBIE: pattern = new NormalZombie(this); break;
            case MONSTER_TYPE.WHEEL: pattern = new WheelZombie(this); break;
            case MONSTER_TYPE.BIG: pattern = new BigZombie(this); break;
        }
        myAnimator = gameObject.GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        gameObject.UpdateAsObservable().Subscribe(_ =>
        {
            float distanceToHero = Vector3.Distance(transform.position, SGGameManager.Instance.hero.transform.position);
            pattern.SetActionState(distanceToHero);
            if (GetAliveState == SGE_ALIVE_STATE.DEAD)
            {
                SGGameManager.Instance.MonsterDie();
                Destroy(gameObject);
            }
        });

        gameObject.FixedUpdateAsObservable().Subscribe(_ =>
        {
            if (!movable) {
                body.velocity = Vector2.zero;
                return;
            }
           pattern.PerformAction();
        });

        base.Start();
    }

    void RotateToLookup(Vector3 target, float t = 1)
    {
        Vector3 myPos = transform.up;
        transform.up = Vector3.Slerp(myPos, target, t);
    }

    public void AttackToBase()
    {
        pattern.AttackBase();
    }

    public override bool AnyDamage(float damage, System.Guid guid)
    {
        if (!base.AnyDamage(damage, guid)) return false;
        myAnimator.SetTrigger("Hit");
        SGSoundManager.Instance.PlaySounds(1);
        return true;
    }

    interface IMonsterPattern { void SetActionState(float distance); void PerformAction(); void AttackBase(); }
    class NormalZombie : IMonsterPattern
    {
        SGE_MONSTER_ACTION_STATE actionState = SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION;
        protected SGMonster me;

        public NormalZombie(SGMonster parent)
        {
            me = parent;
        }

        public void SetActionState(float distanceToHero)
        {
            if (distanceToHero <= me.sightLength)
            {
                actionState = SGE_MONSTER_ACTION_STATE.TRACE_HERO;
                if (distanceToHero <= 1f)
                {
                    actionState = SGE_MONSTER_ACTION_STATE.ATTACK_TO_HERO;
                }
            }
            else if (actionState != SGE_MONSTER_ACTION_STATE.ATTACK_TO_BASE)
                actionState = SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION;
        }

        public void PerformAction()
        {
            if (actionState == SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION || actionState == SGE_MONSTER_ACTION_STATE.TRACE_HERO)
            {
                Vector3 destination = Vector3.zero;

                if (actionState == SGE_MONSTER_ACTION_STATE.TRACE_DESTINATION)
                    destination = SGGameManager.Instance.CurrentMonsterDestination.position;
                else if (actionState == SGE_MONSTER_ACTION_STATE.TRACE_HERO)
                    destination = SGGameManager.Instance.hero.transform.position;

                me.RotateToLookup((me.transform.position - destination).normalized);
                me.body.velocity = (destination - me.transform.position).normalized * me.currentMoveSpeed;
            }
            else
            {
                me.myAnimator.SetTrigger("Attack");
            }
        }

        public void AttackBase()
        {
            actionState = SGE_MONSTER_ACTION_STATE.ATTACK_TO_BASE;
        }
    }

    class WheelZombie : IMonsterPattern
    {
        bool tracingDestination;
        SGMonster me;
        const float damp = 0.5f;
        const float rotationSpeed = 0.3f;
        public WheelZombie(SGMonster parent)
        {
            me = parent;
        }

        public void AttackBase()
        {
            tracingDestination = true;
        }

        public void PerformAction()
        {
            Vector3 destination;
            if (tracingDestination)
                destination = SGGameManager.Instance.CurrentMonsterDestination.position;
            else
                destination = SGGameManager.Instance.hero.transform.position;
            me.RotateToLookup((me.transform.position - destination).normalized, rotationSpeed);
            var acc = -me.transform.up * me.moveSpeed * Time.deltaTime;
            var vertical = Vector2.Dot(me.body.velocity + (Vector2)acc, me.transform.up) * (1 - Time.deltaTime * damp);
            var horizontal = Vector2.Dot(me.body.velocity + (Vector2)acc, me.transform.right) * (1 - Time.deltaTime * damp * 2);
            me.body.velocity = me.transform.up * vertical + me.transform.right * horizontal;
        }

        public void SetActionState(float distanceToHero)
        {
            tracingDestination = distanceToHero > me.sightLength;
        }
    }

    class BigZombie : NormalZombie, IMonsterPattern
    {
        public BigZombie(SGMonster parent) : base(parent)
        {
            me = parent;
        }

        void IMonsterPattern.PerformAction()
        {
            if (me.myAnimator.GetBool("IsSplinting"))
            {
                me.body.velocity = -me.transform.up * me.moveSpeed * 3;
            }
            else base.PerformAction();
        }
    }
}