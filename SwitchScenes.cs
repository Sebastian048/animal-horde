using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public OptionsStyle scene;

    public Animator transition;

    public GameObject selectDifficulty;

    void Start () { 
        transition.Play("EndLoadingScreen");
    }

    public void Play () {
        StartCoroutine (LoadScene(scene.selectedMap));
    }

    IEnumerator LoadScene (int buildSceneNum) {

        transition.SetTrigger("start");

        transition.SetTrigger("end");

        Time.timeScale = 1;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(buildSceneNum);
    }

    public void ReturnToLobby () {
        StartCoroutine (BackToMainMenu(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator BackToMainMenu (int buildSceneNum) {

        transition.SetTrigger("end");

        Time.timeScale = 1;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }

    public void Retry () {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void OldGame () {
        StartCoroutine (LoadScene(5));
    }

    public void PlayButton () {
        if (scene.selectedGM == 0) {
            selectDifficulty.SetActive(true);
        } else {
            StartCoroutine (LoadScene(scene.selectedMap));
        }
    }
}
