using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour
{
    private string bone1000 = "com.andresbrockmann.shooter.1000bones";
    private string removeAds = "com.andresbrockmann.shooter.removeads";
    private string unlockAllSkins = "com.andresbrockmann.shooter.unlockallskins";
    public GameObject restoreButton;

    public int removeAdsBought;
    public int unlockAllSkinsBought;

    public Button removeAdsButton;
    public Button unlockAllSkinsButton;

    public GameObject removeAdsCover;
    public GameObject unlockAllSkinsCover;

    private void Awake () {
        if (Application.platform != RuntimePlatform.IPhonePlayer) {
            restoreButton.SetActive(false);
        }

        //0 no    1 yes

        removeAdsBought = PlayerPrefs.GetInt("ads");
        unlockAllSkinsBought = PlayerPrefs.GetInt("skins");

        if (removeAdsBought == 1) {
            removeAdsButton.enabled = false;
            removeAdsCover.SetActive(true);
        }

        if (unlockAllSkinsBought == 1) {
            unlockAllSkinsButton.enabled = false;
            unlockAllSkinsCover.SetActive(true);
        }

    }

    public void OnPurchaseComplete (Product product) {
        if (product.definition.id == bone1000) {
            FindObjectOfType<SkinShop>().AddMoney(10000);
        }

        if (product.definition.id == removeAds) {
            FindObjectOfType<InterstitialAd>().RemoveAds();

            removeAdsBought = 1;
            PlayerPrefs.SetInt("ads", removeAdsBought);

            removeAdsButton.enabled = false;
            removeAdsCover.SetActive(true);
        }

        if (product.definition.id == unlockAllSkins) {
            FindObjectOfType<SkinShop>().UnlockAllSkins();

            unlockAllSkinsBought = 1;
            PlayerPrefs.SetInt("skins", unlockAllSkinsBought);

            unlockAllSkinsButton.enabled = false;
            unlockAllSkinsCover.SetActive(true);
        }
    }

    public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason) {
        Debug.Log(product.definition.id + " failed because" + failureReason);
    }
}

