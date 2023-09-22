using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Results : GamePhase
{
    public GameObject fanfareFxPrefabs;
    public Camera fxCamera;
    public override void StartPhase()
    {
        base.StartPhase();

        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Results);
        StartCoroutine(FanfareRoutine());
    }
    IEnumerator FanfareRoutine() 
    {
        yield return new WaitForSeconds(0.1f);
        Vector3[] screenPositions = new Vector3[]
        {
            //new Vector3(Screen.width/2, Screen.height/2), //center
            //new Vector3(0,0), //bottom left
            //new Vector3(Screen.width, 0), //bottom right
            new Vector3(Screen.width/2, Screen.height) //top center
        };
        foreach (Vector3 screenPos in screenPositions)
        {
            Vector3 worldPos = fxCamera.ScreenToWorldPoint(screenPos);
            GameObject obj = Instantiate(fanfareFxPrefabs, worldPos, Quaternion.identity);
            Destroy(obj, 10f);
        }
        
        yield return null;
    }
}
