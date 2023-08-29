using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour
{
    public delegate void GoalTrigger(GoalObject goal, Collider2D collision);
    public static GoalTrigger OnGoalTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal triggered!" + collision.name);
        OnGoalTriggered?.Invoke(this, collision);
    }
}
