using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSway : MonoBehaviour
{
    public MobileLook look;
    Vector3 move;
    [SerializeField] private float smooth;
    [SerializeField] private float normalSmooth;
    [SerializeField] private float adsSmooth;

    [SerializeField] private float swayMultiplier;

    public float swayMultiplierAds;

    public GunManager gun;

    float mouseX;
    float mouseY;

    public Slider sliderNormalSmooth;
    public Slider sliderSwayMultiplier;
    public Slider slideradsSmooth;
    public Slider sliderSwayMultiplierAds; 

    public Text normalSmoothText;
    public Text swayMultiplierText;
    public Text adsSmoothText;
    public Text swayMultiplierAdsText;

    public Text moveXText;
    public Text moveYText;

    public Slider testFpsSlider60;
    public Slider testFpsSlider75;
    public Text testFpsText60;
    public Text testFpsText75;

    public float multiplier;
    public float multiplier60;
    public float multiplier75;

    public void FixedUpdate () {

        if (FindObjectOfType<Options>().fps == 60) {
            multiplier = multiplier60;
        } else if (FindObjectOfType<Options>().fps == 75) {
            multiplier = multiplier75;
        } else {
            multiplier = 1;
        }

        /*normalSmooth = sliderNormalSmooth.value * multiplier;
        swayMultiplier = sliderSwayMultiplier.value * multiplier;
        adsSmooth = slideradsSmooth.value * multiplier;
        swayMultiplierAds = sliderSwayMultiplierAds.value * multiplier;

        normalSmoothText.text = sliderNormalSmooth.value.ToString();
        swayMultiplierText.text = sliderSwayMultiplier.value.ToString();
        adsSmoothText.text = slideradsSmooth.value.ToString();
        swayMultiplierAdsText.text = sliderSwayMultiplierAds.value.ToString()*/

        //testFpsText60.text = testFpsSlider60.value.ToString();
        //testFpsText75.text = testFpsSlider75.value.ToString();

        if (gun.gun != 3) {
            move.x = look.moveX * Time.deltaTime;
            move.y = look.moveY * Time.deltaTime;
        } else {
            move.x = look.moveX * 0.4f * Time.deltaTime;
            move.y = look.moveY * 0.4f * Time.deltaTime;
        }

        if (move.x > 30) {
            move.x = 30;
        }
        if (move.y < -30) {
            move.y = -30;
        }

        if (gun.isAdsing == false) {
            mouseX = move.x * (swayMultiplier * multiplier);
            mouseY = move.y * (swayMultiplier * multiplier);
            smooth = normalSmooth;
        } else {
            mouseX = move.x * (swayMultiplierAds * multiplier);
            mouseY = move.y * (swayMultiplierAds * multiplier);
            smooth = adsSmooth;
        }

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
