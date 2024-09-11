using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shop;

    public Text roundCompletedText;
    public WaveSpawner waveSpawner;

    public Text pointsTextShop;

    public Text pointsText;
    public GameObject pointsTextGameObject;

    public int points;

    public int healthPrice;
    public Text healthText;
    public int totalHealthBought;
    public Text totalHealthBoughtText;
    public int damagePrice;
    public Text damageText;
    public int totalDamageBought;
    public Text totalDamageBoughtText;

    public PlayerHealth playerHealth;

    public GameObject[] buyGun; 
    public GameObject[] equipGun;
    public GameObject[] equipedGun;
    public GameObject[] leftGunIcons;
    public GameObject[] rightGunIcons;
    public GameObject primarySelected;
    public GameObject secondarySelected;

    public GunManager gunManager;

    public bool shopOpened;

    public bool[] gunBought;
    public int[] gunPrice;

    public GameObject hitMarkerCanvas;

    public Animator anim;
    public GameObject existChestButton;

    public GameObject[] allButtons;

    public GameObject[] chestGuns;

    public GameObject notEnoughPointsScreen;
    public Text notEnoughPointsText;

    public GameObject ammoButton;

    public WaveSpawner enemySpawner;

    public PlayerHealth stats;

    public Animator animGame;

    public Text ammoConfirmationText;

    public void OpenShop () {

        Invoke("ChangeTime", 0.5f);

        roundCompletedText.text = "Round " + waveSpawner.waveNum.ToString() + " Completed";
        
        pointsTextShop.text = points.ToString();
        pointsTextGameObject.gameObject.SetActive(false);
        
        shop.SetActive(true);

        shopOpened = true;

        hitMarkerCanvas.SetActive(false);

        FindObjectOfType<GunManager>().NoShootVoid();
        FindObjectOfType<GunManager>().UnAdsVoid();
    }

    public void ChangeTime () {
        Time.timeScale = 0;

        FindObjectOfType<SoundManager>().StopPlayerSounds();
    }

    public void CloseShop () {

        Time.timeScale = 1;

        shop.SetActive(false);

        FindObjectOfType<PlayerHealth>().ResetHealth();

        pointsTextGameObject.gameObject.SetActive(true);

        shopOpened = false;

        hitMarkerCanvas.SetActive(true);

        enemySpawner.roundPause = false;

        int soundToPlay = 0;

        for (int i = 0; i < FindObjectOfType<SoundManager>().playerSounds.Length; i++) {

            if (FindObjectOfType<SoundManager>().soundPlayingCheck[i] == true){
            soundToPlay = i;
            }

            FindObjectOfType<SoundManager>().playerSounds[soundToPlay].Play();
        }
    }

    void Update () {
        pointsText.text = "$ " + points.ToString();

        if (shopOpened == true) {
            pointsTextShop.text = "$ " + points.ToString();

            for (int i = 0; i < equipGun.Length; i++) {
                if (gunBought[i] == true) {
                    if (i == gunManager.primaryGun || i == gunManager.secondaryGun) {
                    equipGun[i].SetActive(false);
                    equipedGun[i].SetActive(true);
                } else {
                    equipGun[i].SetActive(true);
                    equipedGun[i].SetActive(false);
                }
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
        }

        if (gunManager.gun == gunManager.primaryGun) {
            primarySelected.SetActive(true);
            secondarySelected.SetActive(false);
        } else {
            secondarySelected.SetActive(true);
            primarySelected.SetActive(false);
        }
    }

    public void AddPoints (int pointsToAdd) {
        points += pointsToAdd;
    }

    public void BuyHealth () {
        if (points >= healthPrice) {
            FindObjectOfType<PlayerHealth>().AddHealth();
            points -= healthPrice;
            healthPrice = (int)(healthPrice * 1.5f);
            healthText.text = "$ " + healthPrice.ToString();

            pointsTextShop.text = points.ToString();
            totalHealthBought += 25;
            totalHealthBoughtText.text = totalHealthBought.ToString() + " Health Boost";
        } else {
            NotEnoughtPoints(healthPrice - points);
        }
    }

    public void BuyDamage () {
        if (points >= damagePrice) {
            FindObjectOfType<GunManager>().AddDamage();
            points -= damagePrice;
            damagePrice = (int)(damagePrice * 1.2f);
            damageText.text = "$ " + damagePrice.ToString();

            pointsTextShop.text = points.ToString();
            totalDamageBought += 5;
            totalDamageBoughtText.text = totalDamageBought.ToString() + "% Boost";
        } else {
            NotEnoughtPoints(damagePrice - points);
        }
    }

    public void BuyGun (int gunToBuy) {
        if (gunBought[gunToBuy] == true) {
            FindObjectOfType<GunManager>().PickUpGun(gunToBuy);
        } else if (points >= gunPrice[gunToBuy]) {
            FindObjectOfType<GunManager>().PickUpGun(gunToBuy);
            gunBought[gunToBuy] = true;
            
            points -= gunPrice[gunToBuy];

            if (gunToBuy != 5) {
                buyGun[gunToBuy].SetActive(false);
            }
        }  else {
            NotEnoughtPoints(gunPrice[gunToBuy] - points);
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

    public void Chest () {
        if (points >= 1000) {

            points -= 1000;

        for (int i = 0; i < chestGuns.Length; i++) {
            chestGuns[i].SetActive(false);
        }

        int gunRandom = Random.Range(0, 101);

        if (gunRandom < 20) {
            gunRandom = 0;
        } else if (gunRandom < 40) {
            gunRandom = 3;
        } else if (gunRandom < 60) {
            gunRandom = 2;
        } else if (gunRandom < 80) {
            gunRandom = 1;
        } else {
            gunRandom = 4;
        }
        gunBought[gunRandom] = true;
        buyGun[gunRandom].SetActive(false);
        gunManager.PickUpGun(gunRandom);
        chestGuns[gunRandom].SetActive(true);

        if (gunRandom == 0) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1);
        } else if (gunRandom == 1) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(0.75f);
        } else if (gunRandom == 2) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.1f);
        } else if (gunRandom == 3) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(0.8f);
        } else if (gunRandom == 4) { 
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.2f);
        }

        for (int i = 0; i < allButtons.Length; i++) {
            if (i >= 7) {
                allButtons[i].GetComponent<Camera>().enabled = false;
            } else if (i == 0) {
                allButtons[i].SetActive(true);
            } else  {
                allButtons[i].SetActive(false);
            }
        }

        anim.SetTrigger("PlayChest");
        anim.SetBool("StopChest", false);

        StartCoroutine (ExitChest());

        
        } else {
            NotEnoughtPoints(2500 - points);
        }
    }

    IEnumerator ExitChest() {

        yield return new WaitForSecondsRealtime(5.5f);

        existChestButton.SetActive(true);
        anim.SetBool("weaponStart", true);
    }

    public void ExitAnim () {
        anim.ResetTrigger("PlayChest");
        anim.SetBool("StopChest", true);
        anim.SetBool("weaponStart", false);
    }

    public void NotEnoughtPoints (int pointsNeeded) {
        notEnoughPointsScreen.SetActive(true);
        notEnoughPointsText.text = "You need " + pointsNeeded.ToString() + " more point/s to complete this purchase";
    }

    int ammoPrice;

    string gunUsing;
    int bulletsToAdd;

    public void openAmmoConfirmation () {

        if (gunManager.gun == 0) {
            gunUsing = "Assult Rifle";
            bulletsToAdd = 125;
        } else if (gunManager.gun == 1) {
            gunUsing = "Light Machine Gun";
            bulletsToAdd = 200;
        } else if (gunManager.gun == 2) {
            gunUsing = "Shotgun";
            bulletsToAdd = 40;
        } if (gunManager.gun == 3) {
            gunUsing = "Sniper";
            bulletsToAdd = 35;
        } else if (gunManager.gun == 4) {
            gunUsing = "Submachine Gun";
            bulletsToAdd = 150;
        } else if (gunManager.gun == 5) {
            gunUsing = "Pistol";
            bulletsToAdd = 60;
        }

        ammoConfirmationText.text = "Are you sure that you want to spend 25 points to buy " + bulletsToAdd.ToString() + " bullets for your " + gunUsing + "?";
    }
    
    public void AddAmmo (bool duringGame) {

        ammoPrice = 25;

        if (points >= ammoPrice) { 

            ammoButton.SetActive(false);

        if (gunManager.gun == 0) {
            FindObjectOfType<ArGun1>().AddAmmo();
            points -= ammoPrice;
        } else if (gunManager.gun == 1) {
            FindObjectOfType<LmgGun1>().AddAmmo();
            points -= ammoPrice;
        } else if (gunManager.gun == 2) {
            FindObjectOfType<ShotgunGun1>().AddAmmo();
            points -= ammoPrice;
        } if (gunManager.gun == 3) {
            FindObjectOfType<SniperGun1>().AddAmmo();
            points -= ammoPrice;
        } else if (gunManager.gun == 4) {
            FindObjectOfType<SmgGun1>().AddAmmo();
            points -= ammoPrice;
        } else if (gunManager.gun == 5) {
            FindObjectOfType<PistolGun1>().AddAmmo();
            points -= ammoPrice;
        }
        } else if (duringGame == true){
            animGame.SetTrigger("noMoney");
        } else {
            NotEnoughtPoints(ammoPrice - points);
        }
    }

    public void DiscountPoints (int pointsToDiscount) {
        points -= pointsToDiscount;
    }
}