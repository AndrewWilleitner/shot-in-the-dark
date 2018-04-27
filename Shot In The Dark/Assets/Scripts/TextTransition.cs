using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTransition : MonoBehaviour {

	Text mainText;
	// quotes are in numerical order of level. so, 0,1,2,3,4,...
	string[] quotes = {
		"Sometimes, life requires a shot in the dark",
		"Sometimes, the way to go isn't always clear",
		"The true adventurer goes forth aimless and uncalculating to meet and greet the unknown fate",
		"There is no darkness, but ignorance",
		"We will sail within a vast sphere, ever drifting in uncertainty, driven from end to end",
		"Information is the resolution of uncertainty",
		"Education is the movement from darkness to light",
		"I have not failed. I've just found 10,000 ways that won't work",
		"The important thing is not to stop questioning. Curiousity has its own reason for existing",
		"It's fine to celebrate success but it is more important to heed the lessons of failure",
		"Mistakes are the portals of discovery",
		"A single moment of understanding can flood a whole life with meaning",
		"You can't cross the sea merely by standing and staring at the water",
		"It does not matter how slowly you go as long as you do not stop",
		"Success is stumbling from failure to failure with no loss of enthusiasm",
		"Failure is success if we learn from it",
		"A wise man adapts himself to circumstances, as water shapes itself to the vessel that contains it",
		"Anyone who has never made a mistake has never tried anything new",
		"Curiousity is the very basis of education and if you tell me that curiousity killed the cat, I say only the cat died nobly",
		"There are no failures - just experiences and your reactions to them"
	};

	// Use this for initialization
	void Start () {
		mainText = transform.GetComponent<Text> ();
		mainText.color = new Color (1, 1, 1, 0);
		StartCoroutine ("FadeText");
	}

	IEnumerator FadeText() 
	{
		int levelNum = PlayerPrefs.GetInt ("CURRENT_LEVEL_NUMBER");
		float textAlpha = 0f;

		if (levelNum >= 0 && levelNum < 20)
			mainText.text = quotes[levelNum];
		else
			mainText.text = "No fancy quote here. Sorry :'(";

		yield return new WaitForSeconds (1);

		while (textAlpha < 1f) 
		{
			textAlpha = textAlpha + (Time.deltaTime / 1);
			mainText.color = new Color (1, 1, 1, textAlpha);
			yield return null;
		}
		textAlpha = 1f;
		mainText.color = new Color (1, 1, 1, 1);

		yield return new WaitForSeconds (6);

		while (textAlpha > 0f) 
		{
			textAlpha = textAlpha - (Time.deltaTime / 1);
			mainText.color = new Color (1, 1, 1, textAlpha);
			yield return null;
		}
		textAlpha = 0f;
		mainText.color = new Color (1, 1, 1, 0);

		if (levelNum >= 0 && levelNum < 10)
			SceneManager.LoadScene ("Level_0"+levelNum);
		else if (levelNum >= 10 && levelNum < 20)
			SceneManager.LoadScene ("Level_"+levelNum);
		else
			SceneManager.LoadScene ("Start_Screen");
		
	}
}
