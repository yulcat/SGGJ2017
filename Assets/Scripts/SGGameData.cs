using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGGameData : SGSingleton<SGGameData> {

    private static SGGameData SGDontInstance;

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

    public int GameScore = 0;



}
