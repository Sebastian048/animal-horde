using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShotgunGun1 : MonoBehaviour
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

    public bool isPaused;

    public int shotgundamage;
    private Enemy enemyGameObject;

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
        startTotalAmmo = totalBullets;

        if (realGame == true) {
            damage = (int)(damage * ((skinShop.damageBoost * 0.01) + 1));
        }
    }

    public void Reload () {
        if (reloading == false && totalBullets > 0 && originalMagazineSize - magazineSize != 0) {
            adsing = false;
            anim.SetBool("isShooting", false);
            anim.SetBool("ShotgunGun1-S",false);
            anim.SetBool("isReloading", true);
            anim.SetBool("ShotgunGun1-R", true);
            reloading = true;
            Invoke ("CheckReload", 3f);
            StopShootAnim();
            FindObjectOfType<JoyStick>().Reload();
            FindObjectOfType<GunManager>().Reloading();
            FindObjectOfType<SoundManager>().Reload();
            }  else if (originalMagazineSize - magazineSize == originalMagazineSize) {
            if (reloading == false && totalBullets != 0) {
            adsing = false;
            anim.SetBool("isShooting", false);
            anim.SetBool("ShotgunGun1-S",false);
            anim.SetBool("isReloading", true);
            anim.SetBool("ShotgunGun1-R", true);
            reloading = true;
            StopShootAnim();
            Invoke ("CheckReload", 3f);
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
            Invoke("Reload", 0.5f);
            adsing = false;
        }

        if (adsing == true) {
            /*if ( adsLocation >= 1) {
                adsLocation = 1f;
            } else {
                adsLocation =  adsLocation + adsSpeed * Time.deltaTime; 
            }*/

            anim.SetBool("isAdsing", true);
            anim.SetBool("arAds", false);
            anim.SetBool("lmgAds", false);
            anim.SetBool("shotgunAds", true);
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
            anim.SetBool("shotgunAds", false);
        }


        if (adsing == false && shooting == false) {
            if (movement.isCrouching == false) {
                
                if (movement.slide == false) {

                    if (movement.sprintIsLocked == false && movement.lockSprintBool == false && movement.sprintBool == false) {

                        if ((movement.sprintIsLocked == false && movement.lockSprintBool == false && movement.sprintBool == false) && movement.moving == false) {
                        
                            if (movement.isGrounded == true) {
                        
                                if (bloomNum >= 75) {
                                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                                } 
                
                                if (bloomNum <= 75) {
                                    bloomNum =  bloomNum + 150 * Time.deltaTime; 
                                }

                            } else {

                                if (bloomNum >= 125) {
                                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                                } 
                        
                                if (bloomNum <= 125) {
                                    bloomNum =  bloomNum + 150 * Time.deltaTime;
                                }
                        }

                        } else {

                            if (bloomNum >= 100) {
                                bloomNum =  bloomNum - 150 * Time.deltaTime;
                            } 
                        
                            if (bloomNum <= 100) {
                                bloomNum =  bloomNum + 150 * Time.deltaTime;
                            }
                        }

                    } else {

                        if (bloomNum >= 125) {
                            bloomNum =  bloomNum - 150 * Time.deltaTime;
                        } 
                        
                        if (bloomNum <= 125) {
                            bloomNum =  bloomNum + 150 * Time.deltaTime;
                        }
                    }
                
            } else {
                
                if (bloomNum >= 140) {
                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                } 
                
                if (bloomNum <= 140) {
                    bloomNum =  bloomNum + 150 * Time.deltaTime;
                }
                
            }
                
            } else {

                if (bloomNum <= 50) {
                    bloomNum =  bloomNum + 150 * Time.deltaTime;
                } 
                
                if (bloomNum >= 50) {
                    bloomNum =  bloomNum - 150 * Time.deltaTime;
                }
            }
        } else if (shooting == true) {

            if (comeBack == false) {
                if (bloomNum <= 130) {
                    bloomNum = bloomNum + 150 * Time.deltaTime;
                } else {
                    comeBack = true;
                }
            }

            if (comeBack == true) {
                if (bloomNum >= 90) {
                    bloomNum = bloomNum - 100 * Time.deltaTime;
                } else {
                    comeBack = false;
                }
            }
        }
    }

    public void Ads () {
        if (reloading == false) {
        anim.SetBool("isReloading", false);
        anim.SetBool("ShotgunGun1-R", false);
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

    public void GamePaused() {
        isPaused = true;
    }

    public void GameUnPaused(){
        isPaused = false;
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
        anim.SetBool("ShotgunGun1-R", false);
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
            bloom += Random.Range(-30, 30) * fpsCam.transform.up;
            bloom += Random.Range(-30, 30) * fpsCam.transform.right;
        }
        bloom -= fpsCam.transform.position;
        bloom.Normalize();

        anim.SetBool("isShooting", true);
        anim.SetBool("ShotgunGun1-S", true);
        Invoke ("StopShootAnim", 0.7f);
        shooting = true;

        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;
        RaycastHit hit5;

        shotgundamage = 0;

        FindObjectOfType<MobileLook>().Recoil();
        FindObjectOfType<JoyStick>().Shoot();
        FindObjectOfType<SoundManager>().Shoot();

        muzzleFlash.Play();

        if (Physics.Raycast(fpsCam.transform.position, bloom, out hit)){

            Enemy target = hit.transform.GetComponent<Enemy>();
            
            if (target != null) {
                if (hit.transform.tag == "Enemy") {
                    if (hit.distance > 50) {
                        shotgundamage = shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                    } else if (hit.distance > 35) {
                        shotgundamage = shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                    } else if (hit.distance > 25) {
                        shotgundamage = shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                    } else if (hit.distance > 15){
                        shotgundamage = shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                    } else {
                        shotgundamage = shotgundamage + (int)(damage * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)(damage * gun.damageMultiplier);
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
        
        Vector3 bloom2 = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        if (adsing == false) {
            bloom2 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.up;
            bloom2 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.right;
        } else {
            bloom2 += Random.Range(-30, 30) * fpsCam.transform.up;
            bloom2 += Random.Range(-30, 30) * fpsCam.transform.right;
        }
        bloom2 -= fpsCam.transform.position;
        bloom2.Normalize();

        if (Physics.Raycast(fpsCam.transform.position, bloom2, out hit2)){

            Enemy target = hit2.transform.GetComponent<Enemy>();
            
            if (target != null) {
                if (hit2.transform.tag == "Enemy") {
                    if (hit2.distance > 50) {
                        shotgundamage = shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                    } else if (hit2.distance > 35) {
                        shotgundamage = shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                    } else if (hit2.distance > 25) {
                        shotgundamage = shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                    } else if (hit2.distance > 15){
                        shotgundamage = shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                    } else {
                        shotgundamage = shotgundamage + (int)(damage * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)(damage * gun.damageMultiplier);
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
        
        Vector3 bloom3 = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        if (adsing == false) {
            bloom3 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.up;
            bloom3 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.right;
        } else {
            bloom3 += Random.Range(-30, 30) * fpsCam.transform.up;
            bloom3 += Random.Range(-30, 30) * fpsCam.transform.right;
        }
        bloom3 -= fpsCam.transform.position;
        bloom3.Normalize();

        if (Physics.Raycast(fpsCam.transform.position, bloom3, out hit3)){

            Enemy target = hit3.transform.GetComponent<Enemy>();
            
            if (target != null) {
                if (hit3.transform.tag == "Enemy") {
                    if (hit3.distance > 50) {
                        shotgundamage = shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                    } else if (hit3.distance > 35) {
                        shotgundamage = shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                    } else if (hit3.distance > 25) {
                        shotgundamage = shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                    } else if (hit3.distance > 15){
                        shotgundamage = shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                    } else {
                        shotgundamage = shotgundamage + (int)(damage * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)(damage * gun.damageMultiplier);
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

        Vector3 bloom4 = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        if (adsing == false) {
            bloom4 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.up;
            bloom4 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.right;
        } else {
            bloom4 += Random.Range(-30, 30) * fpsCam.transform.up;
            bloom4 += Random.Range(-30, 30) * fpsCam.transform.right;
        }
        bloom4 -= fpsCam.transform.position;
        bloom4.Normalize();

        if (Physics.Raycast(fpsCam.transform.position, bloom4, out hit4)){

            Enemy target = hit4.transform.GetComponent<Enemy>();
            
            if (target != null) {
                if (hit4.transform.tag == "Enemy") {
                    if (hit4.distance > 50) {
                        shotgundamage = shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 16) * gun.damageMultiplier);
                    } else if (hit4.distance > 35) {
                        shotgundamage = shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 14) * gun.damageMultiplier);
                    } else if (hit4.distance > 25) {
                        shotgundamage = shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 12) * gun.damageMultiplier);
                    } else if (hit4.distance > 15){
                        shotgundamage = shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 6) * gun.damageMultiplier);
                    } else {
                        shotgundamage = shotgundamage + (int)(damage * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)(damage * gun.damageMultiplier);
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

        Vector3 bloom5 = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        if (adsing == false) {
            bloom5 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.up;
            bloom5 += Random.Range(-bloomNum, bloomNum) * fpsCam.transform.right;
        } else {
            bloom5 += Random.Range(-30, 30) * fpsCam.transform.up;
            bloom5 += Random.Range(-30, 30) * fpsCam.transform.right;
        }
        bloom5 -= fpsCam.transform.position;
        bloom5.Normalize();

        if (Physics.Raycast(fpsCam.transform.position, bloom5, out hit5)){

            Enemy target = hit5.transform.GetComponent<Enemy>();
            enemyGameObject = target;
            
            if (target != null) {
                if (hit5.transform.tag == "Enemy") {
                    if (hit5.distance > 50) {
                        shotgundamage = shotgundamage + (int)((damage - 8) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 8) * gun.damageMultiplier);
                    } else if (hit5.distance > 35) {
                        shotgundamage = shotgundamage + (int)((damage - 7) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 7) * gun.damageMultiplier);
                    } else if (hit5.distance > 25) {
                        shotgundamage = shotgundamage + (int)((damage - 5) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 5) * gun.damageMultiplier);
                    } else if (hit5.distance > 15){
                        shotgundamage = shotgundamage + (int)((damage - 3) * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)((damage - 3) * gun.damageMultiplier);
                    } else {
                        shotgundamage = shotgundamage + (int)(damage * gun.damageMultiplier);
                        stats.damageDealt += shotgundamage + (int)(damage * gun.damageMultiplier);
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
        if (shotgundamage != 0) {
            enemyGameObject = enemyGameObject.GetComponent<Enemy>();
            enemyGameObject.TakeDamage(shotgundamage);
        }
        }
    }

    private float damageShotgun;
 
    public void StopShootAnim () {
        anim.SetBool("isShooting", false);
        anim.SetBool("ShotgunGun1-S", false);
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

