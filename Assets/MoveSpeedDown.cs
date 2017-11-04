using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedDown : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<SGCharacter>().DownMoveSpeed(0.3f);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<SGCharacter>().BackMoveSpeed();
    }
}
