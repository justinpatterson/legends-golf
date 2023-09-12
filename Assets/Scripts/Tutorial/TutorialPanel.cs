using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    public class TutorialPanel : UIPanel
    {
        public TextMeshProUGUI tutorialText;
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
        void Start()
        {

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
            _lastTutDescriptionId = currTutElement.tutText;
            OpenPanel();
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
                tutMan.CheckAutoProgress();
            }
        }

    }
