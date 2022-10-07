using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    PlayerMove playerMove;
    Slider sliderDashDelay;
    private float dashTimDuration, timeDashControl = 0;

    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        sliderDashDelay = GetComponentInChildren<Slider>();

        sliderDashDelay.maxValue = playerMove.DashDelayValue;
        dashTimDuration = playerMove.DashDelayValue;
    }

    void Update()
    {
        if(playerMove.DashInDelayTime){
            StartCoroutine(SliderDashTime());
        }
        else{
            sliderDashDelay.value = 0;
            timeDashControl = 0;
            StopCoroutine(SliderDashTime());
            sliderDashDelay.gameObject.SetActive(false);
        }
    }


    IEnumerator SliderDashTime(){
        sliderDashDelay.gameObject.SetActive(true);
        sliderDashDelay.transform.eulerAngles = new Vector3(0,0,0);

        timeDashControl  += Time.deltaTime;
        sliderDashDelay.value = Mathf.Lerp(0, dashTimDuration, timeDashControl / dashTimDuration);

        if(timeDashControl >= dashTimDuration){
            playerMove.DashInDelayTime = false;
            yield return null;
        }
    }

}


