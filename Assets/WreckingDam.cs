﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingDam : MonoBehaviour {

    public int damage = 30;
    public float stun = 0;
    System.Guid guid;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
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
}
