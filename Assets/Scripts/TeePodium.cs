using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeePodium : MoveableObject
{
    protected override void TriggerBehavior(Collider2D collision)
    {
        //Podium doesn't need to have collisions
    }
}
