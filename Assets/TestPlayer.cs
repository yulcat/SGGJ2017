using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPlayer : MonoBehaviour {
    Animator animator;
    public Slider PlayerHP;
    public float Speed = 5.0f;
    public float TarFloorSpeedDown=0.7f;
    public float TrapDamageTime = 1.0f;
    float Hor;
    float Ver;
    public Transform Player;
    bool TrapCheck = false;
    public float TimeCheck;
    // Use this for initialization
    void Start()
    {
        //animator = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D ColEnter)
    {
        if (ColEnter.CompareTag("Trap"))
        {
            //TimeCheck = Time.time;
            TrapCheck = true;
            StartCoroutine(HpdecreaseFormTrap());
        }
        else if (ColEnter.CompareTag("TarFloor"))
        {
            Speed = Speed * TarFloorSpeedDown;
        }
        else if (ColEnter.CompareTag("RealDeepTrap"))
        {
            //animator.SetBool("PlayerDie",true);
            StartCoroutine(GameOver(2.0f));
            
        }
    }
    public void OnTriggerStay2D(Collider2D ColStay)
    {
        
    }
    public void OnTriggerExit2D(Collider2D ColExit)
    {
        TrapCheck = false;
        Speed = 5.0f;
    }
    IEnumerator HpdecreaseFormTrap()
    {
        while (TrapCheck)
        {
            if (TrapCheck == true)
            {
                yield return new WaitForSeconds(TrapDamageTime);
                print("HPMinus");
                PlayerHP.value -= 5;
            }
            else
                break;
        }
    }
    IEnumerator GameOver(float Sec)
    {
        yield return new WaitForSeconds(Sec);
        print("PlayerDie");
        this.gameObject.SetActive(false);
        //Player가 죽으면 Scene 전환 Or (RePlay,Exit) 창
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        pushObjectBackInFrustum(Player);
        Vector2 moveDirection = UltimateJoystick.GetPosition("Movement");
        Vector2 movement = new Vector2(moveDirection.x, moveDirection.y);
        transform.Translate(movement * Speed * Time.deltaTime);
    }

    public void pushObjectBackInFrustum(Transform Player)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(Player.position);
        if (pos.x < 0f)
            pos.x = 0f;

        if (pos.x > 1f)
            pos.x = 1f;

        if (pos.y < 0f)
            pos.y = 0f;

        if (pos.y > 1f)
            pos.y = 1f;
        Player.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
