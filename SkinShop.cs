using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{
    public bool realGame;

    public int money;

    public int skinSelected;

    public Material[] arMat;
    public Material[] lmgMat;
    public Material[] shotgunMat;
    public Material[] sniperMat;
    public Material[] smgMat;
    public Material[] pistolMat;

    public int[] matChosen;

    public Material[] arMatReal;
    public Material[] lmgMatReal;
    public Material[] shotgunMatReal;
    public Material[] sniperMatReal;
    public Material[] smgMatReal;
    public Material[] pistolMatReal;

    public int[] matChosenReal;


    public GameObject[] arParts;
    public GameObject[] lmgParts;
    public GameObject[] shotgunParts;
    public GameObject[] sniperParts;
    public GameObject[] smgParts;
    public GameObject[] pistolParts;

    public SkinShopDesign skinShopDesign;

    public int[] arPrice;
    public int[] lmgPrice;
    public int[] shotgunPrice;
    public int[] sniperPrice;
    public int[] smgPrice;
    public int[] pistolPrice;

    int[][] prices = new int[7][];


    public int[][] bought = new int[7][];

    public Text buyText;

    public GameObject[] skinBackground;

    public GameObject achievementButton;
    public Text achievementText;
    public int[] achievement;
    public Slider achievementSlider;

    public Text moneyText;

    public int achievementSkinsBought;

    public GameObject notEnoughBones;
    public Text notEnoughBonesTextTitle;
    public Text notEnoughBonesText;

    public int numberOfTimeToWatchAdd;
    public GameObject noMoreAds;

    private int firstTimePlaying;

    public int damageBoost;
    public int pointsNeededToBuyDamage;
    public int healthBoost;
    public int pointsNeededToBuyHealth;
    public Text damagePrice;
    public Text healthPrice;
    public Text damageBoosted;
    public Text healthBoosted;

    public Text realBuyText;
    public GameObject boneImage;

    public GameObject notEnoughBonesBoost;
    public Text notEnoughBonesBoostText;

    public GameObject notEnoughEliminations;
    public Text notEnoughEliminationsText;

    void Start () {
        //0 = not bought
        //1 = bought
        money = PlayerPrefs.GetInt("money");

        achievementSkinsBought = PlayerPrefs.GetInt("allSkinsBought");

        prices[0] = arPrice;
        prices[1] = lmgPrice;
        prices[2] = shotgunPrice;
        prices[3] = sniperPrice;
        prices[4] = smgPrice;
        prices[5] = pistolPrice;
        
        int[][] boughtFake = new int[7][];

        for (int i = 0; i < boughtFake.Length; i++) {
            boughtFake[i] = new int[7];
        }
        
        bought = boughtFake;

        for (int i = 0; i < bought.Length; i++) {
            for (int o = 0; o < bought[i].Length; o++) {
                if (o != 5) {
                    bought[i][o] = PlayerPrefs.GetInt("bought" + i.ToString() + o.ToString());
                    //Debug.Log("bought" + i.ToString() + o.ToString());
                    //Debug.Log(bought[i][o]);
                }
            }

            achievement[i] = PlayerPrefs.GetInt("Achievement" + i.ToString());
        }
        
        matChosenReal[0] = PlayerPrefs.GetInt("0");
        matChosenReal[1] = PlayerPrefs.GetInt("1");
        matChosenReal[2] = PlayerPrefs.GetInt("2");
        matChosenReal[3] = PlayerPrefs.GetInt("3");
        matChosenReal[4] = PlayerPrefs.GetInt("4");
        matChosenReal[5] = PlayerPrefs.GetInt("5");
        matChosenReal[6] = PlayerPrefs.GetInt("6");

        UpdateSkins(true, false);

        if (realGame == false) {
            SelectSkin(matChosenReal[5]);
        }

        firstTimePlaying = PlayerPrefs.GetInt("firstTime");
        if (firstTimePlaying == 0) {
            RemoveAllSkins();
            firstTimePlaying = 1;
            PlayerPrefs.SetInt("allSkinsBought", firstTimePlaying);

            damageBoost = 0;
            pointsNeededToBuyDamage = 100;
            healthBoost = 0;
            pointsNeededToBuyHealth = 250;

            PlayerPrefs.SetInt("damage", damageBoost);
            PlayerPrefs.SetInt("pointsDamage", pointsNeededToBuyDamage);
            PlayerPrefs.SetInt("health", healthBoost);
            PlayerPrefs.SetInt("pointsHealth", pointsNeededToBuyHealth);

        } else {
            damageBoost = PlayerPrefs.GetInt("damage");
            pointsNeededToBuyDamage = PlayerPrefs.GetInt("pointsDamage");
            healthBoost = PlayerPrefs.GetInt("health");
            pointsNeededToBuyHealth = PlayerPrefs.GetInt("pointsHealth");
        }

        if (realGame == false) {
            UpdateBoostText();
        }
    }

    public void Update () {
        if (realGame == false) {
            moneyText.text = money.ToString();
        }
    }
    
    public void SelectSkin (int skinToChoose) {
        skinSelected = skinToChoose;

        matChosen[skinShopDesign.gunChosen] = skinToChoose;

        if (skinToChoose != 5) {
            achievementButton.SetActive(false);

            if (bought[skinShopDesign.gunChosen][skinToChoose] == 0) {
                realBuyText.text = prices[skinShopDesign.gunChosen][skinToChoose].ToString();

                buyText.gameObject.SetActive(false);
                realBuyText.gameObject.SetActive(true);
                boneImage.SetActive(true);
            } else if (skinToChoose == matChosenReal[skinShopDesign.gunChosen]) {

                buyText.gameObject.SetActive(true);
                realBuyText.gameObject.SetActive(false);
                boneImage.SetActive(false);
                
                buyText.text = "Equipped";
            } else {
                buyText.text = "Equip";

                buyText.gameObject.SetActive(true);
                realBuyText.gameObject.SetActive(false);
                boneImage.SetActive(false);
            }
        } else {
            
            int eliminationsNeeded = 1000;

            if (achievement[skinShopDesign.gunChosen] < eliminationsNeeded && achievementSkinsBought != 1) {
                achievementButton.SetActive(true);

                achievementText.text = achievement[skinShopDesign.gunChosen].ToString() + "/" + eliminationsNeeded.ToString() + "Enemies Eliminated";

                achievementSlider.maxValue = 1000;
                achievementSlider.value = achievement[skinShopDesign.gunChosen];

            } else {
                achievementButton.SetActive(false);

                if (skinToChoose == matChosenReal[skinShopDesign.gunChosen]) {
                    buyText.text = "Equipped";

                    buyText.gameObject.SetActive(true);
                    realBuyText.gameObject.SetActive(false);
                    boneImage.SetActive(false);
                } else {
                    buyText.text = "Equip";

                    buyText.gameObject.SetActive(true);
                    realBuyText.gameObject.SetActive(false);
                    boneImage.SetActive(false);
                }
            }
        }

        UpdateSkins(false, false);
    }

    public void UpdateSkins (bool realSkin, bool switchingGun) {

        if (switchingGun == false) {

        if (realSkin == false) {

        for (int i = 0; i < skinBackground.Length; i++) {
            if (i == skinSelected) {
                skinBackground[i].SetActive(true);
            } else {
                skinBackground[i].SetActive(false);
            }
        }

        if (skinShopDesign.gunChosen == 0) {
            for (int i = 0; i < arParts.Length; i++) {
                arParts[i].GetComponent<MeshRenderer>().material = arMat[matChosen[0]];
            }
        } else if (skinShopDesign.gunChosen == 1) {
            for (int i = 0; i < lmgParts.Length; i++) {
                lmgParts[i].GetComponent<MeshRenderer>().material = lmgMat[matChosen[1]];
            }
        } else if (skinShopDesign.gunChosen == 2) {
            for (int i = 0; i < shotgunParts.Length; i++) {
                if (i != 1 && i != 6) {
                    shotgunParts[i].GetComponent<MeshRenderer>().material = shotgunMat[matChosen[2]];
                }
            }
        } else if (skinShopDesign.gunChosen == 3) {
            for (int i = 0; i < sniperParts.Length; i++) {
                if (i == 0) {
                    for (int m = 0; m < sniperParts[i].GetComponent<MeshRenderer>().materials.Length; m++) {
                        if (m != 2) {
                            Material[] materials = sniperParts[i].GetComponent<MeshRenderer>().materials;
                            materials[m] = sniperMat[matChosen[3]];
                            sniperParts[i].GetComponent<MeshRenderer>().materials = materials;
                        }
                    }
                }
            }
        } else if (skinShopDesign.gunChosen == 4) {
            for (int i = 0; i < smgParts.Length; i++) {
                smgParts[i].GetComponent<MeshRenderer>().material = smgMat[matChosen[4]];
            }
        } else if (skinShopDesign.gunChosen == 5) {
            for (int i = 0; i < pistolParts.Length; i++) {
                if (i == 1) {
                    for (int t = 0; t < pistolParts[i].GetComponent<SkinnedMeshRenderer>().materials.Length; t++) {
                        pistolParts[i].GetComponent<SkinnedMeshRenderer>().material = pistolMat[matChosen[5]];
                    }
                } else {
                    pistolParts[i].GetComponent<MeshRenderer>().material = pistolMat[matChosen[5]];
                }
            }
        }
        
        } else {

            
        for (int i = 0; i < skinBackground.Length; i++) {
            if (i == matChosenReal[skinShopDesign.gunChosen]) {
                skinBackground[i].SetActive(true);
            } else {
                skinBackground[i].SetActive(false);
            }
        }

        
        for (int i = 0; i < arParts.Length; i++) {
            arParts[i].GetComponent<MeshRenderer>().material = arMatReal[matChosenReal[0]];
        }

        for (int i = 0; i < lmgParts.Length; i++) {
            lmgParts[i].GetComponent<MeshRenderer>().material = lmgMatReal[matChosenReal[1]];
        }

        for (int i = 0; i < shotgunParts.Length; i++) {
            if (i != 1 && i != 6) {
                shotgunParts[i].GetComponent<MeshRenderer>().material = shotgunMatReal[matChosenReal[2]];
            }
        }

        for (int i = 0; i < sniperParts.Length; i++) {
            if (i == 0) {
                for (int m = 0; m < sniperParts[i].GetComponent<MeshRenderer>().materials.Length; m++) {
                    if (m != 2) {
                        Material[] materials = sniperParts[i].GetComponent<MeshRenderer>().materials;
                        materials[m] = sniperMatReal[matChosenReal[3]];
                        sniperParts[i].GetComponent<MeshRenderer>().materials = materials;
                    }
                }
            }
        }

        for (int i = 0; i < smgParts.Length; i++) {
            smgParts[i].GetComponent<MeshRenderer>().material = smgMatReal[matChosenReal[4]];
        }

        for (int i = 0; i < pistolParts.Length; i++) {
            if (i == 1) {
                for (int t = 0; t < pistolParts[i].GetComponent<SkinnedMeshRenderer>().materials.Length; t++) {
                    pistolParts[i].GetComponent<SkinnedMeshRenderer>().material = pistolMatReal[matChosenReal[5]];
                }
            } else {
                pistolParts[i].GetComponent<MeshRenderer>().material = pistolMatReal[matChosenReal[5]];
            }
        }
        }
        } else {


            if (matChosenReal[skinShopDesign.gunChosen] <= 2) {
            achievementButton.SetActive(false);

            buyText.text = "Equipped";

        } else {

            int eliminationsNeeded = 0;
                if (skinSelected == 3) {
                    eliminationsNeeded = 100;
                } else if (skinSelected == 4) {
                    eliminationsNeeded = 500;
                } else if (skinSelected == 5) {
                eliminationsNeeded = 1000;
                }

            if (achievement[skinShopDesign.gunChosen] < eliminationsNeeded) {
                achievementButton.SetActive(true);

            achievementText.text = achievement[skinSelected].ToString() + "/" + eliminationsNeeded.ToString() + "Enemies Eliminated";

            } else {
                if (skinSelected == matChosenReal[skinShopDesign.gunChosen]) {
                    buyText.text = "Equipped";
                } else {
                    buyText.text = "Equip";
                }
            }
        }

        Debug.Log("Skin Selected is: " + skinSelected);

        for (int i = 0; i < skinBackground.Length; i++) {
            if (i == matChosenReal[skinShopDesign.gunChosen]) {
                skinBackground[i].SetActive(true);
            } else {
                skinBackground[i].SetActive(false);
            }
        }
        }
    }

    public void BuySkin () {

        if (skinSelected != 5) {
            if (money >= prices[skinShopDesign.gunChosen][skinSelected]) {

                if (bought[skinShopDesign.gunChosen][skinSelected] != 1) {
                    money -= prices[skinShopDesign.gunChosen][skinSelected];

                    PlayerPrefs.SetInt("money", money);
                }

                bought[skinShopDesign.gunChosen][skinSelected] = 1;
                PlayerPrefs.SetInt("bought" + skinShopDesign.gunChosen.ToString() + skinSelected.ToString(), bought[skinShopDesign.gunChosen][skinSelected]);
                Debug.Log("bought" + skinShopDesign.gunChosen.ToString() + skinSelected.ToString());

                Debug.Log(bought[skinShopDesign.gunChosen][skinSelected]);

                matChosenReal[skinShopDesign.gunChosen] = skinSelected;

                buyText.text = "Equipped";

                buyText.gameObject.SetActive(true);
                realBuyText.gameObject.SetActive(false);
                boneImage.SetActive(false);

                UpdateSkins(true, false);
            } else {
                notEnoughBones.SetActive(true);
                notEnoughBonesTextTitle.text = "You need more bones!";
                notEnoughBonesText.text = "You need " + (prices[skinShopDesign.gunChosen][skinSelected] - money).ToString() + " more bones to complete this purchase";
            }
        } else {
            int eliminationsNeeded = 1000;

            if (achievement[skinShopDesign.gunChosen] >= eliminationsNeeded || achievementSkinsBought == 1) {

                matChosenReal[skinShopDesign.gunChosen] = skinSelected;

                buyText.text = "Equipped";

                UpdateSkins(true, false);
            } else if (achievement[skinShopDesign.gunChosen] < eliminationsNeeded){
                notEnoughEliminations.SetActive(true);
                notEnoughEliminationsText.text = "You need " + (eliminationsNeeded - achievement[skinShopDesign.gunChosen]).ToString() + " more eliminations to complete this purchase";
            }
        }

        PlayerPrefs.SetInt("0", matChosenReal[0]);
        PlayerPrefs.SetInt("1", matChosenReal[1]);
        PlayerPrefs.SetInt("2", matChosenReal[2]);
        PlayerPrefs.SetInt("3", matChosenReal[3]);
        PlayerPrefs.SetInt("4", matChosenReal[4]);
        PlayerPrefs.SetInt("5", matChosenReal[5]);
        PlayerPrefs.SetInt("6", matChosenReal[6]);
    }

    public void AddAchievement (int gunUsing) {
        achievement[gunUsing] += 1;

        PlayerPrefs.SetInt("Achievement" + gunUsing.ToString(), achievement[gunUsing]);
    }

    public void TestAddAchievement () {
        achievement[skinShopDesign.gunChosen] += 10;
    }

    //money gets added down here

    public void OnRewardedAdSuccessMenu () {
        money += 100;

        PlayerPrefs.SetInt("money", money);
    }

    public void AddMoney (int moneyToGive) {
        money += moneyToGive;

        PlayerPrefs.SetInt("money", money);
    }

    public void UnlockAllSkins () {
        for (int i = 0; i < bought.Length; i++) {
            for (int o = 0; o < bought[i].Length; o++) {
                bought[i][o] = 1;
                PlayerPrefs.SetInt("bought" + i.ToString() + o.ToString(), bought[i][o]);
                Debug.Log("bought" + i.ToString() + o.ToString());
            }
        }

        achievementSkinsBought = 1;
        PlayerPrefs.SetInt("allSkinsBought", achievementSkinsBought);
    }

    public void RemoveAllSkins () {
        for (int i = 0; i < bought.Length; i++) {
            for (int o = 0; o < bought[i].Length; o++) {
                if (o == 0) {
                    bought[i][o] = 1;
                } else if (o != 0) {
                    bought[i][o] = 0;
                }
                PlayerPrefs.SetInt("bought" + i.ToString() + o.ToString(), bought[i][o]);
                Debug.Log("bought" + i.ToString() + o.ToString());
            }

            achievement[i] = 0;
        }

        achievementSkinsBought = 0;
        PlayerPrefs.SetInt("allSkinsBought", achievementSkinsBought);
    }


    public void BuyDamage () {
        if (money >= pointsNeededToBuyDamage) {
            money -= pointsNeededToBuyDamage;

            damageBoost += 5;

            PlayerPrefs.SetInt("damage", damageBoost);

            pointsNeededToBuyDamage += 100;
            PlayerPrefs.SetInt("pointsDamage", pointsNeededToBuyDamage);
            
            PlayerPrefs.SetInt("money", money);

            UpdateBoostText();

        } else {
            notEnoughBonesBoost.SetActive(true);
            notEnoughBonesBoostText.text = "You need " + (pointsNeededToBuyDamage - money).ToString() + " more bones to complete this purchase";
        }
    }

    public void BuyHealth () {
        if (money >= pointsNeededToBuyHealth) {
            money -= pointsNeededToBuyHealth;

            healthBoost += 25;

            PlayerPrefs.SetInt("health", healthBoost);

            pointsNeededToBuyHealth += 250;
            PlayerPrefs.SetInt("pointsHealth", pointsNeededToBuyHealth);

            PlayerPrefs.SetInt("money", money);

            UpdateBoostText();

        } else {
            notEnoughBonesBoost.SetActive(true);
            notEnoughBonesBoostText.text = "You need " + (pointsNeededToBuyDamage - money).ToString() + " more bones to complete this purchase";
        }
    }

    void UpdateBoostText () {
        damagePrice.text = pointsNeededToBuyDamage.ToString();
        healthPrice.text = pointsNeededToBuyHealth.ToString();

        if (healthBoost != 0) {
            healthBoosted.text = "+" + healthBoost.ToString() + " Health Currently Boosted";
        }
        if (damageBoost != 0) {
            damageBoosted.text = "+" + damageBoost.ToString() + "% Damage Currently Boosted";
        }
    }
}
