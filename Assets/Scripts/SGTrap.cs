using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGTrap : MonoBehaviour {

    bool trigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        StartCoroutine(Damage(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        StopCoroutine(Damage(collision));
    }

    IEnumerator Damage(Collider2D collision)
    {
        while(trigger)
        {
            yield return new WaitForSeconds(1);
            collision.GetComponent<SGCharacter>().AnyDamage(5);
        }
    }


}
