using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Linq;
using LitJson;
using UnityEngine.SceneManagement;

public class SGGameManager : SGSingleton<SGGameManager> {

    public SGHero hero;

    public enum SGE_GameState
    {
        STAGE_START,
        GAME_FAIL,
        STAGE_CLEAR
    }

    ReactiveProperty<SGE_GameState> gameState = new ReactiveProperty<SGE_GameState>(SGE_GameState.STAGE_START);


    IntReactiveProperty currentStageNum = new IntReactiveProperty(1);    //현재 스테이지 번호

    int allStageCount;

    public Transform MonsterSpawn;
    public Transform CurrentMonsterStartPoint { get { return MonsterSpawn.gameObject.Child("StartPoint").transform; } }
    public Transform CurrentMonsterDestination { get { return MonsterSpawn.gameObject.Child("Destination").transform; } }

    public TextAsset stageJsonAsset;
    JsonData stageJson;

    public int CurrentStageNum { get { return currentStageNum.Value; } }    //현재 스테이지 번호 가져가기

    [HideInInspector]
    public int stageTime;

    // Use this for initialization
    void Start () {

        //게임스테이트에 따라
        gameState.Subscribe(_ =>
        {
            switch (_)
            {
                case SGE_GameState.STAGE_START:
                    StageStart();
                    break;
                case SGE_GameState.STAGE_CLEAR:
                    StageClear();
                    break;
                case SGE_GameState.GAME_FAIL:
                    GameFail();
                    break;
            }
        });        
    }

    void StageStart()
    {
        //스테이지 시간
        stageJson = SGUtils.GetJsonArrayForKey(JsonMapper.ToObject(stageJsonAsset.text), "stage", SceneManager.GetActiveScene().name);
        stageTime = int.Parse(stageJson["playtime"].ToString());
    }

    void StageClear()
    {
        if (stageJson["nextstage"].ToString() != "endstage")
            SceneManager.LoadScene(stageJson["nextstage"].ToString());
    }

    void GameClear()
    {

    }

    void GameFail()
    {

    }
    
}
