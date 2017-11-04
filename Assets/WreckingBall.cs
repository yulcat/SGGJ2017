using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public float WreckingBallRotationTime = 5.0f;
    public float WreckingBallDropTime;
    Animator animator;
    GameObject child;
    void Start()
    {
        animator = GetComponent<Animator>();
        child = transform.GetChild(0).gameObject;
        StartCoroutine(WreckingRotation());
    }
    IEnumerator WreckingRotation()
    {
        child.SetActive(false);
        animator.enabled = false;
        while (true)
        {
            yield return new WaitForSeconds(WreckingBallRotationTime);
            transform.position = SGGameManager.Instance.hero.transform.position;
            child.SetActive(true);
            var anim = animator.runtimeAnimatorController;
            DestroyImmediate(animator);
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = anim;
            StartCoroutine(WreckingBallDrop());
            LeanTween.delayedCall(1f, ()=>{SGSoundManager.Instance.PlaySounds(7);});
           // ;
        }
    }
    IEnumerator WreckingBallDrop()
    {
        
        yield return new WaitForSeconds(WreckingBallDropTime);
        child.SetActive(false);
        animator.enabled = false;
        
    }


}
