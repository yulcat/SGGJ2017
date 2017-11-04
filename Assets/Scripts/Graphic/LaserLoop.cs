using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserLoop : MonoBehaviour
{
    public float LengthMultiplier = 1;
    public float TimeMultiplier = 1;
    LineRenderer rend;
    void Start()
    {
        rend = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        rend.material.SetFloat("_Length", rend.GetPosition(1).y * LengthMultiplier);
        rend.material.SetFloat("_Offset", Time.time * TimeMultiplier);
    }
}
