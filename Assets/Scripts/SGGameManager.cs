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
        STAGE_INTRO,
        STAGE_START,
        GAME_FAIL,
        STAGE_CLEAR
    }

    ReactiveProperty<SGE_GameState> gameState = new ReactiveProperty<SGE_GameState>(SGE_GameState.STAGE_INTRO);


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
    public GameObject GameStartPanel;
    public GameObject GameClearPanel;
    public GameObject GameOverPanel;

    IntReactiveProperty monsterCount = new IntReactiveProperty();

    // Use this for initialization
    void Start() {



        //게임스테이트에 따라
        gameState.Subscribe(_ =>
        {
            switch (_)
            {
                case SGE_GameState.STAGE_INTRO:
                    StageIntro();
                    break;
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

    void StageIntro()
    {
        //GameStartPanel.SetActive(true);임시로빠르게 하기 위해
        hero.SetMoveable(false);
        stageJson = SGUtils.GetJsonArrayForKey(JsonMapper.ToObject(stageJsonAsset.text), "stage", SceneManager.GetActiveScene().name);
        stageTime = int.Parse(stageJson["playtime"].ToString());

        timerSlider.SetTimerText(stageTime);


        for(int s=0;s< stageJson["waveInfo"].Count;s++)
        {
            monsterCount.Value += int.Parse(stageJson["waveInfo"][s]["Count"].ToString());
        }

        monsterCount.Where(_ => _ <= 0).Subscribe(_ => { gameState.Value = SGE_GameState.STAGE_CLEAR; });

        //임시로빠르게 하기 위해
        Stage_Start();
    }

    public void Stage_Start()
    {
        gameState.Value = SGE_GameState.STAGE_START;
    }


    void StageStart()
    {
        //스테이지 시간
        // GameStartPanel.SetActive(false);임시로빠르게 하기 위해
        hero.SetMoveable(true);
        timerSlider.TimerStart(stageTime);
        
    }


    public void OnPlayTime(int remainTime) //몬스터 리스폰을 위하여
    {

        if (remainTime <= 0)
        {
            gameState.Value = SGE_GameState.GAME_FAIL;
            return;
        }

        int startTime = stageTime - remainTime;
        JsonData waveInfo =  SGUtils.GetJsonArrayForKey(stageJson["waveInfo"], "wave", currentWaveNum);
        if (waveInfo == null)
            return;

        GameObject monsterPrefab = monsterPrefabs.Where(_ => _.name == waveInfo["prefab"].ToString()).FirstOrDefault();
        monsterCount.Value = int.Parse(waveInfo["Count"].ToString());
        float duration = float.Parse(waveInfo["duration"].ToString());
       if (int.Parse(waveInfo["starttime"].ToString()) <= startTime)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(0f), System.TimeSpan.FromSeconds(duration))
                .Take(monsterCount.Value).Subscribe(_ => {
                    GameObject mon = Instantiate<GameObject>(monsterPrefab, CurrentMonsterStartPoint.position, Quaternion.identity, MonsterSpawn);
                });

            currentWaveNum += 1;
        }



       
    }

    void StageClear()
    {
        timerSlider.TimerStop();
        GameClearPanel.SetActive(true);

        /*
        if (stageJson["nextstage"].ToString() != "endstage")
            SceneManager.LoadScene(stageJson["nextstage"].ToString());
            */
    }

    void GameClear()
    {
        
    }

    public void Game_Fail()
    {
        gameState.Value = SGE_GameState.GAME_FAIL;
    }

    void GameFail()
    {
        timerSlider.TimerStop();
        GameOverPanel.SetActive(true);
    }

    public void HeroDie()
    {
        //영웅 죽음
        gameState.Value = SGE_GameState.GAME_FAIL;
    }

    public void MonsterDie()
    {
        monsterCount.Value--;
    }
    
}
