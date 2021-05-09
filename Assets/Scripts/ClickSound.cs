using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string clickSound;

    public void OnCLick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(clickSound);
    }

   
}
