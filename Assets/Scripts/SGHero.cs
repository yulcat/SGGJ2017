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

    CompositeDisposable heroDisposable = new CompositeDisposable();
    Rigidbody2D body;
    bool movable;
    // Use this for initialization
    override protected void Start()
    {
        myAnimator = gameObject.Child("Body").GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        base.Start();
        movable = true;
    }

    void FixedUpdate()
    {
        if (!this.movable)
            return;
        float h = CnInputManager.GetAxis("Horizontal");
        float v = CnInputManager.GetAxis("Vertical");

        RotateToLookup(new Vector3(h, v, 0f));

        if (h != 0f && v != 0f)
        {
            myAnimator.SetBool("Moving", true);
            body.velocity = new Vector3(h, v, 0f) * currentMoveSpeed;
        }
        else
        {
            myAnimator.SetBool("Moving", false);
            body.velocity = Vector3.zero;
        }
    }
    void RotateToLookup(Vector3 target)
    {
        Vector3 myPos = gameObject.Child("Body").transform.up;
        gameObject.Child("Body").transform.up = Vector3.Slerp(myPos, target, 1f);
    }
    IEnumerator CoSetUnmovable(float time)
    {
        StopAllCoroutines();
        movable = false;
        yield return new WaitForSeconds(time);
        movable = true;
    }

    void SetUnmovable(float time)
    {
        StartCoroutine(CoSetUnmovable(time));
    }

    public override bool AnyDamage(float damage, System.Guid guid)
    {
        if (!base.AnyDamage(damage, guid)) return false;
        if (GetAliveState == SGE_ALIVE_STATE.DEAD)
        {
            myAnimator.SetBool("Dead", true);
            heroDisposable.Clear();
            SGGameManager.Instance.HeroDie();
            movable = false;
        }
        else
            myAnimator.SetTrigger("Hit");
        return true;
    }

    public void AnyDamage(float damage, System.Guid guid, float stun)
    {
        SetUnmovable(stun);
        AnyDamage(damage, guid);
    }

}
