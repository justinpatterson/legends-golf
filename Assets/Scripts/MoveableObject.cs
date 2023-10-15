using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D col;
    Vector2 targetPos;
    Vector2 mouseOffset;
    bool _moveInProgress;
    public float movementSpeed = 2f;
    public GameObject radiusInstance;
    public float radiusLimiter = 10f;
    float _radiusLimiterColliderOffset = 0f;
    public MoveableObjectInfoPanel infoPanel;

    public SpriteRenderer interactionCueSprite;
    public Gradient interactionCurveVales; //x is scale, y is local-y, z is rotation
    public float interactionCueOffset = 0f;
    private void Awake()
    {
        interactionCueOffset = UnityEngine.Random.Range(0f, Mathf.PI);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        if (col!=null) _radiusLimiterColliderOffset = col.radius;
    //    PlayPanel.OnInfoButtonClicked += ReportInfo;
    }

    private void ReportInfo(bool toggleState)
    {
        Debug.Log("Info reported...");
        if (infoPanel!=null)
        {
            if(toggleState)
                infoPanel.OpenPanel();
            else 
                infoPanel.ClosePanel();
        }
    }
    private void OnDestroy()
    {
    //    PlayPanel.OnInfoButtonClicked -= ReportInfo;
    }

    private void FixedUpdate()
    {
        if(rb != null && _moveInProgress) 
        {
            rb.MovePosition(rb.position + (targetPos - rb.position)*Time.deltaTime*movementSpeed);
        }
        if (interactionCueSprite != null) 
        {
            float currVal = ((1f + Mathf.Sin(Time.time + interactionCueOffset))/2f);
            Color gradEval = interactionCurveVales.Evaluate(currVal);
            interactionCueSprite.transform.localScale = Vector3.one * gradEval.r;
            interactionCueSprite.color = new Color(1f,1f,1f, gradEval.g);
            //interactionCueSprite.transform.localPosition  = Vector3.up * gradEval.g;  
            interactionCueSprite.transform.Rotate(Vector3.forward * gradEval.b * 360f * Time.deltaTime);
        }
    }
    public void SetMoveState(bool isMoving) 
    { 
        _moveInProgress = isMoving;

        mouseOffset = Camera.main.WorldToScreenPoint(rb.position) - Input.mousePosition;
        
        //todo: fancy animation for radius instance appearing and disappearing
        if (radiusInstance)
            radiusInstance.SetActive(isMoving);
    }
    public void MoveToPosition(Vector2 position, bool isScreenSpace) 
    {
        if(isScreenSpace) 
            position = Camera.main.ScreenToWorldPoint(position + mouseOffset);

        Vector2 offset = position - (Vector2)radiusInstance.transform.position;
        if (offset.magnitude > ( radiusLimiter - _radiusLimiterColliderOffset) ) 
        {
            position = (Vector2)radiusInstance.transform.position + (offset.normalized * (radiusLimiter-_radiusLimiterColliderOffset));
        }
        targetPos = position;
        /*
        Vector2 centerOffset = position - (Vector2)radiusInstance.transform.position;
        Vector2 clamped = (Vector2) radiusInstance.transform.position + (centerOffset.normalized * Mathf.Clamp(centerOffset.magnitude, 0f, radiusLimiter));
        targetPos = Vector2.ClampMagnitude(radiusInstance.transform.position, radiusLimiter);
        */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerBehavior(collision);
    }
    protected virtual void TriggerBehavior(Collider2D collision) 
    {
        if (collision.GetComponent<GravitySim>() != null)
        {
            Debug.Log("TRIGGER HITTING MOVALBE OBJECT");
            GP_Gameplay gp_phase = (GP_Gameplay)GameManager.instance.GetCurrentGamePhase();
            if (gp_phase != null)
            {
                gp_phase.ReportGravityObjectCollision();
            }
        }
    }
}
