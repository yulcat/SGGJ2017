using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedDown : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<SGCharacter>() != null)
            col.GetComponent<SGCharacter>().DownMoveSpeed(0.3f, this);
    }
    public void OnTriggerExit2D(Collider2D cols)
    {
        if (cols.GetComponent<SGCharacter>() != null)
            cols.GetComponent<SGCharacter>().BackMoveSpeed(this);
    }
}
