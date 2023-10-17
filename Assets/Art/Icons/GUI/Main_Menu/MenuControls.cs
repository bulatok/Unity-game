using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControls : MonoBehaviour {
    
    public void PlayPressed(String levelName) {
        SceneManager.LoadScene(levelName);
    }
    
    public void ExitPressed() {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
