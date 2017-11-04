using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGBGM : SGSingleton<SGBGM> {

    private static SGBGM SGDontInstance;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);

        if (SGDontInstance == null)
        {
            SGDontInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Awake();
    }
}
