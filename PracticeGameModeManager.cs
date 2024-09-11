using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PracticeGameModeManager : MonoBehaviour
{
    public int points;

    public GunManager gunManager;

    public GameObject primarySelected;
    public GameObject secondarySelected;

    public GameObject[] equipGun;
    public GameObject[] equipedGun;
    public GameObject[] leftGunIcons;
    public GameObject[] rightGunIcons;

    public bool shopOpened;

    public GameObject gameEndedScreen;

    public Text pointsText;
    public int normalGameModeHS;
    public int flickGameModeHS;
    public int trackingGameModeHS;
    public Text highScoreText;
    public Text highScoreTextEnd;
    public Text endPointsText;

    public GameObject pointsGameObject;

    float currentTime = 0f;
    float startingTime = 60f;

    [SerializeField] Text countdownText;

    public Slider timeSlider;

    public Text bonesEarnedText;

    public GameObject[] buttons;


    void Start () {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            normalGameModeHS = PlayerPrefs.GetInt("normal");
            highScoreText.text = "High Score: " + normalGameModeHS.ToString();
        } else if (SceneManager.GetActiveScene().buildIndex == 2) {
            flickGameModeHS = PlayerPrefs.GetInt("flick");
            highScoreText.text = "High Score: " + flickGameModeHS.ToString();
        } else if (SceneManager.GetActiveScene().buildIndex == 3) {
            trackingGameModeHS = PlayerPrefs.GetInt("tracking");
            highScoreText.text = "High Score: " + trackingGameModeHS.ToString();
        }

        currentTime = startingTime;
    }

    void Update () {
        if (shopOpened == true) {

            for (int i = 0; i < equipGun.Length; i++) {
                    if (i == gunManager.primaryGun || i == gunManager.secondaryGun) {
                    equipGun[i].SetActive(false);
                    equipedGun[i].SetActive(true);
                } else {
                    equipGun[i].SetActive(true);
                    equipedGun[i].SetActive(false);
                }
            }

            for (int i = 0; i < rightGunIcons.Length; i++) {
                if (i == gunManager.primaryGun) {
                    leftGunIcons[i].SetActive(true);
                } else {
                    leftGunIcons[i].SetActive(false);
                }

                if (i == gunManager.secondaryGun) {
                    rightGunIcons[i].SetActive(true);
                } else {
                    rightGunIcons[i].SetActive(false);
                }
            }
        } else {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 0) {
                currentTime = 0;
            }

            timeSlider.value = currentTime;
        }

        if (gunManager.gun == gunManager.primaryGun) {
            primarySelected.SetActive(true);
            secondarySelected.SetActive(false);
        } else {
            secondarySelected.SetActive(true);
            primarySelected.SetActive(false);
        }
    }

    public void SelectGunToSwitch() {
        if (gunManager.gun == gunManager.primaryGun && gunManager.secondaryGun != 6) {
            gunManager.gun = gunManager.secondaryGun;
            primarySelected.SetActive(false);
            secondarySelected.SetActive(true);

            for (int i = 0; i < gunManager.gunTypes.Length; i++) {
                if (i == gunManager.gun) {
                    gunManager.gunTypes[i].SetActive(true);
                } else {
                    gunManager.gunTypes[i].SetActive(false);
                }
            }
        } else {
            gunManager.gun = gunManager.primaryGun;
            primarySelected.SetActive(true);
            secondarySelected.SetActive(false);

            for (int i = 0; i < gunManager.gunTypes.Length; i++) {
                if (i == gunManager.gun) {
                    gunManager.gunTypes[i].SetActive(true);
                } else {
                    gunManager.gunTypes[i].SetActive(false);
                }
            }
        }
    }

    public void StartGame () {
        shopOpened = false;

        FindObjectOfType<MobileLook>().canLookAround = true;

        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(true);
        }

        StartCoroutine (PlayingGame());
    }

    IEnumerator PlayingGame () {

        yield return new WaitForSeconds(60);

        Time.timeScale = 0;

        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(false);
        }

        FindObjectOfType<SoundManager>().StopPlayerSounds();

        FindObjectOfType<MobileLook>().canLookAround = false;

        pointsGameObject.SetActive(false);

        gameEndedScreen.SetActive(true);

        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (points > normalGameModeHS) {
                normalGameModeHS = points;
                PlayerPrefs.SetInt("normal", normalGameModeHS);
            }

            highScoreTextEnd.text = "High Score: " + normalGameModeHS.ToString();

        } else if (SceneManager.GetActiveScene().buildIndex == 2) {
            if (points > flickGameModeHS) {
                flickGameModeHS = points;
                PlayerPrefs.SetInt("flick", flickGameModeHS);
            }

            highScoreTextEnd.text = "High Score: " + flickGameModeHS.ToString();

        } else if (SceneManager.GetActiveScene().buildIndex == 3) {
            if (points > trackingGameModeHS) {
                trackingGameModeHS = points;
                PlayerPrefs.SetInt( "tracking", trackingGameModeHS);
            }

            highScoreTextEnd.text = "High Score: " + trackingGameModeHS.ToString();

        }

        int bonesEarned = 0;

        if (SceneManager.GetActiveScene().buildIndex == 1) {
            bonesEarned = points * 5;
        } else if (SceneManager.GetActiveScene().buildIndex == 2) {
            bonesEarned = (int)(points * 1.75f);
        } else if (SceneManager.GetActiveScene().buildIndex == 3) {
            bonesEarned = points * 7;
        }

        FindObjectOfType<SkinShop>().AddMoney(bonesEarned);

        bonesEarnedText.text = "Bones Eaned: " + bonesEarned.ToString();

        endPointsText.text = "Targets Destroyed: " + points.ToString();

        
    }

    public void AddPoint () {
        points += 1;

        pointsText.text = points.ToString();
    }
}
