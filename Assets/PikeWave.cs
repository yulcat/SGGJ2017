﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeWave : MonoBehaviour {
    public int damage = 30;
    public float stun = 0;
    System.Guid guid;
    // Use this for initialization
    void Start () {
        Destroy(this.gameObject, 6);
	}
    void OnEnable()
    {
        guid = new System.Guid();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SGCharacter>() != null)
        {
            collision.GetComponent<SGCharacter>().AnyDamage(damage, guid, stun);
        }

    }
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.up * -7.0f * Time.deltaTime);
        
    }

}
