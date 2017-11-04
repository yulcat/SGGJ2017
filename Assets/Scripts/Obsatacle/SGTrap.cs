using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGTrap : MonoBehaviour
{
    public int damage = 3;
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
