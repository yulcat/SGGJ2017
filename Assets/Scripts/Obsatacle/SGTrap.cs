using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SGTrap : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SGCharacter>() != null)
        {
            collision.GetComponent<SGCharacter>().OnContinueDamage(5f, 1f);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SGCharacter>() != null)
        {
            collision.GetComponent<SGCharacter>().OffContinueDamage();
        }
    }
}
