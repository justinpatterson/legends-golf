using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntroPanel : UIPanel
{
    public GameObject[] slides;
    public Button nextButton, lastButton;
    int currentSlide = 0;
    public override void OpenPanel()
    {
        base.OpenPanel();
        currentSlide = 0;
        RevealSlide(currentSlide);
    }
    public void NextSlide() 
    {
        currentSlide++;
        currentSlide = Mathf.Clamp(currentSlide, 0, slides.Length);
        if (currentSlide < slides.Length)
        {
            RevealSlide(currentSlide);
        }
        else
            ClosePanel();

    }
    public void PreviousSlide() 
    {
        currentSlide--;
        currentSlide = Mathf.Clamp(currentSlide, 0, slides.Length);
        RevealSlide(currentSlide);
    }
    public void RevealSlide(int index) 
    {
        for (int i = 0; i < slides.Length; i++) 
        {
            GameObject go = slides[i];
            go.SetActive(i == index);
        }
        nextButton.interactable = (index < slides.Length-1);
        lastButton.interactable = (index > 0);
    }
}
