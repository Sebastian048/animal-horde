using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopDesign : MonoBehaviour
{
    public Text[] gunText;
    public Image[] gunBackground;
    public GameObject[] gunShop;

    public Material textMatWhite;
    public Material textMatBlack;

    public int gunChosen;

    public Animator anim;

    public Sprite orangeButton;
    public Sprite blackButton;

    public SkinShop skinShop;

    public void Switch (int chosenGun) {
        gunChosen = chosenGun;
        for (int i = 0; i < gunText.Length; i++) {
            if (i == chosenGun) {
                gunText[i].material = textMatBlack;
                gunBackground[i].GetComponent<Image>().sprite = orangeButton;
                gunShop[i].SetActive(true);
            } else {
                gunText[i].material = textMatWhite;
                gunBackground[i].GetComponent<Image>().sprite = blackButton;
                gunShop[i].SetActive(false);
            }
        }

        skinShop.UpdateSkins(true, true);
        skinShop.UpdateSkins(true, false);

        skinShop.buyText.gameObject.SetActive(true);
        skinShop.buyText.text = "Equipped";
        skinShop.realBuyText.gameObject.SetActive(false);
        skinShop.boneImage.SetActive(false);

        skinShop.skinSelected = skinShop.matChosenReal[chosenGun];

        if (skinShop.matChosenReal[chosenGun] == 6) {
            skinShop.achievementButton.SetActive(true);
        } else {
            skinShop.achievementButton.SetActive(false);
        }


        anim.SetInteger ("Gun", chosenGun);
    }
}
