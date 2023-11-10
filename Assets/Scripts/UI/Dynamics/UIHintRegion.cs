using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHintRegion : MonoBehaviour
{
    public Animator hintAnimator;
    public Button hintButton;
    bool _isActive;
    public void ActivateHint(bool isActive) 
    {
        _isActive = isActive;
        hintButton.interactable = _isActive;
        hintAnimator.SetBool("OnHover", false);

        if (_isActive)
        {
            GP_Gameplay gameplayPhase = GameManager.instance.GetCurrentGamePhase() as GP_Gameplay;
            if (gameplayPhase)
            {
                if (gameplayPhase.HasHint())
                    gameObject.SetActive(true);
                else
                    gameObject.SetActive(false);
            }
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }


    public void PointerEntered() 
    {
        if (!_isActive)
            return;
        hintAnimator.SetBool("OnHover", true);
    }
    public void PointerExited()
    {
        if (!_isActive)
            return;
        hintAnimator.SetBool("OnHover", false);
    }
    public void HintClicked() 
    {
        GP_Gameplay gameplayPhase = GameManager.instance.GetCurrentGamePhase() as GP_Gameplay;
        if (gameplayPhase) 
        {
            gameplayPhase.RevealHint();
        }
    }
}
