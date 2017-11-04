using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class SGCharacter : MonoBehaviour
{

    public float maxHP;
    public float moveSpeed;

    protected float currentHP;
    protected float currentMoveSpeed;   //현재 이동속도
    public float GetCurrentHP { get { return currentHP; } }
    public string hitEffect;
    Dictionary<Guid, float> attacks = new Dictionary<Guid, float>();

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

    public bool GuidCheck(Guid guid)
    {
        if (!attacks.ContainsKey(guid))
        {
            attacks.Add(guid, Time.time);
            return true;
        }
        if (Time.time > attacks[guid] + 0.5f)
        {
            attacks[guid] = Time.time;
            return true;
        }
        return false;
    }

    //데미지를 받으면
    public virtual bool AnyDamage(float damage, Guid guid)
    {
        if (!GuidCheck(guid)) return false;
        currentHP -= damage;
        currentHP = Mathf.Max(0f, currentHP);
        if (!string.IsNullOrEmpty(hitEffect)) EffectSpawner.SetEffect(hitEffect, transform.position);

        if (currentHP == 0f)
            aliveState = SGE_ALIVE_STATE.DEAD;
        return true;
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
        currentMoveSpeed = currentMoveSpeed * multiply;
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
}