using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public float WreckingBallRotationTime = 5.0f;
    public float WreckingBallDropTime;
    Animator animator;
    GameObject child;

    bool enable = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        child = transform.GetChild(0).gameObject;
        StartCoroutine(WreckingRotation());
    }

    private void Update()
    {

        enable = !SGGameManager.Instance.IsGameEnd();
    }
    IEnumerator WreckingRotation()
    {
        child.SetActive(false);
        animator.enabled = false;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (enable)
            {
                yield return new WaitForSeconds(WreckingBallRotationTime);
                transform.position = SGGameManager.Instance.hero.transform.position;
                child.SetActive(true);
                var anim = animator.runtimeAnimatorController;
                DestroyImmediate(animator);
                animator = gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = anim;
                StartCoroutine(WreckingBallDrop());
            }
        }
    }
    IEnumerator WreckingBallDrop()
    {
        yield return new WaitForSeconds(WreckingBallDropTime);
        child.SetActive(false);
        animator.enabled = false;
    }
}
