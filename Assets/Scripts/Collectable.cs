using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField]
    public CollectableInfo collectableInfo;
    [System.Serializable]
    public struct CollectableInfo 
    {
        public string id;
        public int amt;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<GravitySim>() != null) 
        {
            //COLLECT!
            GP_Gameplay gp_phase = (GP_Gameplay)GameManager.instance.GetCurrentGamePhase();
            if (gp_phase != null) 
            {
                gp_phase.ReportCollectibleGathered(collectableInfo.id, collectableInfo.amt);
            }
            StartCoroutine(FadeOutRoutine());
            GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator FadeOutRoutine() 
    {
        float time = 0f;
        float exitTime = 0.333f;
        float percent = 0f;
        while (percent <= 1f) 
        {
            time+=Time.deltaTime;
            percent = time / exitTime;
            percent = Mathf.Clamp(percent,0f, 1f);
            transform.Rotate(Vector3.up * Time.deltaTime * 360f * (1f/exitTime));
            transform.localScale = Vector3.one * (1f - percent);
            transform.position += Vector3.up * Time.deltaTime * 2f * (1f/exitTime);
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
        yield return null; 
    }
}
