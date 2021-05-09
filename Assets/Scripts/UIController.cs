using UnityEngine;
using System.Collections;


public class UIController : MonoBehaviour
{
    public GameObject DangerUI;
    [FMODUnity.EventRef]
    public string lightSound;
    public GameObject coneCollider;

    public void DangerUIOn()
    {
        DangerUI.SetActive(true);
    }
    public void DangerUIOff()
    {
        DangerUI.SetActive(false);
    }

    public void LightOn()
    {
        FMODUnity.RuntimeManager.PlayOneShot(lightSound);
        StartCoroutine(lightAttack());
    }

    IEnumerator lightAttack()
    {
        coneCollider.SetActive(true);
        yield return new WaitForSeconds(2f);
        coneCollider.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }


}
