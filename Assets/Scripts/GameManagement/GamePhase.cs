using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhase : MonoBehaviour
{
    protected bool _inProgress;
    public virtual void StartPhase() { _inProgress = true; }
    public virtual void UpdatePhase() { }
    public virtual void EndPhase() { _inProgress = false; }
}
