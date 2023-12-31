using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Elara.MinnowMeadow.Localization;
using SimpleJSON;
using System.IO;
using System;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        //public LocalizationWrapper localizationObject;
        public JSONNode localizationJSON;
        [SerializeField]
        public static string currentLanguage = "en";
        //public Dictionary<string, Dictionary<string, string>> localizationIdMap = new Dictionary<string, Dictionary<string, string>>();
        private void Awake()
        {
            SerializeLocalizationFromResources();
        }

        void SerializeLocalizationFromResources()
        {
            string langFilePath = Path.Combine(Application.streamingAssetsPath, "language.json");
            if (File.Exists(langFilePath))
            {
                string langDataAsJson = File.ReadAllText(langFilePath);
                // The dev payload in language.json includes all languages.
                // Parse this file as JSON, encode, and stringify to mock
                // the platform payload, which includes only a single language.
                // use the languageCode from startGame.json captured above
                localizationJSON = JSON.Parse(langDataAsJson);
                Debug.Log("Localization File Loaded with... " + localizationJSON["en"].Count + " objects.");
                Debug.Log("First localization key is... " + localizationJSON["en"][0]);
            }
            else
            {
                Debug.LogWarning("Couldn't find Language JSON in Streaming Assets.  Using Fallback...");
                TextAsset mytxtData = (TextAsset)Resources.Load("language_fixed");
                string txt = mytxtData.text;
                localizationJSON = JSON.Parse(txt);
            }
        }

        public string GetContent(string contentId, string overrideLanguage) 
        {
            
            return GetContentForLanguage(contentId, overrideLanguage);

        }
        public string GetContent(string contentId)
        {
            string language = "en";
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Spanish:
                    language = "es";
                    break;
                default:
                    language = "en";
                    break;
            }
            return GetContentForLanguage(contentId, language);
        }

        string GetContentForLanguage(string contentId, string language = "en")
        {
            if (localizationJSON.HasKey(language))
            {
                if (localizationJSON[language].HasKey(contentId))
                {
                    if (localizationJSON[language][contentId] != null)
                        return localizationJSON[language][contentId];
                    else
                    {
                        Debug.LogError("Couldn't find key for: " + contentId);
                        return "";
                    }
                }
                else
                    return contentId;
            }
            else 
            {
                return contentId;
            }
            
        }
    }
}