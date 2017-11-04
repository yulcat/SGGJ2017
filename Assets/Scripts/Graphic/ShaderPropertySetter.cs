﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class ShaderPropertySetter : MonoBehaviour
{
    public string PropertyName;
    public float Value;
    public Projector Target;

    // Update is called once per frame
    void Update()
    {
        Target.material.SetFloat(PropertyName, Value);
    }
}
