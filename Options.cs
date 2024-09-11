using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GunManager gunManager;
    public GameObject optionsMenu;

    public bool paused;

    public Camera camEnvironment;
    public Slider environmentFOV;
    public float numEnvironmentFOV;
    public Text environmentFOVText;
    public int damageNumbersNum;
    public int healthBarNum;

    public int enterFirst;

    public Slider sensSlider;
    public Text sensText;
    public Slider sensSliderADS;
    public Text sensTextADS;

    public Slider sensSliderSniper;
    public Text sensSniperText;

    public float sens;
    public float sensADS;
    public float sensSniper;

    public int holdToAds;

    public float adsSpeed;

    public GameObject[] holdToAdsB;
    public GameObject[] damageNumB;
    public GameObject[] healthBarB;

    public bool menu;

    public GameObject[] fpsBG;
    public int fps;

    void Start () {
        enterFirst = PlayerPrefs.GetInt("firstTime");

        if (enterFirst == 0) {
            enterFirst = 1;
            PlayerPrefs.SetInt("firstTime", enterFirst);
            sens = 15;
            sensADS = 10;
            sensSniper = 3;
            numEnvironmentFOV = 60;
            damageNumbersNum = 1;
            damageNumB[1].SetActive(true);
            damageNumB[2].SetActive(true);
            holdToAds = 0;
            holdToAdsB[0].SetActive(true);
            holdToAdsB[3].SetActive(true);
            healthBarNum = 1;
            healthBarB[1].SetActive(true);
            healthBarB[2].SetActive(true);

            int screenHZ = Screen.currentResolution.refreshRate;

            if (screenHZ <= 61) {
                ChangeFps(0);
            } else if (screenHZ <= 76) {
                ChangeFps(1);
            } else if (screenHZ <= 91) {
                ChangeFps(2);
            } else {
                ChangeFps(3);
            }
        } else {
        sens = PlayerPrefs.GetFloat("normalSens");
        sensADS = PlayerPrefs.GetFloat("ADSSens");
        sensSniper = PlayerPrefs.GetFloat("SniperSens");
        numEnvironmentFOV = PlayerPrefs.GetFloat("envFOV");

        damageNumbersNum = PlayerPrefs.GetInt("damageNum");

        if (damageNumbersNum == 1) {
            damageNumB[0].SetActive(false);
            damageNumB[1].SetActive(true);
            damageNumB[2].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            damageNumB[3].GetComponent<Text>().color = new Color(1, 1, 1);
        } else {
            damageNumB[0].SetActive(true);
            damageNumB[1].SetActive(false);
            damageNumB[3].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            damageNumB[2].GetComponent<Text>().color = new Color(1, 1, 1);
        }

        holdToAds = PlayerPrefs.GetInt("holdforads");
        if (holdToAds == 0) {
            holdToAdsB[0].SetActive(true);
            holdToAdsB[1].SetActive(false);
            holdToAdsB[3].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            holdToAdsB[2].GetComponent<Text>().color = new Color(1, 1, 1);
        } else {
            holdToAdsB[0].SetActive(false);
            holdToAdsB[1].SetActive(true);
            holdToAdsB[2].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            holdToAdsB[3].GetComponent<Text>().color = new Color(1, 1, 1);
        }

        healthBarNum = PlayerPrefs.GetInt("healthBar");
        if (healthBarNum == 0) {
            healthBarB[0].SetActive(true);
            healthBarB[1].SetActive(false);
            healthBarB[3].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            healthBarB[2].GetComponent<Text>().color = new Color(1, 1, 1);
        } else {
            healthBarB[0].SetActive(false);
            healthBarB[1].SetActive(true);
            healthBarB[2].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            healthBarB[3].GetComponent<Text>().color = new Color(1, 1, 1);
        }

        fps = PlayerPrefs.GetInt("frames");

        if (fps == 60) {
            fpsBG[0].SetActive(true);
            fpsBG[1].SetActive(false);
            fpsBG[2].SetActive(false);
            fpsBG[3].SetActive(false);

        } else if (fps == 75) {
            fpsBG[0].SetActive(false);
            fpsBG[1].SetActive(true);
            fpsBG[2].SetActive(false);
            fpsBG[3].SetActive(false);
        } else if (fps == 90) {
            fpsBG[0].SetActive(false);
            fpsBG[1].SetActive(false);
            fpsBG[2].SetActive(true);
            fpsBG[3].SetActive(false);
        } else if (fps == 120) {
            fpsBG[0].SetActive(false);
            fpsBG[1].SetActive(false);
            fpsBG[2].SetActive(false);
            fpsBG[3].SetActive(true);
        }

        }

        sensSlider.value = sens;
        sensSliderADS.value = sensADS;
        sensSliderSniper.value = sensSniper;
        environmentFOV.value = numEnvironmentFOV;

        camEnvironment.fieldOfView = numEnvironmentFOV;

        Application.targetFrameRate = fps;

        SavePreferences();
    }

    public void SavePreferences () {
        PlayerPrefs.SetFloat("normalSens", sens);
        PlayerPrefs.SetFloat("ADSSens", sensADS);
        PlayerPrefs.SetFloat("SniperSens", sensSniper);
        PlayerPrefs.SetFloat("envFOV", numEnvironmentFOV);
        PlayerPrefs.SetInt ("holdforads", holdToAds);
        PlayerPrefs.SetInt ("damageNum", damageNumbersNum);
        PlayerPrefs.SetInt ("healthBar", healthBarNum);
    }

    void Update () {
        PlayerPrefs.SetInt("firstTime", enterFirst);
        if (paused == true) {

        sens = Mathf.Round(sensSlider.value);
        sensText.text = sens.ToString();
        sensADS = Mathf.Round(sensSliderADS.value);
        sensTextADS.text = sensADS.ToString();
        sensSniper = Mathf.Round(sensSliderSniper.value);
        sensSniperText.text = sensSniper.ToString();
        numEnvironmentFOV = Mathf.Round(environmentFOV.value);
        environmentFOVText.text = numEnvironmentFOV.ToString();
        camEnvironment.fieldOfView = numEnvironmentFOV;
        }
        
    }

    public void Pause () {
        paused = true;
        Time.timeScale = 0f;
 
        if (menu == false) {
            for (int i = 0; i < FindObjectOfType<SoundManager>().playerSounds.Length; i++) {
            FindObjectOfType<SoundManager>().playerSounds[i].Stop();
        }
        }

    }

    public void UnPause () {
        paused = false;
        Time.timeScale = 1f;

        int soundToPlay = 0;
        
        if (menu == false) {
        for (int i = 0; i < FindObjectOfType<SoundManager>().playerSounds.Length; i++) {

            if (FindObjectOfType<SoundManager>().soundPlayingCheck[i] == true){
            soundToPlay = i;
            }

            FindObjectOfType<SoundManager>().playerSounds[soundToPlay].Play();
        }
        }
    }

    public void Ads () {
        if (adsSpeed >= 1f) {
            adsSpeed = 1f;
        } else {
            adsSpeed = adsSpeed + Time.deltaTime * 3;
        }
        if (gunManager.gun != 3) {
           camEnvironment.fieldOfView = Mathf.Lerp(environmentFOV.value, 30, adsSpeed);
       } else {
           camEnvironment.fieldOfView = Mathf.Lerp(environmentFOV.value, 15, adsSpeed);
       }
    }
    public void UnAds() {
        if (adsSpeed <= 0f) {
            adsSpeed = 0f;
        } else {
            adsSpeed = adsSpeed - Time.deltaTime * 3;
        }
       if (gunManager.gun != 3) {
           camEnvironment.fieldOfView = Mathf.Lerp(environmentFOV.value, 30, adsSpeed);
       } else {
           camEnvironment.fieldOfView = Mathf.Lerp(environmentFOV.value, 15, adsSpeed);
       }
    }

    public void DamageNumberChange () {
        if (damageNumbersNum == 1) {
            damageNumbersNum = 0;
            damageNumB[0].SetActive(true);
            damageNumB[1].SetActive(false);
            damageNumB[3].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            damageNumB[2].GetComponent<Text>().color = new Color(1, 1, 1);
        } else {
            damageNumbersNum = 1;
            damageNumB[0].SetActive(false);
            damageNumB[1].SetActive(true);
            damageNumB[2].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            damageNumB[3].GetComponent<Text>().color = new Color(1, 1, 1);
        }
    }

    public void HoldForAds () {
        if (holdToAds == 0) {
            holdToAds = 1;
            holdToAdsB[0].SetActive(false);
            holdToAdsB[1].SetActive(true);
            holdToAdsB[2].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            holdToAdsB[3].GetComponent<Text>().color = new Color(1, 1, 1);
        } else {
            holdToAds = 0;
            holdToAdsB[0].SetActive(true);
            holdToAdsB[1].SetActive(false);
            holdToAdsB[3].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            holdToAdsB[2].GetComponent<Text>().color = new Color(1, 1, 1);
        }
    }

    public void HealthBarOption () {
        if (healthBarNum == 1) {
            healthBarNum = 0;
            healthBarB[0].SetActive(true);
            healthBarB[1].SetActive(false);
            healthBarB[3].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            healthBarB[2].GetComponent<Text>().color = new Color(1, 1, 1);
        } else {
            healthBarNum = 1;
            healthBarB[0].SetActive(false);
            healthBarB[1].SetActive(true);
            healthBarB[2].GetComponent<Text>().color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
            healthBarB[3].GetComponent<Text>().color = new Color(1, 1, 1);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].GetComponent<Enemy>().healthBarAcitvation(healthBarNum);
        }
    }

    public void ChangeFps (int fpsToChange) {

        for(int i = 0; i < fpsBG.Length; i++) {
            fpsBG[i].SetActive(false);
        }
        fpsBG[fpsToChange].SetActive(true);

        if (fpsToChange == 0) {
            fps = 60;
        } else if (fpsToChange == 1) {
            fps = 75;
        } else if (fpsToChange == 2) {
            fps = 90;
        } else if (fpsToChange == 3) {
            fps = 120;
        }

        PlayerPrefs.SetInt("frames", fps);

        Application.targetFrameRate = fps;
    }
}
