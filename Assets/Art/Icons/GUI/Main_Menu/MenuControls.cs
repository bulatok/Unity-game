using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControls : MonoBehaviour
{

    [SerializeField] RectTransform fader;

    private void Start() {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }
    
    public void PlayPressed(String levelName) {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene(levelName);
        });
    }
    
    public void ExitPressed() {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
