using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArGun1 : MonoBehaviour
{
    public GunManager gun;

    public Transform gunModel;

    public float bloomNum;

    public float adsLocation;
    public float adsSpeed;
    public Transform startAds;
    public Transform endAds;

    public bool adsing;
    public Animator anim;

    public int originalMagazineSize;
    public int totalBullets;
    public int magazineSize;

    public float damage;
    public float fireRate;

    public GameObject impactEffect;


    public Camera fpsCam;

    public bool reloading;

    public bool shooting;

    private float nextTimeToFire;

    public float numKickBack;
    public bool addKickBack;

    public Vector3 startkickBack;
    public Vector3 endkickBack;

    public ParticleSystem muzzleFlash;

    public JoyStick movement;
    bool comeBack;

    public GameObject parentObject;

    public PlayerHealth stats;

    public GameObject ReloadAmmoButton;

    public int startTotalAmmo;

    public SkinShop skinShop;

    public bool realGame;

    void Start () {
        startkickBack = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.003f);
        endkickBack = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        startTotalAmmo = totalBullets;

        if (realGame == true) {
            damage = (int)(damage * ((skinShop.damageBoost * 0.01) + 1));
        }
    }

    public void Reload () {
        if (reloading == false && totalBullets > 0 && originalMagazineSize - magazineSize != 0) {
            adsing = false;
            anim.SetBool("isReloading", true);
            anim.SetBool("ArGun1-R", true);
            reloading = true;
            Invoke ("CheckReload", 2f);
            StopShooting();
            FindObjectOfType<JoyStick>().Reload();
            FindObjectOfType<GunManager>().Reloading();
            FindObjectOfType<SoundManager>().Reload();
            }  else if (originalMagazineSize - magazineSize == originalMagazineSize) {
            if (reloading == false && totalBullets != 0) {
            adsing = false;
            anim.SetBool("isReloading", true);
            anim.SetBool("ArGun1-R", true);
            reloading = true;
            StopShooting();
            Invoke ("CheckReload", 2f);
            FindObjectOfType<GunManager>().Reloading();
            FindObjectOfType<JoyStick>().Reload();
            FindObjectOfType<SoundManager>().Reload();
            }
        }
    }

    void FixedUpdate()
    {
        if (totalBullets == 0) {
            ReloadAmmoButton.SetActive(true);
        }

        if (magazineSize <= 0) {
            FindObjectOfType<GunManager>().UnAdsReload();
            Reload();
            adsing = false;
        }


        //gunModel.localPosition = Vector3.Lerp(startAds.localPosition, endAds.localPosition, adsLocation);

        if (adsing == true) {
            /*if ( adsLocation >= 1) {
                adsLocation = 1f;
            } else {
                adsLocation =  adsLocation + adsSpeed * Time.deltaTime; 
            }*/

            anim.SetBool("isAdsing", true);
            anim.SetBool("arAds", true);
            anim.SetBool("lmgAds", false);
            anim.SetBool("shotgunAds", false);
            anim.SetBool("sniperAds", false);
            anim.SetBool("smgAds", false);
            anim.SetBool("pistolAds", false);
        } else if (adsing == false){
            /*if ( adsLocation <= 0) {
                 adsLocation = 0f;
            } else {
                 adsLocation =  adsLocation - adsSpeed * Time.deltaTime;
            }*/
            
            anim.SetBool("isAdsing", false);
            anim.SetBool("arAds", false);
        }

        if (adsing == false && shooting == false) {
            if (movement.isCrouching == false) {
                
                if (movement.slide == false) {

                    if (movement.sprintIsLocked == false && movement.lockSprintBool == false && movement.sprintBool == false) {

                        if ((movement.sprintIsLocked == false && movement.lockSprintBool == false && movement.sprintBool == false) && movement.moving == false) {
                        
                            if (movement.isGrounded == true) {
                        
                                if (bloomNum >= 150) {
                                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                                } 
                
                                if (bloomNum <= 150) {
                                    bloomNum =  bloomNum + 150 * Time.deltaTime; 
                                }

                            } else {

                                if (bloomNum >= 200) {
                                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                                } 
                        
                                if (bloomNum <= 200) {
                                    bloomNum =  bloomNum + 150 * Time.deltaTime;
                                }
                        }

                        } else {

                            if (bloomNum >= 175) {
                                bloomNum =  bloomNum - 150 * Time.deltaTime;
                            } 
                        
                            if (bloomNum <= 175) {
                                bloomNum =  bloomNum + 150 * Time.deltaTime;
                            }
                        }

                    } else {

                        if (bloomNum >= 200) {
                            bloomNum =  bloomNum - 150 * Time.deltaTime;
                        } 
                        
                        if (bloomNum <= 200) {
                            bloomNum =  bloomNum + 150 * Time.deltaTime;
                        }
                    }
                
            } else {
                
                if (bloomNum >= 215) {
                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                } 
                
                if (bloomNum <= 215) {
                    bloomNum =  bloomNum + 150 * Time.deltaTime;
                }
                
            }
                
            } else {

                if (bloomNum <= 125) {
                    bloomNum =  bloomNum + 150 * Time.deltaTime;
                } 
                
                if (bloomNum >= 125) {
                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                }
            }
        } else if (shooting == true) {

            if (comeBack == false) {
                if (bloomNum <= 175) {
                    bloomNum = bloomNum + 250 * Time.deltaTime;
                } else {
                    comeBack = true;
                }
            }

            if (comeBack == true) {
                if (bloomNum >= 160) {
                    bloomNum = bloomNum - 250 * Time.deltaTime;
                } else {
                    comeBack = false;
                }
            }
        }

        if (shooting == true) {
            if (numKickBack >= 1 && addKickBack == true) {
                addKickBack = false;
            } else if (numKickBack <= 0 && addKickBack == false){
                addKickBack = true;
            }

            if (addKickBack == false) {
                numKickBack = numKickBack - Time.deltaTime * 25;
            } else {
                numKickBack = numKickBack + Time.deltaTime * 25;
            }
        } else  {
            if (numKickBack > 0) {
                numKickBack = numKickBack - Time.deltaTime * 25;
            }
        }

        transform.localPosition = Vector3.Lerp(endkickBack, startkickBack, numKickBack);
    }

    public void Ads () {
        if (reloading == false) {
        anim.SetBool("isReloading", false);
        anim.SetBool("ArGun1-R", false);
        FindObjectOfType<Options>().Ads();
        FindObjectOfType<MobileLook>().Ads();
        FindObjectOfType<JoyStick>().Ads();
        adsing = true;
        }
    }

    public void UnAds () {
        adsing = false;
        FindObjectOfType<MobileLook>().UnAds();
        FindObjectOfType<Options>().UnAds();
        if (reloading == false) {
           FindObjectOfType<JoyStick>().UnAds(); 
        }
    }

    public void CheckReload () {
        if (reloading == true && totalBullets > 0 && parentObject.activeSelf == true) {
        FindObjectOfType<GunManager>().NotReloading();
        FindObjectOfType<JoyStick>().UnReload();
        if (totalBullets >= originalMagazineSize - magazineSize) {
            if (SceneManager.GetActiveScene().buildIndex == 4) {
                totalBullets = totalBullets - (originalMagazineSize - magazineSize);
                magazineSize = originalMagazineSize;
            } else {
                magazineSize = originalMagazineSize;
            }
        } else if (totalBullets + magazineSize < originalMagazineSize){
            magazineSize =  magazineSize + totalBullets;
            totalBullets = 0;
        }
        }
        anim.SetBool("isReloading", false);
        anim.SetBool("ArGun1-R", false);
        reloading = false;
    }

    public void Shoot () {
        if (magazineSize > 0 && reloading == false && Time.time >= nextTimeToFire) {

        nextTimeToFire = Time.time + 1f / fireRate;

        Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        if (adsing == false) {
            bloom += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.up;
            bloom += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.right;
        } else {
            bloom += Random.Range(0, 0) * fpsCam.transform.up;
            bloom += Random.Range(0, 0) * fpsCam.transform.right;
        }
        bloom -= fpsCam.transform.position;
        bloom.Normalize();
        
        shooting = true;

        RaycastHit hit;

        FindObjectOfType<MobileLook>().Recoil();
        FindObjectOfType<JoyStick>().Shoot();
        FindObjectOfType<SoundManager>().Shoot();

        muzzleFlash.Play();

        if (Physics.Raycast(fpsCam.transform.position, bloom, out hit)){
            Enemy target = hit.transform.GetComponent<Enemy>();

            
            if (target != null) {
                if (hit.transform.tag == "Enemy") {
                    if (hit.distance > 100) {
                        target.TakeDamage((int)((damage - 10) * gun.damageMultiplier));
                        stats.damageDealt += (int)((damage - 10) * gun.damageMultiplier);
                    } else if (hit.distance > 75) {
                        target.TakeDamage((int)((damage - 7) * gun.damageMultiplier));
                        stats.damageDealt += (int)((damage - 7) * gun.damageMultiplier);
                    } else if (hit.distance > 35){
                        target.TakeDamage((int)((damage - 5) * gun.damageMultiplier));
                        stats.damageDealt += (int)((damage - 5) * gun.damageMultiplier);
                    } else {
                        target.TakeDamage((int)(damage * gun.damageMultiplier));
                        stats.damageDealt += (int)(damage * gun.damageMultiplier);
                    }
                } else {
                        target.TakeDamage(0);
                }
            }

            if (hit.transform.tag == "moveWall") {
                FindObjectOfType<WallMoverManager>().MoveWall(5);
            } else if (hit.transform.tag == "moveWall2") {
                FindObjectOfType<WallMoverManager>().MoveWall(25);
            } else if (hit.transform.tag == "moveWall3") {
                FindObjectOfType<WallMoverManager>().MoveWall(40);
            } else if (hit.transform.tag == "moveWall4") {
                FindObjectOfType<WallMoverManager>().MoveWall(75);
            }

            if (hit.collider != null && hit.collider.isTrigger == false && hit.collider.gameObject.name != "Terrain") {
            Renderer mat = hit.transform.GetComponent<Renderer>();
            impactEffect.GetComponent<Renderer>().material.color = mat.material.color;
            GameObject hitPlayerEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitPlayerEffect, 2);
            }  
        }

        magazineSize = magazineSize - 1;

        if (magazineSize + originalMagazineSize == 0) {
            gun.gun = 6;
        }
        }
    }

    public void StopShooting () {
        shooting = false;
        FindObjectOfType<MobileLook>().StopRecoil();
        FindObjectOfType<JoyStick>().UnShoot();
    }

    public void InstantReload () {
        magazineSize = originalMagazineSize;
    }

    public void AddAmmo () {
        totalBullets += originalMagazineSize * 5;
    }

    public void FillAmmo () {
    magazineSize = originalMagazineSize;
    totalBullets = startTotalAmmo;
    }
}