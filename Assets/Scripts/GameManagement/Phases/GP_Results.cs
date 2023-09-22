using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Results : GamePhase
{
    public GameObject fanfareFxPrefabs;

    public override void StartPhase()
    {
        base.StartPhase();

        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Results);
        StartCoroutine(FanfareRoutine());
    }
    IEnumerator FanfareRoutine() 
    {
        yield return new WaitForSeconds(0.1f);
        //Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2));
        //GameObject fanfareInst = Instantiate(fanfareFxPrefabs, pos, Quaternion.identity);
        Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(0,0));
        GameObject fanfareInst2 = Instantiate(fanfareFxPrefabs, pos2, Quaternion.identity);
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0));
        GameObject fanfareInst3 = Instantiate(fanfareFxPrefabs, pos3, Quaternion.identity);
        //Destroy(fanfareInst, 10f);
        Destroy(fanfareInst2, 10f);
        Destroy(fanfareInst3, 10f);
        yield return null;
    }
}
