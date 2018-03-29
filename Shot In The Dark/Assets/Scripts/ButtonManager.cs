using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void LevelLoad(string Level){
		SceneManager.LoadScene (Level);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
