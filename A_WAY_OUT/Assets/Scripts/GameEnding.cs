using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;                     //fadeTime1S
    public float displayImageDuration = 1f;             //waitTime1S
    public GameObject player;                           //Player
    public CanvasGroup exitBackgroundImageCanvasGroup;  //Changing alpha's CanvasGroup

    bool m_IsPlayerAtExit;                              //Exit
    float m_Timer;

    private void OnTriggerEnter(Collider other)
    {
        //if player enter trigger, Exit true
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    private void Update()
    {
        //EndLevel();
        if (m_IsPlayerAtExit)
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        m_Timer = m_Timer + Time.deltaTime; //Frame add time

        //alpha changing
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        //if time bigger than waitTime 
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Application.Quit();//Exit game
        }
    }
}
