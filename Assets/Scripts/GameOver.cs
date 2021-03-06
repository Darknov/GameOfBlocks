﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    public float restartDelay = 2f;

    Animator anim;
    float restartTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CountDown.timeRemaining >= 120)
        {
            anim.SetTrigger("GameOver1");
            CountDown.started = false;
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(0);
                if (CountDown.timeRemaining != 60)
                {
                    CountDown.timeRemaining = 60;
                }
                StaticOptions.rabbitswin++;

            }
        }

        if (!GameObject.FindWithTag("Player"))
        {
            anim.SetTrigger("GameOver2");
            

            CountDown.started = false;
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(0);
                if (CountDown.timeRemaining != 60)
                {
                    CountDown.timeRemaining = 60;
                }
                StaticOptions.godswin++;
            }
        }
    }
}
