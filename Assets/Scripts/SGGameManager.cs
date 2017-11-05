﻿using System.Collections;
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
    public Transform BuddyTransform;
    public Transform CurrentMonsterStartPoint { get { return MonsterSpawn.gameObject.Child("StartPoint").transform; } }
    public Transform CurrentMonsterDestination { get { return BuddyTransform; } }

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
    System.IDisposable spawnDis;

    public GameObject GameScoreText;

    // Use this for initialization
    void Start() {
        currentStageNum.Value = int.Parse(SceneManager.GetActiveScene().name.Substring(5));

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
        GameStartPanel.SetActive(true);
        hero.SetMoveable(false);
        stageJson = SGUtils.GetJsonArrayForKey(JsonMapper.ToObject(stageJsonAsset.text), "stage", SceneManager.GetActiveScene().name);
        stageTime = int.Parse(stageJson["playtime"].ToString());

        if (!SGGameData.Instance.inifinityMode)
        {
            timerSlider.SetTimerText(stageTime);
        }

        for(int s=0;s< stageJson["waveInfo"].Count;s++)
        {
            monsterCount.Value += int.Parse(stageJson["waveInfo"][s]["Count"].ToString());
        }

        monsterCount.Where(_ => _ <= 0).Subscribe(_ => {
            if (gameState.Value == SGE_GameState.STAGE_START)
                gameState.Value = SGE_GameState.STAGE_CLEAR;
        });
    }

    public void Stage_Start()
    {
        gameState.Value = SGE_GameState.STAGE_START;
    }

    public bool IsGameEnd()
    {
        return (gameState.Value != SGE_GameState.STAGE_START );
    }


    void StageStart()
    {
        //스테이지 시간
        GameStartPanel.SetActive(false);
        hero.SetMoveable(true);
        if (!SGGameData.Instance.inifinityMode)
        {
            timerSlider.TimerStart(stageTime);
        }
        else
        {
            timerSlider.TimerStart(int.MaxValue);
        }
        
    }


    public void OnPlayTime(int remainTime) //몬스터 리스폰을 위하여
    {

        if (remainTime <= 0)
        {
            if (gameState.Value == SGE_GameState.STAGE_START)
                gameState.Value = SGE_GameState.GAME_FAIL;
            return;
        }

        int startTime = stageTime - remainTime;
        if (SGGameData.Instance.inifinityMode)
        {
            startTime = int.MaxValue - remainTime;
        }

        JsonData waveInfo =  SGUtils.GetJsonArrayForKey(stageJson["waveInfo"], "wave", currentWaveNum);
        if (waveInfo == null)
            return;

       if (float.Parse(waveInfo["starttime"].ToString()) <= startTime)
        {
            GameObject monsterPrefab = monsterPrefabs.Where(_ => _.name == waveInfo["prefab"].ToString()).FirstOrDefault();
            int mCount = int.Parse(waveInfo["Count"].ToString());
            float duration = float.Parse(waveInfo["duration"].ToString());


            spawnDis = Observable.Timer(System.TimeSpan.FromSeconds(0f), System.TimeSpan.FromSeconds(duration))
                .Take(mCount).Subscribe(_ => {
                    GameObject mon = Instantiate<GameObject>(monsterPrefab, CurrentMonsterStartPoint.position, Quaternion.identity, MonsterSpawn);
                }).AddTo(this);

            currentWaveNum += 1;
        } 
    }

    void MonstersStop()
    {
        MonsterSpawn.gameObject.Descendants().OfComponent<SGMonster>().ForEach(_ => _.SetMoveable(false));
        spawnDis.Dispose();
    }

    void StageClear()
    {
        int remainTime = timerSlider.GetRemainTime();
        timerSlider.TimerStop();
        MonstersStop();

        //게임 스코어 결산
        int currentScore = ((int)hero.GetCurrentHP * 100) + (remainTime * 100);
        SGGameData.Instance.GameScore += currentScore;

        SGPostScore.Instance.PostScore();
        GameClearPanel.SetActive(true);
        GameClearPanel.GetComponent<SGGameClear>().texts(remainTime, (int)hero.GetCurrentHP, SGGameData.Instance.GameScore);
    }

    public void GoToNextStage()
    {
        if (stageJson["nextstage"].ToString() != "endstage")
            SceneManager.LoadScene(stageJson["nextstage"].ToString());
        else
            SceneManager.LoadScene("StageStartEnd");
    }

    public void Game_Fail()
    {
        if(gameState.Value == SGE_GameState.STAGE_START)
            gameState.Value = SGE_GameState.GAME_FAIL;
    }

    void GameFail()
    {
        timerSlider.TimerStop();
        MonstersStop();
        SGPostScore.Instance.PostScore();
        GameOverPanel.SetActive(true);
        
        GameOverPanel.GetComponent<SGGameend>().SetData(SGGameData.Instance.GameNickname, SGGameData.Instance.GameScore.ToString());
    }

    public void HeroDie()
    {
        //영웅 죽음
        if (gameState.Value == SGE_GameState.STAGE_START)
            gameState.Value = SGE_GameState.GAME_FAIL;
    }

    public void MonsterDie()
    {
        monsterCount.Value--;
        SGGameData.Instance.GameScore += 500;
    }
    
}
