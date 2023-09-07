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
            gameObject.SetActive(false);
        }
        
    }
}
