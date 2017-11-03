using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGBCharacter : MonoBehaviour
{

    public float maxHP;

    float currentHP;
    public float GetCurrentHP { get { return currentHP; } }

    public enum SGE_ALIVE_STATE
    {
        ALIVE,
        DEAD
    }

    SGE_ALIVE_STATE aliveState = SGE_ALIVE_STATE.ALIVE;
    public SGE_ALIVE_STATE GetAliveState { get { return aliveState; } }

    // Use this for initialization
    void Start()
    {
        currentHP = maxHP;
    }

    //데미지를 받으면
    void AnyDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0f, currentHP);

        if (currentHP == 0f)
            aliveState = SGE_ALIVE_STATE.DEAD;
    }

    //힐이 있을지 모르지만 힐을 받으면
    void AnyHeal(float heal)
    {
        currentHP += heal;
        currentHP = Mathf.Min(maxHP, currentHP);
    }
}