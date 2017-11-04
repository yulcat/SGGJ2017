using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Linq;

public class SGTimerSlider : MonoBehaviour {


    public SGTimer timer = new SGTimer();

    public void SetTimerText(int time)
    {
        gameObject.Child("Text").GetComponent<Text>().text = System.TimeSpan.FromSeconds(time).ToString().Substring(3);
        GetComponent<Slider>().maxValue = time;
        GetComponent<Slider>().value = time;
    }

    public void TimerStart(int time)
    {
        GetComponent<Slider>().maxValue = time;
        GetComponent<Slider>().value = time;
        timer.OnUpdate = (n) => {
            GetComponent<Slider>().value = n;
            gameObject.Child("Text").GetComponent<Text>().text = System.TimeSpan.FromSeconds(n).ToString().Substring(3);
            SGGameManager.Instance.OnPlayTime(n);
        };
        timer.SetTime(time);
        timer.TimerStart();
    }

    public void TimerStop()
    {
        timer.TimerStop();
    }

    public int GetRemainTime()
    {
        return timer.CurrentTime;
    }

}
