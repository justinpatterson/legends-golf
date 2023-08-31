using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySim : MonoBehaviour
{
    [SerializeField]
    Vector2 attackDir = Vector3.right;
    [SerializeField]
    float attackStrength = 1f;
    [SerializeField]
    float maxAttackStrength = 100f;
    Rigidbody2D rb;

    public Vector2 currentForce = Vector2.zero;
    public Rigidbody2D[] gravityObjs; //lets just say the scale of the planet indicates its mass for now
    //float decayFactor -- meh, don't need it for now
    bool _inMotion = false;

    public LineRenderer lineRenderer;
    public GameObject hitIndicator;

    Vector2 _startPos = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        _startPos = transform.position;
        ResetObject();
    }
    private void Start()
    {
        //if (hitIndicator)
        //    hitIndicator.transform.parent = null; //so that it doesn't get effected by this object's rotation
        //Shoot();
    }
    private void OnDestroy()
    {
        //if (hitIndicator)
        //    Destroy(hitIndicator);
    }

    private void Update()
    {
        if (!_inMotion) 
        {
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(new Vector3(0, 0, h));
            attackDir = transform.right;


            if (Input.GetKeyDown(KeyCode.UpArrow))
                attackStrength++;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                attackStrength--;
            attackStrength = Mathf.Clamp(attackStrength, 0f, maxAttackStrength);

            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }
        else
        {
        }
    }
    private void FixedUpdate()
    {
        if (!_inMotion) { VisualizeTrajectory(); }
        else
        {
            currentForce = AddGravityObjectForceInfluences(currentForce, transform.position, 1f);
            rb.MovePosition(rb.position += currentForce*Time.deltaTime);
        }
    }

    Vector3 AddGravityObjectForceInfluences(Vector3 initialForce, Vector2 objectPos, float iterationScale = 1f)
    {
        Vector3 force = initialForce;
        foreach (Rigidbody2D r in gravityObjs)
        {
            Vector2 nextForce = force;
            Vector2 gravDir = (r.position - objectPos);
            float radius = gravDir.magnitude;
            nextForce +=  (gravDir.normalized * (9.8f * ((rb.mass * r.mass) / radius * radius))) * (iterationScale); //visualize how far it moves over the first second
            force = nextForce;
        }
        force = Vector3.ClampMagnitude(force, maxAttackStrength);
        return force;
    }

    void VisualizeTrajectory(int maxIterations = 100, int resolutionMod = 2)
    {
        Vector3 pos = transform.position; 
        Vector3 force = attackDir * attackStrength;
        
        if(!lineRenderer.enabled) lineRenderer.enabled = true;
        
        lineRenderer.SetPosition(0, pos);
        if(lineRenderer.positionCount < maxIterations) { lineRenderer.positionCount = maxIterations; }
        bool obstacleOverlap = false;
        bool goalOverlap = false;
        for (int i = 1; i < maxIterations; i++) 
        {
            if (!obstacleOverlap && !goalOverlap)
            {
                force = AddGravityObjectForceInfluences(force, pos, 1f);
                pos += force*Time.deltaTime;
                obstacleOverlap |= CheckGravObjOverlap(pos);
                goalOverlap |= CheckGoalOverlap(pos);
                //if (i%resolutionMod == 0 || overlap) //I thought I could make it do every other position, but not initially working. Will review later.
                lineRenderer.SetPosition(i, pos);
            }
            else 
            {
                lineRenderer.SetPosition(i, pos); //this probably will have visual wonkiness.  Perhaps resize line positions array to "i" when overlap becomes true.
            }

        }

        if (obstacleOverlap) 
        {
            hitIndicator.transform.position = pos;
            hitIndicator.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (goalOverlap)
        {
            hitIndicator.transform.position = goalCollider.transform.position;//pos;
            hitIndicator.GetComponent<SpriteRenderer>().color = Color.green;
        }
        hitIndicator.SetActive(obstacleOverlap||goalOverlap);
    }

    bool CheckGravObjOverlap(Vector2 pos) 
    {
        foreach(Rigidbody2D r in gravityObjs) 
        {
            CircleCollider2D col = r.GetComponent<CircleCollider2D>();
            if (col.OverlapPoint(pos))
                return true;
            //float mg = (r.position - pos).magnitude;
            //if (mg <= col.radius)
            //    return true;
        }
        return false;
    }
    public Collider2D goalCollider = null;
    bool CheckGoalOverlap(Vector2 pos)
    {
        if (goalCollider == null)
            return false;
        return goalCollider.OverlapPoint(pos);

    }

    void ShowTrajectory()
    {
        lineRenderer.enabled = true;
        hitIndicator.SetActive(false);  //just in case
    }
    void HideTrajectory()
    {
        for (int i = 1; i < 100; i++)
        {
            lineRenderer.SetPosition(i, Vector3.zero);
        }
        lineRenderer.enabled = false;
        hitIndicator.SetActive(false);
    }
    public void Shoot() 
    {
        _inMotion = true;
        currentForce = attackDir*attackStrength; 

        if (lineRenderer.enabled) lineRenderer.enabled = false;
    }
    public void ResetObject()
    {
        _inMotion = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.MovePosition(_startPos);
    }
}
