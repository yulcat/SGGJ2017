using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realDeepTrap : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<SGCharacter>().Dead();
    }
}
