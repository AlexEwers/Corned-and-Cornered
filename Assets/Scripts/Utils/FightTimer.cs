using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightTimer : MonoBehaviour
{
    public GameEvent OnEndGame;
    public GameObject sliderObject;
    private Slider slider;

    private float timer;

    private void Awake()
    {
        slider = sliderObject.GetComponent<Slider>();
        timer = slider.maxValue;
        StartTimer();
    }

    public void StartTimer()
    {
        sliderObject.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            slider.value = timer;
            yield return null;
        }

        sliderObject.SetActive(false);
        OnEndGame.Raise();
    }
}
