using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UniRx;
using UniRx.Triggers;
using Unity.Linq;

public class SGHero : SGCharacter
{

    Animator myAnimator;

    Rigidbody2D body;
    // Use this for initialization
    override protected void Start()
    {
        myAnimator = gameObject.Child("Body").GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        base.Start();
    }

    void FixedUpdate()
    {
        if (!this.movable)
            return;
        float h = CnInputManager.GetAxis("Horizontal");
        float v = CnInputManager.GetAxis("Vertical");
        var dirc = new Vector3(h, v, 0);
        if (dirc.magnitude > 1) dirc /= dirc.magnitude;

        RotateToLookup(dirc);

        if (h != 0f || v != 0f)
        {
            myAnimator.SetBool("Moving", true);
            body.velocity = dirc * currentMoveSpeed;
        }
        else
        {
            myAnimator.SetBool("Moving", false);
            body.velocity = Vector3.zero;
        }
    }

    void RotateToLookup(Vector3 target)
    {
        gameObject.Child("Body").transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-target.x, target.y));
    }

    public override bool AnyDamage(float damage, System.Guid guid)
    {
        if (!base.AnyDamage(damage, guid)) return false;
        if (GetAliveState == SGE_ALIVE_STATE.DEAD)
            PlayerDead();
        else
            myAnimator.SetTrigger("Hit");
        return true;
    }

    void PlayerDead()
    {
        myAnimator.SetBool("Dead", true);
        SGGameManager.Instance.HeroDie();
        movable = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
