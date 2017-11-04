using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour {
    public GameObject Shadow;
    //public GameObject DropTheBomb;
    public float DropSPeed=5.0f;
    public float xValueMax = 2.21f;
    public float xValueMin = -2.361f;
    public float yValueMax = 3.88f;
    public float yValueMin = -3.59f;
    public float WreckingBallRotationTime=5.0f;
    public float WreckingBallDropTime;
    //public Transform WreckingBalls;
    bool DropCheck = false;
	// Use this for initialization
	void Start () {
        StartCoroutine(WreckingRotation());
	}
    IEnumerator WreckingRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(WreckingBallRotationTime);
            float xValueRandom = Random.Range(xValueMin, xValueMax);
            float yValueRandom = Random.Range(yValueMin, yValueMax);
            //DropTheBomb.transform.position = new Vector2(Shadow.transform.position.x,Shadow.transform.position.y+1.75f);
            Shadow.transform.position = new Vector2(xValueRandom,yValueRandom);
            Shadow.SetActive(true);
            StartCoroutine(WreckingBallDrop());
        }
    }
    IEnumerator WreckingBallDrop()
    {
        yield return new WaitForSeconds(WreckingBallDropTime);
        Shadow.SetActive(false);
        //DropTheBomb.SetActive(true);
        //DropCheck = true;
        //while (true)
        //{
        //    if (DropCheck)
        //    {
        //        DropTheBomb.transform.position = Vector2.MoveTowards(DropTheBomb.transform.position,Shadow.transform.position,DropSPeed);
        //    }
        //    else
        //        break; 
        //}
        //DropTheBomb.transform.Translate(Shadow.transform.position*DropSPeed*Time.deltaTime);
        //DropTheBomb.SetActive(false);
        //if (DropTheBomb.transform.position == Shadow.transform.position)
        //{
        //    DropCheck = false;
        //    DropTheBomb.SetActive(false);
        //}
    }
    //public void FixedUpdate()
    //{
    //    pushObjectBackInFrustum(WreckingBalls);
    //}
    //public void pushObjectBackInFrustum(Transform WreckingBalls)
    //{
    //    Vector3 pos = Camera.main.WorldToViewportPoint(WreckingBalls.position);
    //    if (pos.x < 0f)
    //        pos.x = 0f;

    //    if (pos.x > 1f)
    //        pos.x = 1f;

    //    if (pos.y < 0f)
    //        pos.y = 0f;

    //    if (pos.y > 1f)
    //        pos.y = 1f;
    //    WreckingBalls.position = Camera.main.ViewportToWorldPoint(pos);
    //}
}
