using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
	[System.Serializable]
	public class LocalizationWrapper 
	{
		public List<LocalizationElement> content;
	}
	[System.Serializable]
	public class LocalizationElement 
	{
		public string id;
		public List<LocalizationElementContent> content;
	}
	[System.Serializable]
	public class LocalizationElementContent 
	{
		public string language;
		public string content; 
	}
	/*
    {
	"content":[
		{
			"id":"wood_description",
			"content":
			[
				{
					"language":"en",
					"content":"this is wood."
				}
			]
		}
	]
	}*/
}
