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
    // Use this for initialization
    void Start()
    {
        stagenumber = SGGameManager.Instance.CurrentStageNum;
        ShowText(stagenumber);
        StartCoroutine(Blink());
        Counttextobj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.Child("Panel").SetActive(false);
            StartCoroutine(BlinkCountText());
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
                DescriptionText.text = "휴가를 맞아 던전 여행을 하던...\n\n\n\n몬스터들을 함정으로 유인하여 해치우자";
                break;
            case 2:
                TitleText.text = "제 2장 : 피어오르는 사랑";
                DescriptionText.text = "새로운 몬스터 등장!\n대사는 좀 나중에 생각해봅시다";
                break;
            case 3:
                TitleText.text = "제 3장 : 계속되는 사랑";
                DescriptionText.text = "아..힘들어\n대사는 좀 나중에 생각해봅시다";
                break;
            case 4:
                TitleText.text = "제 4장 : 지쳐가는 사랑";
                DescriptionText.text = "새로운 몬스터 등장!\n대사는 좀 나중에 생각해봅시다";
                break;
            case 5:
                TitleText.text = "마지막 장 : 영원한 사랑";
                DescriptionText.text = "그 둘의 사랑은 영원할 것인가?\n대사는 좀 나중에 생각해봅시다";
                break;



        }

    }

    IEnumerator BlinkCountText()
    {

        Counttextobj.SetActive(true);
        counttext.text = "3";
        yield return new WaitForSeconds(1f);
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0.01f, 1f);
        yield return new WaitForSeconds(1f);
        counttext.text = "2";
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 100f, 1f);
        yield return new WaitForSeconds(1.2f);
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0.01f, 1f);
        yield return new WaitForSeconds(1f);
        counttext.text = "1";
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 100f, 1f);
        yield return new WaitForSeconds(1.2f);
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0.01f, 1f);
        yield return new WaitForSeconds(1f);
        counttext.text = "GO!";
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 100f, 1f);
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(Counttextobj.GetComponent<RectTransform>(), Counttextobj.GetComponent<RectTransform>().localScale * 0, 0.3f);

    }
}
