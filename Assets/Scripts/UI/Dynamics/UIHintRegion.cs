using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHintRegion : MonoBehaviour
{
    public Animator hintAnimator;
    public Button hintButton;
    bool _isActive;
    private void Awake()
    {
        //since you start out _isActive = false;
        transform.localScale = Vector3.zero;
    }
    public void ActivateHint(bool isActive) 
    {
        bool activeSwap = (_isActive != isActive);

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

        if (activeSwap)
        {
            if (isActive)
            {
                RevealHint();
            }
            else
            {
                transform.localScale = Vector3.zero;
            }
        }
    }
    private void OnDisable()
    {
        if (revealRoutine != null) {
            StopCoroutine(revealRoutine);
            revealRoutine = null;
        }
    }
    Coroutine revealRoutine;
    void RevealHint() 
    {
        if (revealRoutine != null)
        {
            StopCoroutine(revealRoutine);
            revealRoutine = null;
        }
        revealRoutine = StartCoroutine(RevealRoutine());
    }
    IEnumerator RevealRoutine() 
    {
        float t = 0f;
        float max = 0.3f;
        float progress = 0f;
        while (t<max) 
        {
            t+=Time.deltaTime;
            progress = t/max;
            transform.localScale = Vector3.one * (progress);
            yield return new WaitForEndOfFrame();
        }
        revealRoutine = null;
        yield return null;
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
            hintButton.interactable = false;
        }
    }
}
