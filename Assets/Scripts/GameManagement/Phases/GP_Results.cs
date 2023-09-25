using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Results : GamePhase
{
    public GameObject fanfareFxPrefabs;
    public Camera fxCamera;
    List<GameObject> _fxInstances = new();
    Coroutine _fxInstancesCoroutine = null;
    public override void StartPhase()
    {
        base.StartPhase();

        AudioManager.instance.TriggerMusic(AudioManager.AudioKeys.Results);

        if(_fxInstancesCoroutine != null) { 
            StopCoroutine(_fxInstancesCoroutine);
            ClearFxInstances();
            _fxInstancesCoroutine = null;
        }
        _fxInstancesCoroutine = StartCoroutine(FanfareRoutine());
    }
    public override void EndPhase()
    {
        base.EndPhase();
        if (_fxInstancesCoroutine != null)
        {
            StopCoroutine(_fxInstancesCoroutine);
            ClearFxInstances();
            _fxInstancesCoroutine = null;
        }
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
            worldPos.z = 0;
            GameObject obj = Instantiate(fanfareFxPrefabs, worldPos, Quaternion.identity);
            _fxInstances.Add(obj);
        }
        yield return new WaitForSeconds(10f);
        ClearFxInstances();
        yield return null;
    }
    void ClearFxInstances() 
    {
        if (_fxInstances.Count==0)
            return;

        for (int i= _fxInstances.Count-1; i >= 0 ; i--){
            Destroy(_fxInstances[i]);
        }
        _fxInstances.Clear();
    }
}
