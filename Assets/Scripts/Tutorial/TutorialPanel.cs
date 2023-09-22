using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    public class TutorialPanel : UIPanel
    {
        public TextMeshProUGUI tutorialText;
        public TextMeshProUGUI tutorialHeader;
        public Image tutorialImage;
        public TextMeshProUGUI tutorialButtonText;
        
#region Tutorial Control
        /// <summary>
        /// Logic for setting the tutorial element
        /// </summary>
        public void LoadTutorial()
        {           
            OpenPanel();
        }

        public void ClickVoiceButton()
        {
#if UNITY_EDITOR
            //voiceManager.TextToSpeech(tutorialText.text);
#else
            //voiceManager.TextToSpeech(_lastTutDescriptionId);
#endif
        }

        public void ClickNextButton() 
        {
            //the close function already checks auto progress();
            //GameManager.instance.GetComponent<TutorialManager>().CheckAutoProgress();
        }

        string _lastTutDescriptionId = "";
        public void LoadTutorial(TutorialElement currTutElement)
        {
            //Set text and image
            tutorialText.text = currTutElement.tutText;
            //tutorialText.text = GameManager.instance.localizationManager.GetContent(currTutElement.tutText);
            tutorialText.text = tutorialText.text.Replace("\\n", "\r\n\n");
            tutorialImage.enabled = currTutElement.tutImage != null;
            tutorialImage.sprite = currTutElement.tutImage;
            tutorialHeader.text = currTutElement.tutHeader;
            _lastTutDescriptionId = currTutElement.tutText;
            OpenPanel();
            if (_textRoutine != null) StopCoroutine(_textRoutine);
            _textRoutine = StartCoroutine(RevealTutorialText(currTutElement.tutText));
        }
    Coroutine _textRoutine;
    IEnumerator RevealTutorialText(string text) 
    {
        string currStr = "";
        int currIndex = 0;
        while (currIndex < text.Length) 
        {
            char c = text[currIndex];
            currStr = text.Substring(0,currIndex) + "<color=#00000000>" + text.Substring(currIndex) + "</color>";
                //string.Concat(currStr, c);
            tutorialText.text = currStr;
            yield return new WaitForSeconds(c=='.' ? 0.06f : 0.02f); //eh it feels better to be smoother.
            currIndex++;
        }
        tutorialText.text = text; //last character
        _textRoutine = null;
        yield return null;
    }
#endregion

        public override void OpenPanel()
        {
            base.OpenPanel();
        }

        public override void ClosePanel()
        {
            base.ClosePanel();
                
            TutorialManager tutMan = FindObjectOfType<TutorialManager>();

            if (tutMan != null) 
            {
                tutMan.TriggerTutorialIteration();
            }
        }

    }
