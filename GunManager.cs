using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    public GameObject[] gunTypes;
    public int gun;

    public bool isPaused;

    public bool isAdsing;

    public bool isReloading;

    public bool shooting;

    public bool isCrouching;

    public Options options;

    public Animator anim;

    public GameObject joyStickGameObject;

    public bool boolShootAndAds;

    public int primaryGun;
    public int secondaryGun;
    bool switchBool;

    public GameObject[] leftGunIcon;
    public GameObject[] rightGunIcon;

    public Text[] ammo;

    public ArGun1 gun0;
    public LmgGun1 gun1;
    public ShotgunGun1 gun2;
    public SniperGun1 gun3;
    public SmgGun1 gun4;
    public PistolGun1 gun5;

    public GameObject bloomLines;

    public float damageMultiplier;

    public AudioListener listener;

    void Start () {

        leftGunIcon[primaryGun].SetActive(true);
        rightGunIcon[secondaryGun].SetActive(true);

        for (int i = 0; i < gunTypes.Length; i++) {
            gunTypes[i].SetActive(false);
        }
        gunTypes[gun].SetActive(true);

        Invoke ("EnableListener", 1);

        Invoke ("StartGame", 0.1f);
    }

    void EnableListener () {
        listener.enabled = true;
    }

    void StartGame () {
        IsShootingUpdate();
        NoShoot();
        if (gun == 0) {
        FindObjectOfType<ArGun1>().InstantReload();
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1);
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().InstantReload();
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(0.75f);
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().InstantReload(); 
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.1f);
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().InstantReload(); 
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(0.8f);
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().InstantReload(); 
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.2f);
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().InstantReload(); 
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.1f);
        }
    }

    void Update()
    {
        if (primaryGun == 0) {
            ammo[0].text = gun0.magazineSize.ToString();
            ammo[1].text = gun0.totalBullets.ToString();
        } else if (primaryGun == 1) {
            ammo[0].text = gun1.magazineSize.ToString();
            ammo[1].text = gun1.totalBullets.ToString();
        } else if (primaryGun == 2) {
            ammo[0].text = gun2.magazineSize.ToString();
            ammo[1].text = gun2.totalBullets.ToString();
        } else if (primaryGun == 3) {
            ammo[0].text = gun3.magazineSize.ToString();
            ammo[1].text = gun3.totalBullets.ToString();
        } else if (primaryGun == 4) {
            ammo[0].text = gun4.magazineSize.ToString();
            ammo[1].text = gun4.totalBullets.ToString();
        } else if (primaryGun == 5) {
            ammo[0].text = gun5.magazineSize.ToString();
            ammo[1].text = gun5.totalBullets.ToString();
        }

        if (secondaryGun == 0) {
            ammo[2].text = gun0.magazineSize.ToString();
            ammo[3].text = gun0.totalBullets.ToString();
        } else if (secondaryGun == 1) {
            ammo[2].text = gun1.magazineSize.ToString();
            ammo[3].text = gun1.totalBullets.ToString();
        } else if (secondaryGun == 2) {
            ammo[2].text = gun2.magazineSize.ToString();
            ammo[3].text = gun2.totalBullets.ToString();
        } else if (secondaryGun == 3) {
            ammo[2].text = gun3.magazineSize.ToString();
            ammo[3].text = gun3.totalBullets.ToString();
        } else if (secondaryGun == 4) {
            ammo[2].text = gun4.magazineSize.ToString();
            ammo[3].text = gun4.totalBullets.ToString();
        } else if (secondaryGun == 5) {
            ammo[2].text = gun5.magazineSize.ToString();
            ammo[3].text = gun5.totalBullets.ToString();
        }

        ammo[4].text = ammo[0].text;
        ammo[5].text = ammo[1].text;
        
        ammo[6].text = ammo[2].text;
        ammo[7].text = ammo[3].text;

        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 5;
        joystickOriginalPos = joyStickGameObject.transform.position;

        if (isAdsing == true) {
        IsAdsingUpdate();
        bloomLines.SetActive(false);
        } else if (isAdsing == false){
        IsNotAdsingUpdate();
        bloomLines.SetActive(true);
        }

        if (shooting == true) {
            IsShootingUpdate();
        }

        if (boolShootAndAds == true) {

            if (gun != 3) {
                isAdsing = true;
            if (options.adsSpeed >= 1) {
                shooting = true;
            }
            } else {
                isAdsing = true;
            }
        }
    }

    public void UnAdsReload () {
        isAdsing = false;
    }

    public void Reload () {
        ReloadVoid();
        UnAdsReload();
    }

    public void Reloading () {
        isReloading = true;
    }
    public void NotReloading () {
        isReloading = false;
    }

    public void Shoot () {
        shooting = true;
    }
    public void NoShoot() {
        NoShootVoid();
        shooting = false;
    }

    public void Ads () {
        if (options.holdToAds == 1) {
        isAdsing = true;
        } else if (options.holdToAds == 0 && isAdsing == false) {
        isAdsing = true;
        } else if (options.holdToAds == 0 && isAdsing == true) {
        isAdsing = false;
        }
    }

    public void ShootAndAds () {
        boolShootAndAds = true;
    }

    public void UnShootAndAds () {
        boolShootAndAds = false;
        if (gun != 3) {
            UnAdsVoid();
            NoShootVoid();
            shooting = false;
            isAdsing = false;
        } else {
            FindObjectOfType<SniperGun1>().Shoot(); 
            UnAdsVoid();
            isAdsing = false;
        }
    }

    public void UnAds () {
        if (options.holdToAds == 1) {
        isAdsing = false;
        }
    }

    public void GamePaused() {
        isPaused = true;
    }

    public void GameUnPaused(){
        isPaused = false;
    }

    public void IsAdsingUpdate () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().Ads();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().Ads();
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().Ads(); 
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().Ads(); 
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().Ads(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().Ads(); 
        }
    }

    public void IsNotAdsingUpdate () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().UnAds();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().UnAds();
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().UnAds();     
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().UnAds(); 
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().UnAds(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().UnAds(); 
        }
    }

    public void IsShootingUpdate () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().Shoot();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().Shoot();
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().Shoot();     
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().Shoot(); 
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().Shoot(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().Shoot(); 
        }
    }

    public void ReloadVoid () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().Reload();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().Reload();
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().Reload(); 
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().Reload(); 
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().Reload(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().Reload(); 
        }
    }

    public void NoShootVoid () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().StopShooting();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().StopShooting();
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().StopShooting(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().StopShooting(); 
        }
    }

    public void AdsVoid () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().Ads();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().Ads();
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().Ads(); 
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().Ads(); 
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().Ads(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().Ads(); 
        }
    }
    
    public void UnAdsVoid () {
        if (gun == 0) {
        FindObjectOfType<ArGun1>().UnAds();
        } else if (gun == 1) {
        FindObjectOfType<LmgGun1>().UnAds();
        } else if (gun == 2) {
        FindObjectOfType<ShotgunGun1>().UnAds(); 
        } else if (gun == 3) {
        FindObjectOfType<SniperGun1>().UnAds(); 
        } else if (gun == 4) {
        FindObjectOfType<SmgGun1>().UnAds(); 
        } else if (gun == 5) {
        FindObjectOfType<PistolGun1>().UnAds(); 
        }
    } 

    public void PointerDown()
    {
        joystickTouchPos = joystickBG.transform.position;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if(joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }

        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystickBG.transform.position = joystickOriginalPos;
        joystick.transform.position = joystickOriginalPos;
    }

    public void SwapGun () {
    if (switchBool == false && secondaryGun != 6) {

        for (int i = 0; i < FindObjectOfType<SoundManager>().rGun.Length; i++) {
            FindObjectOfType<SoundManager>().audioSource[1].Stop();
        }
        
        gunTypes[gun].SetActive(false);
        
        if (gun == primaryGun) {
            gunTypes[secondaryGun].SetActive(true);
            gun = secondaryGun;
        } else {
            gunTypes[primaryGun].SetActive(true);
            gun = primaryGun;
        }

        anim.SetBool("gunSwapBool", true);
        switchBool = true;
        
        Invoke ("SwapTrue", 0.5f);

        anim.SetBool("isReloading", false);

        if (gun == 0) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1);
        } else if (gun == 1) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(0.75f);
        } else if (gun == 2) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.1f);
        } else if (gun == 3) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(0.8f);
        } else if (gun == 4) { 
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.2f);
        } else if (gun == 5) {
        FindObjectOfType<JoyStick>().MoveSpeedMultiplier(1.1f);
        }

        leftGunIcon[primaryGun].SetActive(true);
        rightGunIcon[secondaryGun].SetActive(true);

        FindObjectOfType<GunManager>().NotReloading();
        FindObjectOfType<JoyStick>().UnReload();
      }
    }

    public void PickUpGun (int gunPickedUp) {
       if (gunPickedUp != secondaryGun && gunPickedUp != primaryGun) {

        if (secondaryGun != 6) {
            for (int i = 0; i < gunTypes.Length; i++) {
                gunTypes[i].SetActive(false);
                leftGunIcon[i].SetActive(false);
                rightGunIcon[i].SetActive(false);
            }

            gunTypes[gunPickedUp].SetActive(true);

            if (gun == primaryGun) {
               primaryGun = gunPickedUp;
           } else if (gun == secondaryGun) {
               secondaryGun = gunPickedUp;
           }
        } else {
            for (int i = 0; i < gunTypes.Length; i++) {
                gunTypes[i].SetActive(false);
                leftGunIcon[i].SetActive(false);
                rightGunIcon[i].SetActive(false);
            }

            secondaryGun = gunPickedUp;
            gunTypes[gunPickedUp].SetActive(true);
        }

        gun = gunPickedUp;

        anim.SetBool("gunSwapBool", true);
        switchBool = true;
        
        Invoke ("SwapTrue", 0.5f);

        anim.SetBool("isReloading", false);


        leftGunIcon[primaryGun].SetActive(true);
        rightGunIcon[secondaryGun].SetActive(true);
      }
    }

    public void SwapTrue () {
        switchBool = false;
        anim.SetBool("gunSwapBool", false);
    }

    public void AddDamage () {
        damageMultiplier += 0.05f;
    }

    public void FillAllAmmo () {
        gun0.FillAmmo();
        gun1.FillAmmo();
        gun2.FillAmmo();
        gun3.FillAmmo();
        gun4.FillAmmo();
        gun5.FillAmmo();
    }
}
