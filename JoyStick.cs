using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
{
    public Animator anim;
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    public float moveSpeed;
    public float moveSpeedMultiplier;

    public float horizontalMovement;
    public float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    public Rigidbody rb;

    [SerializeField] public float walkSpeed;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float adsSpeed;
    [SerializeField] public float crouchSpeed;
    [SerializeField] float acceleration = 10f;

    public bool isGrounded;
    public float groundedDistance = 0.3f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;

    float playerHeight = 3.8f;

    public float jumpForce;
    public float groundDrag;
    public float airDrag;

    RaycastHit slopeHit;

    public GameObject sprintButton;
    public Transform sprintJoystickPos;
    public bool lockSprintBool;
    public bool sprintIsLocked;
    public bool sprintBool;

    public float gravity;

    public bool adsing;
    public bool reloading;
    public bool shooting;
    public bool ocupied;

    public bool isCrouching;
    public BoxCollider collider;
    public float colliderSize;

    public bool isSliding;
    public bool slide;
    public float slideSpeed;

    public bool moving;

    public GameObject joyStickGameObject;
    public RectTransform joystickRect;

    public float maxSlopeAngle;

    public GameObject crouchButton;
    public Sprite crouchImg;
    public Sprite slideImg;

    public int Multiplier;

    public Transform cameraHolder;

    private bool OnSlope () {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 1.9f + 0.5f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection () {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    void Start()
    {
        rb.freezeRotation = true;
    }

    public void Slide () {
            if (OnSlope() == true) {
                rb.AddForce (GetSlopeMoveDirection() * slideSpeed * moveSpeedMultiplier, ForceMode.Acceleration);
            } else {
                rb.AddForce (moveDirection.normalized * slideSpeed * moveSpeedMultiplier, ForceMode.Acceleration);
            }
            if (rb.velocity.y < 0) {
                rb.AddForce (0, gravity * 5, 0);
            }
            Invoke ("StopSlide", 1f);
    }

    public void SlideTest () {
        if (OnSlope()) {

            rb.AddForce (cameraHolder.forward.normalized * 100 * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);

            if (rb.velocity.y > 0) {
                rb.AddForce(Vector3.down * 10f, ForceMode.Force);
            }

            if (rb.velocity.magnitude > 100) {
                rb.velocity = rb.velocity.normalized * 100;
            } else {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                if (flatVel.magnitude > 100) {
                    Vector3 limitedVel = flatVel.normalized * 100;
                    rb.velocity = new Vector3 (limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }

        } else {
            if (sprintIsLocked == true || sprintBool == true ) {
                rb.AddForce (cameraHolder.forward.normalized * 100 * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
            }
            
            if (isGrounded == true) {
                rb.AddForce (cameraHolder.forward.normalized * 100 * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
            }
        }

        if (isGrounded == false) {
            rb.AddForce (0, gravity, 0);
            rb.AddForce (cameraHolder.forward.normalized * 100 * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
        }

        rb.useGravity = !OnSlope();

        Invoke ("StopSlide", 1f);
    }
    public void StopSlide () {
        slide = false;
    }

    void FixedUpdate () {

        if (isGrounded == true && (sprintIsLocked == true || lockSprintBool == true || sprintBool == true) && ocupied == false && isCrouching == false && isSliding == false) {
            crouchButton.GetComponent<Image>().sprite = slideImg;
        } else if (isSliding == false){
            crouchButton.GetComponent<Image>().sprite = crouchImg;
        } else if (isSliding == true) {
            crouchButton.GetComponent<Image>().sprite = slideImg;
        }

        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
        joystickOriginalPos = joyStickGameObject.transform.position;

        if (adsing || reloading || shooting) {
            ocupied = true;
        } else {
            ocupied = false;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position , groundedDistance, groundMask);

        verticalMovement = joystickVec.y;
        horizontalMovement = joystickVec.x;
        MyInput();
        ControlSpeed();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        if (verticalMovement >= 0.65f) {
            sprintBool = true;
        } else {
            sprintBool = false;
        }

        if (isCrouching == true) {
            colliderSize = Mathf.Lerp(colliderSize, 1.9f, acceleration * Time.deltaTime * 0.5f);
            collider.size = new Vector3 (collider.size.x , colliderSize, collider.size.x);
        } else {
            colliderSize = Mathf.Lerp(colliderSize, 3.8f, acceleration * Time.deltaTime * 0.5f);
            collider.size = new Vector3 (collider.size.x , colliderSize, collider.size.x);
        }

        if (slide == true) {
            colliderSize = Mathf.Lerp(colliderSize, 1.5f, acceleration * Time.deltaTime * 0.5f);
            collider.size = new Vector3 (collider.size.x , colliderSize, collider.size.x);
        } else {
            colliderSize = Mathf.Lerp(colliderSize, 3.8f, acceleration * Time.deltaTime * 0.5f);
            collider.size = new Vector3 (collider.size.x , colliderSize, collider.size.x);
        }

        if (slide == false) {
            MovePlayer();
        } else {
            SlideTest();
        }

        if (slide == true && ocupied == false) {
            anim.SetBool("isSliding", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);

            FindObjectOfType<SoundManager>().PlayerMovement(2);
        } else if ((sprintIsLocked == true || sprintBool == true) && moving == true && ocupied == false && isCrouching == false) {
            anim.SetBool("isSliding", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);

            FindObjectOfType<SoundManager>().PlayerMovement(1);
        } else if (moving == true && adsing == false){
            anim.SetBool("isSliding", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);

            FindObjectOfType<SoundManager>().PlayerMovement(0);
        } else if (ocupied == false){
            anim.SetBool("isSliding", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);

            FindObjectOfType<SoundManager>().PlayerMovement(10);
        } else {
            
            anim.SetBool("isSliding", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }

        if ((ocupied == true || adsing == true )&& moving == true) {
            FindObjectOfType<SoundManager>().PlayerMovement(0);
        }

        if (isGrounded == false && slide == false) {
            FindObjectOfType<SoundManager>().PlayerMovement(1);
        }

        if (moving == false) {
            FindObjectOfType<SoundManager>().PlayerMovement(10);
        }
    }

    void ControlSpeed () {
        if (sprintIsLocked == true || sprintBool == true && isGrounded == true && slide == false) {
            if (isCrouching == true) {
                moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
            } else if (ocupied == true) {
                moveSpeed = Mathf.Lerp(moveSpeed, adsSpeed, acceleration * Time.deltaTime);
            } else {
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            }
        } else {
            if (isCrouching == true) {
                moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
            } else if (ocupied == true) {
                moveSpeed = Mathf.Lerp(moveSpeed, adsSpeed, acceleration * Time.deltaTime);
            } else {
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
        }
    }

    public void EnableSlide () {
        isSliding = false;
    }

    public void Crouch () {
        if (isGrounded == true) {
            if ((sprintIsLocked == true || lockSprintBool == true || sprintBool == true) && ocupied == false && isCrouching == false) {
                if (isSliding == false) {
                    isSliding = true;
                    slide = true;
                    Invoke ("EnableSlide", 1.5f);
                }
            } else {
            if (isCrouching == false) {
                isCrouching = true;
            } else if (isCrouching == true){
                isCrouching = false;
            }              
            }
        }
    }

    public void SlideButton () {
        if (isGrounded == true) {
            if (ocupied == false && isCrouching == false) {
                if (isSliding == false) {
                    isSliding = true;
                    slide = true;
                    Invoke ("EnableSlide", 1.5f);
                }
            }            
        }
    }

    public void StopLockedSprint () {
        if (joystick.transform.position != joystickBG.transform.position) {
        sprintIsLocked = false;
        joystick.transform.position = joystickOriginalPos;
        sprintButton.SetActive(false);
        lockSprintBool = false;
        }
    }

    public void LockSprint () {
        lockSprintBool = true;
    }

    public void UnlockSprint () {
        lockSprintBool = false;
    }

    void MovePlayer() {

        
        if (OnSlope()) {
            if (sprintIsLocked == true || sprintBool == true ) {
                rb.AddForce (GetSlopeMoveDirection() * moveSpeed * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
            }
            
            if (isGrounded == true) {
                rb.AddForce (GetSlopeMoveDirection() * moveSpeed * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
            }

            if (rb.velocity.y > 0) {
                rb.AddForce(Vector3.down * 10f, ForceMode.Force);
            }

            if (rb.velocity.magnitude > moveSpeed) {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            } else {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                if (flatVel.magnitude > moveSpeed) {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3 (limitedVel.x * Time.deltaTime, rb.velocity.y, limitedVel.z * Time.deltaTime);
                }
            }

        } else {
            if (sprintIsLocked == true || sprintBool == true ) {
                rb.AddForce (moveDirection.normalized * moveSpeed * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
            }
            
            if (isGrounded == true) {
                rb.AddForce (moveDirection.normalized * moveSpeed * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
            }
        }

        if (isGrounded == false) {
            rb.AddForce (0, gravity, 0);
            rb.AddForce (moveDirection.normalized * moveSpeed * 0.7f * moveSpeedMultiplier, ForceMode.Acceleration);
        }

        rb.useGravity = !OnSlope();

    }

    public void MoveSpeedMultiplier (float m) {
        moveSpeedMultiplier = m;
    }

    void MyInput () {
        if (sprintIsLocked == true) {
            moveDirection = cameraHolder.forward;
        } else {
            moveDirection = cameraHolder.forward * verticalMovement + cameraHolder.right * horizontalMovement;
        }
    }

    public void PointerDown()
    {
        moving = true;
        joystickTouchPos = joystickBG.transform.position;
        sprintButton.SetActive(true);

    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        sprintIsLocked = false;

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
        if (lockSprintBool == false) {
            joystickVec = Vector2.zero;
            joystick.transform.position = joystickOriginalPos;
            joystickBG.transform.position = joystickOriginalPos;
            sprintButton.SetActive(false);
            sprintIsLocked = false;
            moving = false;
        } else {
            joystickVec = Vector2.zero;
            joystick.transform.position = new Vector2 (sprintJoystickPos.position.x, (sprintButton.transform.position.y + sprintJoystickPos.position.y) / 2);
            joystickBG.transform.position = joystickOriginalPos;
            sprintIsLocked = true;
        }
    }

    public void Jump () {
        if (isGrounded == true) {
            if (isCrouching == true) {
                isCrouching = false;
            } else {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce (transform.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void Ads () {
        adsing = true;
    }
    public void UnAds () {
        adsing = false;
    }
    public void Reload () {
        reloading = true;
    }
    public void UnReload () {
        reloading = false;
    }
    public void Shoot () {
        shooting = true;
    }
    public void UnShoot () {
        shooting = false;
    }
}
