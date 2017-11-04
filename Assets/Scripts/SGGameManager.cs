using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Linq;
using LitJson;
using UnityEngine.SceneManagement;
using System.Linq;

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

    public Transform MonsterSpawn;
    public Transform CurrentMonsterStartPoint { get { return MonsterSpawn.gameObject.Child("StartPoint").transform; } }
    public Transform CurrentMonsterDestination { get { return MonsterSpawn.gameObject.Child("Destination").transform; } }

    public TextAsset stageJsonAsset;
    JsonData stageJson;

    public int CurrentStageNum { get { return currentStageNum.Value; } }    //현재 스테이지 번호 가져가기

    [HideInInspector]
    public int stageTime;

    [HideInInspector]
    public int currentWaveNum = 1;

    public GameObject[] monsterPrefabs;
    public SGTimerSlider timerSlider;

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

        timerSlider.TimerStart(stageTime);
    }


    public void OnPlayTime(int remainTime) //몬스터 리스폰을 위하여
    {
        int startTime = stageTime - remainTime;
        JsonData waveInfo =  SGUtils.GetJsonArrayForKey(stageJson["waveInfo"], "wave", currentWaveNum);
        if (waveInfo == null)
            return;

        GameObject monsterPrefab = monsterPrefabs.Where(_ => _.name == waveInfo["prefab"].ToString()).FirstOrDefault();
        int count = int.Parse(waveInfo["Count"].ToString());
        float duration = float.Parse(waveInfo["duration"].ToString());
       if (int.Parse(waveInfo["starttime"].ToString()) <= startTime)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(0f), System.TimeSpan.FromSeconds(duration))
                .Take(count).Subscribe(_ => {
                    GameObject mon = Instantiate<GameObject>(monsterPrefab, CurrentMonsterStartPoint.position, Quaternion.identity, MonsterSpawn);
                });

            currentWaveNum += 1;
        }
        
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

    public void HeroDie()
    {
        //영웅 죽음
        gameState.Value = SGE_GameState.GAME_FAIL;
    }
    
}
