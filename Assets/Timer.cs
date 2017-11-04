using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider TimeSlider;
    public Slider WaveBar;
    public Image[] Star;
    public GameObject[] StarObj;
    //float timeRemaning = 300.0f;
    string minutes;
    string seconds;
    public Text timerText;
    void Update()
    {
        if (TimeSlider.value > 0)
        {
            TimeSlider.value -= Time.deltaTime;
            WaveBar.value -= Time.deltaTime;
            if (WaveBar.value <= 54)
            {
                Star[0].color = Color.red;
            }
            if (WaveBar.value <= 46)
            {
                StarObj[0].SetActive(false);
            }
            minutes = ((int)TimeSlider.value / 60).ToString();
            seconds = ((int)TimeSlider.value % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
            //timerText.font = (Font)Resources.Load("virgo", typeof(Font));
            if (((int)TimeSlider.value / 60) == 0 && ((int)TimeSlider.value % 60)==40)
            {
                timerText.color = Color.red;
            }
        }
    }
}