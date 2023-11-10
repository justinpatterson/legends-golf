using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHintRegion : MonoBehaviour
{
    public Animator hintAnimator;
    public void PointerEntered() 
    {
        hintAnimator.SetTrigger("Entered");
    }
    public void PointerExited() 
    {

        hintAnimator.SetTrigger("Exited");
    }
}
