using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGCharacter : MonoBehaviour
{

    public float maxHP;
    public float moveSpeed;

    protected float currentHP;
    protected float currentMoveSpeed;   //현재 이동속도
    public float GetCurrentHP { get { return currentHP; } }
    CompositeDisposable continueDamageDIspose = new CompositeDisposable();

    public enum SGE_ALIVE_STATE
    {
        ALIVE,
        DEAD
    }

    SGE_ALIVE_STATE aliveState = SGE_ALIVE_STATE.ALIVE;
    public SGE_ALIVE_STATE GetAliveState { get { return aliveState; } }

    // Use this for initialization
    virtual protected void Start()
    {
        currentHP = maxHP;
        currentMoveSpeed = moveSpeed;
    }

    //데미지를 받으면
    public virtual void AnyDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0f, currentHP);

        if (currentHP == 0f)
            aliveState = SGE_ALIVE_STATE.DEAD;
    }

    //힐이 있을지 모르지만 힐을 받으면
    public void AnyHeal(float heal)
    {
        currentHP += heal;
        currentHP = Mathf.Min(maxHP, currentHP);
    }

    public void DownMoveSpeed(float multiply)
    {
        print("Down");
        currentMoveSpeed = currentMoveSpeed*multiply;
    }

    public void BackMoveSpeed()
    {
        print("back");
        currentMoveSpeed = moveSpeed;
    }
    public void Dead()
    {
        aliveState = SGE_ALIVE_STATE.DEAD;
    }

    public void OnContinueDamage(float damage, float duration)
    {
        Observable.Interval(System.TimeSpan.FromSeconds(duration)).Subscribe(_ =>
        {
            AnyDamage(damage);
        }).AddTo(continueDamageDIspose);
    }

    public void OffContinueDamage()
    {
        continueDamageDIspose.Clear();
    }
}