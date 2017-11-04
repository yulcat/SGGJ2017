using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public GameObject[] RedFloor;
    public int randoms;
    public GameObject ArrowAttack;
    public GameObject[] raser;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(RandomSpawn());
    }
    IEnumerator RandomSpawn()
    {
        print("Check");
        yield return new WaitForSeconds(5);
        if (randoms == 0)
        {
            RedFloor[0].SetActive(true);
            StartCoroutine(ThreeTimeSign(randoms));
        }
        else if (randoms == 1)
        {
            RedFloor[1].SetActive(true);
            StartCoroutine(ThreeTimeSign(randoms));
        }
        else if (randoms == 2)
        {
            RedFloor[2].SetActive(true);
            StartCoroutine(ThreeTimeSign(randoms));
        }

    }
    IEnumerator ThreeTimeSign(int randoms)
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (randoms == 0)
            {
                RedFloor[0].SetActive(false);
            }
            else if (randoms == 1)
            {
                RedFloor[1].SetActive(false);
            }
            else if (randoms == 2)
            {
                RedFloor[2].SetActive(false);
            }
            yield return new WaitForSeconds(0.5f);
            if (randoms == 0)
            {
                RedFloor[0].SetActive(true);
            }
            else if (randoms == 1)
            {
                RedFloor[1].SetActive(true);
            }
            else if (randoms == 2)
            {
                RedFloor[2].SetActive(true);
            }
        }
        yield return new WaitForSeconds(1.0f);
        RedFloor[0].SetActive(false);
        RedFloor[1].SetActive(false);
        RedFloor[2].SetActive(false);        
        if (randoms == 0)
        {
            raser[0].SetActive(true);
        }
        else if (randoms == 1)
        {
            raser[2].SetActive(true);
        }
        else if (randoms == 2)
        {
            raser[1].SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        raser[0].SetActive(false);
        raser[2].SetActive(false);
        raser[1].SetActive(false);
        StartCoroutine(RandomSpawn());
    }
    // Update is called once per frame
    void Update()
    {
        randoms = Random.Range(0, 3);
    }
}
