using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Linq;

public class SGTimerSlider : MonoBehaviour {


    SGTimer timer = new SGTimer();
    private void Start()
    {
        TimerStart(60);
    }


    public void TimerStart(int time)
    {
        GetComponent<Slider>().maxValue = time;
        timer.OnUpdate = (n) => {
            GetComponent<Slider>().value = n;
            gameObject.Child("Text").GetComponent<Text>().text = System.TimeSpan.FromSeconds(n).ToString().Substring(3);
        };
        timer.SetTime(time);
        timer.TimerStart();
    }

}
