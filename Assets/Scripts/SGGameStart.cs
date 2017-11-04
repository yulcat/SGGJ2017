using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Linq;

public class SGGameStart : SGSingleton<SGGameStart>
{
    public int stagenumber;

    public GameObject starttext;

    public Text TitleText;
    public Text DescriptionText;

    public GameObject Counttextobj;
    public Text counttext;
    bool clicked = false;

    
    // Use this for initialization
    void OnEnable()
    {
        stagenumber = SGGameManager.Instance.CurrentStageNum;
        ShowText(stagenumber);
        StartCoroutine(Blink());
        Counttextobj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.Child("Panel").SetActive(false);
                StartCoroutine(BlinkCountText());
                clicked = true;
            }
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            starttext.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            starttext.SetActive(false);
            yield return new WaitForSeconds(0.5f);

        }
    }

    public void ShowText(int stagenumber)
    {

        switch (stagenumber)
        {
            case 1:
                TitleText.text = "제 1장 : 시작되는 사랑";
                DescriptionText.text = "휴가를 맞아 던전 여행을 하던 커플\n\n여행지에서 사랑을 키우다가 그만 남자친구가 다치고 말았다\n\n여자친구가 나서서 남자친구를 지켜야하는 상황!\n\n몬스터들을 함정으로 유인하여 해치우자";
                break;
            case 2:
                TitleText.text = "제 2장 : 피어오르는 사랑";
                DescriptionText.text = "위급한 상황에서 사랑은 피어나고..\n\n새로운 몬스터 등장!\n\n이번 몬스터는 좀 더 강합니다\n\n함정으로 유인하여 해치워 봅시다";
                break;
            case 3:
                TitleText.text = "마지막 장 : 영원한 사랑";
                DescriptionText.text = "역경을 견뎌낸 \n\n그들의 사랑은 영원할 것입니다.\n\n더 강략한 몬스터가 등장합니다. \n\n긴장하세요";
                break;
        }

        SGGameData.Instance.stageTitle = TitleText.text;

    }

    IEnumerator BlinkCountText()
    {

        Counttextobj.SetActive(true);
        counttext.text = "3";
        SGSoundManager.Instance.PlayButtonSound();
        yield return new WaitForSeconds(1f);
        
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0.01f, 0.5f);
        yield return new WaitForSeconds(1f);
        counttext.text = "2";
        SGSoundManager.Instance.PlayButtonSound();
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 100f, 0.5f);
        yield return new WaitForSeconds(1.2f);
    
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0.01f, 0.5f);
        yield return new WaitForSeconds(1f);
        counttext.text = "1";
        SGSoundManager.Instance.PlayButtonSound();
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 100f, 0.5f);
        yield return new WaitForSeconds(1.2f);

        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0.01f, 0.5f);
        yield return new WaitForSeconds(1f);
        counttext.text = "GO!";
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 100f, 0.5f);
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0, 0.3f);

        SGGameManager.Instance.Stage_Start();

    }
}
