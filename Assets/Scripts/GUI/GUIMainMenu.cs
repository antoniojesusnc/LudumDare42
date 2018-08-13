using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMainMenu : MonoBehaviour {

	public void ClickOnNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ClickONExit()
    {
        Application.Quit();
    }

    public void ClickInAudio()
    {
        
    }


}
