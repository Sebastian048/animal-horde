using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileLook : MonoBehaviour
{
    public bool canLookAround;
    
    public SniperGun1 sniper;

    public GunManager gunManager;
    public Transform cameraTransform;

    public float cameraSensitivity;

    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    public Vector2 lookInput;
    public float cameraPitch;

    public bool adsing;

    public float moveX;
    public float moveY;

    public Vector2 currentRotation;
    public Vector2 targetRotation;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    public Vector2 lookAroundx;

    public float xReturn;
    public float yReturn;
    
    public bool touching;
    public bool moving;

    public Options options;

    public float timeCount;


    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenWidth = Screen.width / 2;

    }

    private bool shooting;

    public float timer;

    public void Recoil () {
        shooting = true;

        if (gunManager.gun == 0) {
            recoilX = 0.15f;
            recoilY = 0.15f;
        } else if (gunManager.gun == 1) {
            recoilX = 0.3f;
            recoilY = 0.2f;
        } else if (gunManager.gun == 2) {
            recoilX = 0f;
            recoilY = 0f;
        } else if (gunManager.gun == 3) {
            recoilX = 0f;
            recoilY = 0f;
        } else if (gunManager.gun == 4) {
            recoilX = 0.2f;
            recoilY = 0.2f;
        } else if (gunManager.gun == 5) {
            recoilX = 0.1f;
            recoilY = 0.1f;
        }
        
        targetRotation += new Vector2 (Random.Range(-recoilX, recoilX), Random.Range(-recoilY / 2, -recoilY));
    }
    public void StopRecoil () {
        shooting = false;
    }

    public float comeBack = 0f;
    public float yReturnLerp;
    public float xReturnLerp;

    void Update () {

        if (canLookAround == true) {

        if (shooting == true) {
            timer = Time.deltaTime + timer;
        } else {
            timer = 0;
        }

        targetRotation = Vector2.Lerp(targetRotation, Vector2.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector2.Lerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        
        //xAxis Recoil
        if (shooting == true || moving == true) {
            lookAroundx.x = lookInput.x + currentRotation.x / 10f;
        } else {
            comeBack = Time.deltaTime + comeBack;
            xReturnLerp = Mathf.Lerp(currentXRotation, xReturn, comeBack);
            lookAroundx.x = xReturnLerp;
        }
        
        //yAxis Recoil
        if (shooting == true || moving == true) {
            cameraPitch = cameraPitch - lookInput.y;
            cameraPitch = cameraPitch + currentRotation.y / 5f;
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
            comeBack = 0;
        } else{
            comeBack = Time.deltaTime + comeBack;
            yReturnLerp = Mathf.Lerp(cameraPitch, yReturn, comeBack);
            cameraPitch = yReturnLerp;
        }  

        if (moving == true || shooting == true) {
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
            transform.Rotate (transform.up, lookAroundx.x);
        } else {
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
            transform.localRotation = Quaternion.Euler (0, lookAroundx.x, 0);
            transform.Rotate (transform.up, 0, 0);
        }

        currentXRotation = transform.localRotation.eulerAngles.y;
        if (moving == true) {
            xReturn = transform.localRotation.eulerAngles.y;
            yReturn = cameraTransform.localRotation.eulerAngles.x;
            if (yReturn > 91) {
                yReturn = yReturn - 360;
            }
        }

        if (touching == false) {
            lookInput.x = 0;
            lookInput.y = 0;
        }
        moveX = lookInput.x;
        moveY = lookInput.y;

        if (gunManager.gun == 3 && sniper.adsing == true) {
            cameraSensitivity = options.sensADS / 2;
        } else if (adsing == false) {
            cameraSensitivity = options.sens * 2;
        } else {
            cameraSensitivity = options.sensADS;
        }

        GetTouchInput();
        }
        
    }

    public float currentXRotation;

    void GetTouchInput () {

        for (int i = 0; i < Input.touchCount; i++) {
            Touch t = Input.GetTouch(i);

            switch (t.phase) {
                case TouchPhase.Began:

                if (t.position.x < halfScreenWidth && leftFingerId == -1) {
                    leftFingerId = t.fingerId;
                } else if (t.position.x > halfScreenWidth && rightFingerId == -1){
                    touching = true;
                    rightFingerId = t.fingerId;
                }

                break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                if (t.fingerId == leftFingerId) {
                    leftFingerId = -1;
                } else if (t.fingerId == rightFingerId) {
                    rightFingerId = -1;
                    touching = false;
                    moving = false;
                }

                break;
                case TouchPhase.Moved:

                if (t.fingerId == rightFingerId) {
                    moving = true;
                    lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                }

                break;
                case TouchPhase.Stationary:
                
                if (t.fingerId == rightFingerId) {
                    moving = false;
                    lookInput = Vector2.zero;
                }
                break;
            }
        }
    }

    public void Ads () {
        adsing = true;
    }

    public void UnAds () {
        adsing = false;
    }
}
