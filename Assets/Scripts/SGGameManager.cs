using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Linq;

public class SGGameManager : SGSingleton<SGGameManager> {

    public SGHero hero;
    public Transform stagesTransform;

    IntReactiveProperty currentStageNum = new IntReactiveProperty(1);    //현재 스테이지 번호

    int allStageCount;
    [HideInInspector]
    public Transform currentStage;

    public Transform CurrentMonsterStartPoint { get { return currentStage.gameObject.Child("StartPoint").transform; } }
    public Transform CurrentMonsterDestination { get { return currentStage.gameObject.Child("Destination").transform; } }

    // Use this for initialization
    void Start () {
        allStageCount = stagesTransform.childCount;

        currentStageNum.Subscribe(_ => {
            currentStage = stagesTransform.gameObject.Child("Stage" + _).transform; //스테이지 정보를 저장
        });
    }
}
