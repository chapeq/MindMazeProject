using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [FMODUnity.EventRef]
    public string alertSound;
    public PlayerStats player;
    public float timeDamage = 30f;
    public bool isTimerOn;

    private float curTimeDamage;


    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }


    void Start()
    {
        curTimeDamage = 0f;
    }

    public void StartTimer()
    {
        StartCoroutine(TimerDamage());
    }

    public IEnumerator TimerDamage()
    {
        while (isTimerOn)
        {
            yield return new WaitForSeconds(1f);
            curTimeDamage += 1f;
            if (curTimeDamage >= timeDamage)
            {
                FMODUnity.RuntimeManager.PlayOneShot(alertSound);
                player.RemovePtsVie(10);
                curTimeDamage = 0f;
                yield return new WaitForSeconds(2f);
            }
        }

    }


}
