using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int totalHealth;

    public Text healthText;
    public Slider healthSlider;
    public float healthLerp;

    public bool practice;

    public GameObject deathScreen;
    public GameObject[] buttons;

    public int enemiesEliminated;
    public int damageDealt;
    public int pointsAccumulated;

    public Text[] stats;

    int moneyAccumulated;

    public InterstitialAd iAds;

    private GameObject doubleMoneyButton;

    public int highScoreEasy;
    public int highScoreMedium;
    public int highScoreHard;
    public Text highScoreText;
    public Text highScoreTitle;

    public Text[] highScoreTextMenu;

    public GameObject hitShow;
    public GameObject testCube;

    public Animator anim;

    public Animator enemyFollowAnim;
    public GameObject enemyThatJustHit;

    public SkinShop skinShop;
    
    public bool realGame;

    public GameObject player;

    public AudioSource hitPlayer;
    public AudioClip clipHit;

    void Start () {
        highScoreEasy = PlayerPrefs.GetInt("easyHighScore");
        highScoreMedium = PlayerPrefs.GetInt("mediumHighScore");
        highScoreHard = PlayerPrefs.GetInt("hardHighScore");

        if (SceneManager.GetActiveScene().buildIndex == 0) {
            highScoreTextMenu[0].text = highScoreEasy.ToString();
            highScoreTextMenu[1].text = highScoreMedium.ToString();
            highScoreTextMenu[2].text = highScoreHard.ToString();
        }

        if (realGame == true) {
            health = health + skinShop.healthBoost;
            totalHealth = health;
            healthText.text = "HP: " + health.ToString() + "/" + totalHealth;
            healthSlider.maxValue = totalHealth;
            healthSlider.value = totalHealth;
            iAds.LoadAd();
        }
    }

    public void RecieveDamage (int damage, GameObject enemy) {
        if (health > 0) {
            health -= damage;
            healthText.text = "HP: " + health.ToString() + "/" + totalHealth;

            enemyThatJustHit = enemy;
            enemyFollowAnim.Play("enemyJustHitPlayer",-1, 0f);

            DamageImpact();

            hitPlayer.PlayOneShot(clipHit);

            if (health <= 0f) {
                Died();
            }
        }
    }

    void FixedUpdate () {


        if (practice == false) {
            if (enemyThatJustHit != null) {
                testCube.transform.LookAt(enemyThatJustHit.transform);
            }
            hitShow.transform.localRotation = Quaternion.Euler (0, 0, -testCube.transform.eulerAngles.y + player.transform.localEulerAngles.y);

            if (healthLerp <= health) {
                healthLerp =  healthLerp + (totalHealth / 4) * Time.deltaTime;
            } 
            if (healthLerp >= health) {
                healthLerp =  healthLerp - (totalHealth / 4) * Time.deltaTime;
            }
            healthSlider.value = healthLerp;
        }
    }

    public void Died () {
        deathScreen.SetActive(true);
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(false);
        }

        if (FindObjectOfType<DifficultySelection>().difficulty == "easy") {
            moneyAccumulated = (int) (pointsAccumulated / 10);

            highScoreTitle.text = "High Score In Easy Mode:";

            if (FindObjectOfType<WaveSpawner>().waveNum > highScoreEasy) {
            highScoreEasy = FindObjectOfType<WaveSpawner>().waveNum;
            PlayerPrefs.SetInt("easyHighScore", highScoreEasy);
            }

            highScoreText.text = highScoreEasy.ToString();

        } else if (FindObjectOfType<DifficultySelection>().difficulty == "medium") {
            moneyAccumulated = (int) (pointsAccumulated / 5);

            highScoreTitle.text = "High Score In Medium Mode:";
            
            if (FindObjectOfType<WaveSpawner>().waveNum > highScoreMedium) {
            highScoreMedium = FindObjectOfType<WaveSpawner>().waveNum;
            PlayerPrefs.SetInt("mediumHighScore", highScoreMedium);
            }

            highScoreText.text = highScoreMedium.ToString();
            
        } else if (FindObjectOfType<DifficultySelection>().difficulty == "hard") {
            moneyAccumulated = (int) (pointsAccumulated / 2);

            highScoreTitle.text = "High Score In Hard Mode:";
            
            if (FindObjectOfType<WaveSpawner>().waveNum > highScoreHard) {
            highScoreHard = FindObjectOfType<WaveSpawner>().waveNum;
            PlayerPrefs.SetInt("hardHighScore", highScoreHard);
            }

            highScoreText.text = highScoreHard.ToString();
            
        }

        FindObjectOfType<SoundManager>().StopPlayerSounds();

        FindObjectOfType<SkinShop>().AddMoney(moneyAccumulated);

        stats[0].text = enemiesEliminated.ToString();
        stats[1].text = FindObjectOfType<WaveSpawner>().waveNum.ToString();
        stats[2].text = pointsAccumulated.ToString();
        stats[3].text = moneyAccumulated.ToString();

        FindObjectOfType<MobileLook>().canLookAround = false;
        FindObjectOfType<GunManager>().NoShootVoid();
        FindObjectOfType<GunManager>().UnAdsVoid();


        /*int chanceToGetAnAd = Random.Range(0, 11);

        if (chanceToGetAnAd > -1) {
            iAds.ShowAd();
        }*/

        Time.timeScale = 0;
    }

    public void AddEnemiesEliminated() {
        enemiesEliminated += 1;
    }
    public void AddDamageDealt (int damage) {
        damageDealt += damage;
    }
    public void AddPointsAccumulated (int points) {
        pointsAccumulated += points;
    }

    public void AddHealth () {
        totalHealth = totalHealth + 25;
        healthSlider.maxValue = totalHealth;
    }

    public void ResetHealth() {
        health = totalHealth;
        healthText.text = "HP: " + health.ToString() + "/" + totalHealth;
    }

    public void AddWatchedMultiplyMoney () {
        //adManager.PlayRewardedAdGame(DoubleMoney);
    }

    //money gets added down here

    public void DoubleMoney () {
        //change the name of the void if it doesnt work
        
        FindObjectOfType<SkinShop>().AddMoney(moneyAccumulated);

        GameObject showMoneyDoubled = GameObject.Find("MoneyTextNum");

        showMoneyDoubled.GetComponent<Text>().text = (moneyAccumulated * 2).ToString();

        doubleMoneyButton = GameObject.Find("DoubleMoney");
        doubleMoneyButton.SetActive(false);

        Debug.Log("double money");
    }

    bool playDamageAnim = true;

    void DamageImpact () {
        if (playDamageAnim == true) {
            anim.SetTrigger("DamageRecieved");
            playDamageAnim = false;
        }

        Invoke("DamageImpactedPassedHighestPoint", 0.2f);
    }

    void DamageImpactedPassedHighestPoint () {
        playDamageAnim = true;
    }
}
