using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realDeepTrap : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<SGCharacter>() != null)
            collision.GetComponent<SGCharacter>().AnyDamage(collision.GetComponent<SGCharacter>().maxHP, new System.Guid());
    }
}
