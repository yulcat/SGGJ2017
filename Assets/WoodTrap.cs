using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodTrap : MonoBehaviour {
    public GameObject[] RedFloor;
    public int randoms;
    public GameObject WoodAttack;
    public GameObject[] WoodSpawPoint;
    // Use this for initialization
    void Start () {
        StartCoroutine(RandomSpawn());
	}
    IEnumerator RandomSpawn()
    {
        print("Check");
        yield return new WaitForSeconds(15);
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
        for (int i=0; i<2; i++)
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
        StartCoroutine(RandomSpawn());
        if (randoms == 0)
        {
            
            Instantiate(WoodAttack, WoodSpawPoint[2].transform.position = new Vector3(2.50f, 6.04f, 0.0f), Quaternion.identity);
        }
        else if (randoms == 1)
        {
            Instantiate(WoodAttack, WoodSpawPoint[1].transform.position = new Vector3(0.11f, 6.04f, 0.0f), Quaternion.identity);
        }
        else if (randoms == 2)
        {
            Instantiate(WoodAttack, WoodSpawPoint[0].transform.position = new Vector3(-2.4f, 6.04f, 0.0f), Quaternion.identity);
        }
        
    }
	// Update is called once per frame
	void Update () {
        randoms = Random.Range(0, 3);
    }
}
