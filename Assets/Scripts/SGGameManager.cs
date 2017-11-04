using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Linq;
using LitJson;

public class SGGameManager : SGSingleton<SGGameManager> {

    public SGHero hero;
    public Transform stagesTransform;
    public Transform monstersTransform;

    public enum SGE_GameState
    {
        GAME_INIT,  //게임 초기화
        STAGE_START,
        GAME_FAIL,
        GAME_CLEAR
    }

    ReactiveProperty<SGE_GameState> gameState = new ReactiveProperty<SGE_GameState>(SGE_GameState.GAME_INIT);


    IntReactiveProperty currentStageNum = new IntReactiveProperty(1);    //현재 스테이지 번호

    int allStageCount;
    [HideInInspector]
    public Transform currentStage;

    public Transform CurrentMonsterStartPoint { get { return currentStage.gameObject.Child("StartPoint").transform; } }
    public Transform CurrentMonsterDestination { get { return currentStage.gameObject.Child("Destination").transform; } }

    public TextAsset stageJsonAsset;
    JsonData stageJson;

    public int CurrentStageNum { get { return currentStageNum.Value; } }    //현재 스테이지 번호 가져가기

    public int stageTime;

    // Use this for initialization
    void Start () {

        //게임스테이트에 따라
        gameState.Subscribe(_ =>
        {
            switch (_)
            {
                case SGE_GameState.GAME_INIT:
                    GameInit();
                    break;
                case SGE_GameState.STAGE_START:
                    StageStart();
                    break;
                case SGE_GameState.GAME_CLEAR:
                    GameClear();
                    break;
                case SGE_GameState.GAME_FAIL:
                    GameFail();
                    break;
            }
        });

        //스테이지가 변경되면
        currentStageNum.Subscribe(_ => {
            currentStage = stagesTransform.gameObject.Child("Stage" + _).transform; //스테이지 정보를 저장
            stageJson = SGUtils.GetJsonArrayForKey(JsonMapper.ToObject(stageJsonAsset.text), "stage", _);
            gameState.Value = SGE_GameState.STAGE_START;
            
        });
    }

    void GameInit()
    {
        allStageCount = stagesTransform.childCount;
    }

    void StageStart()
    {
        //스테이지 시간
        stageTime = int.Parse(stageJson["playtime"].ToString());


    }

    

    void StageClear()
    {
        if (currentStageNum.Value > allStageCount)
            gameState.Value = SGE_GameState.GAME_CLEAR;
        else
            currentStageNum.Value += 1;
    }

    void GameClear()
    {

    }

    void GameFail()
    {

    }
    
}
