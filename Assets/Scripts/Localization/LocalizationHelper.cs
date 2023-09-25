using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Localization {
    public class LocalizationHelper : MonoBehaviour
    {
        public string localizationId;
        public TextMeshProUGUI ProTextTarget;

        void Start()
        {
            if (ProTextTarget == null)
            {
                ProTextTarget = GetComponent<TextMeshProUGUI>();
            }
            DoLocalization();
        }
        void DoLocalization() 
        {
            string targetLanguageText = GameManager.instance.localizationManager.GetContent(localizationId);
            if (targetLanguageText.Length == 0) { }
            else
            {   
                if (ProTextTarget != null)
                    ProTextTarget.text = (targetLanguageText);

                Debug.Log("Should have set localization text to - " + targetLanguageText);
            }
        }
        public void RefreshLocalization(string inLocId) 
        {
            localizationId = inLocId;
            DoLocalization();
        }
    }
}