using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider HealthSlider;
    public Text HealthPercent;
    public Image HealthIcon;
    public GameObject GameOverUI;
    [FMODUnity.EventRef]
    public string DamageSound;

    private int ptsVie;
    private int ptsVieMax;
    private Animator animator;
    private PlayerController control;


    void Start()
    {
        ptsVie = 100;
        ptsVieMax = 100;
        HealthSlider.maxValue = ptsVieMax;
        HealthSlider.value = ptsVie;
        animator = gameObject.GetComponent<Animator>();
        control = gameObject.GetComponent<PlayerController>();
    }

    public void AddPtsVie(int points)
    {
        ptsVie += points;
        if (ptsVie > ptsVieMax)
            ptsVie = ptsVieMax;
        UpdateUI();
    }

    public void RemovePtsVie(int points)
    {
        if (points > 10)
            TakeDamage();

        ptsVie -= points;
        if (ptsVie < 0)
        {
            ptsVie = 0;
            GameOver();
        }
        UpdateUI(); 
    }

    void TakeDamage()
    {
        FMODUnity.RuntimeManager.PlayOneShot(DamageSound, gameObject.transform.position);
        animator.SetTrigger("Damage");
    }

    void UpdateUI()
    {
        float percentage = (ptsVie % ptsVieMax);
        HealthSlider.value = ptsVie;
        HealthPercent.text = "" + percentage + "%";
        StartCoroutine(ClignoteIcon());
    }

    IEnumerator ClignoteIcon()
    {
        int cpt = 0;
        while (cpt < 3)
        {
            HealthIcon.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.2f);
            HealthIcon.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
            cpt += 1;
        }
    }

    void GameOver()
    {
        animator.SetTrigger("Dead");
        Timer.instance.isTimerOn = false;
        GameOverUI.SetActive(true);
        control.enabled = false;
    }
}
