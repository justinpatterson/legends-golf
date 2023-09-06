using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectMover : MonoBehaviour
{
    public bool isActive = true;
    public MoveableObject objectTarget;

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.currentPhase != GameManager.GamePhases.Gameplay)
            return;
        if((GP_Gameplay) GameManager.instance.GetCurrentGamePhase() != null) 
        {
            GP_Gameplay gpCast = ((GP_Gameplay)GameManager.instance.GetCurrentGamePhase());
            if (gpCast.currentGameplayPhase != GP_Gameplay.GameplayPhases.EditorMode)
                return;
        }
        if (objectTarget == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1500f);
                if (hit && hit.transform.GetComponent<MoveableObject>())
                {
                    objectTarget = hit.transform.GetComponent<MoveableObject>();
                    objectTarget.SetMoveState(true);
                }
            }
        }
        else 
        {
            objectTarget.MoveToPosition(Input.mousePosition, true);

            if (Input.GetMouseButton(0) == false)
            {
                objectTarget.SetMoveState(false);
                objectTarget = null;
            }
        }
    }
}
