
using System;
using System.Collections;
using NUnit.Framework;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerManagerS : MonoBehaviour
{

    [SerializeField] PlayerMovementS playerMovement; 
    private bool messageAssistance = true;

    private Coroutine Coroutine;
    private Coroutine Coroutine1;

    //Holding Objects
    public bool holdingObject = false;
    public GameObject heldObject;
    Vector3 heldObjectReturn;
    
    //Cameras
    [SerializeField] private GameObject Canvas;
    public bool inCamera = false;
    public int cameraNumber;
    int lastCameraNumber = 0;
    Transform computerChair;

    //Smoothmoving
    Vector3 moveTowards;
    bool inMovement = false;

    
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] private InputActionReference moveAction;

    private CharacterController _characterController;
    private Vector2 _moveInput;

    //PlayerWalk
    //bool playerMovement.canWalk = true;
    [SerializeField] private Transform PlayerCamera;
    Vector3 test = new Vector3(0, 0, 0);
    bool isGrounded;
    private float verticalVelocity;

    private float gravity = -12f;
    private float initialFallValue = -2f;

    

    //Raycast
    GameObject hitObject;
    public GameObject hitObjectPosition;
    LayerMask RaycastMask;

    //Position checks
    public bool inPosition = false;
    inPositionS _inPositionReturnS;
    inPositionS hitObjectS;
    Transform _inPositionReturn;

    //Flashlight
    public bool isFlashlightOn = false;
    


    [SerializeField] private TextMeshProUGUI messageDisplay;
    [SerializeField] private TextMeshProUGUI messageDisplay1;
    


    //Camera Display

    [SerializeField] public GameObject[] CameraVisualizers;


    private void Awake()
    {
        RaycastMask = LayerMask.GetMask("raycastObject");
        _characterController = GetComponent<CharacterController>();

    }
    private void Start()
    {
        Canvas.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        
        messageDisplay.alpha = 0;
        messageDisplay1.alpha = 0;

        playerMovement = GetComponent<PlayerMovementS>();

    }

    private void MessageAssistance(string message)
    {
        messageDisplay1.text = message;

        if (Coroutine1 != null) StopCoroutine(Coroutine1);
            Coroutine1 = StartCoroutine(MessageAnimation(messageDisplay1, 1));
    }

    private void Update()
    {

        transform.position = transform.position + new Vector3(0, 10, 0);
        //if (hitObject != null) hitObject.GetComponent<RaycastActionS>().DetermineAction();

        if (inCamera == true)
        {
            CameraVisualizer();

            if(messageAssistance == true) MessageAssistance("Press RBM to leave cameras");
        }
        else
        {
            CameraVisualizers[lastCameraNumber].SetActive(false);
        }
        



        //Flashlight
        if (Keyboard.current.fKey.isPressed)
        {
            isFlashlightOn = true;
        }
        else
        {
            isFlashlightOn = false;
        }

        //Walking

        isGrounded = _characterController.isGrounded;

        if(messageAssistance == true)
        {
            if(inPosition == true && inCamera == false)
            {
                MessageAssistance("Press RBM to leave position");
            }
            if(inCamera == true)
            {
                MessageAssistance("Press RBM to leave cameras");
            }
            if(holdingObject == true)
            {
                MessageAssistance("Press E to drop object");
            }
        }


        //Return from position
        if (inPosition == true && inCamera == false && Mouse.current.rightButton.wasPressedThisFrame)
        {
            _inPositionReturn = hitObjectS.inPositionReturn.transform;

            transform.position = _inPositionReturn.transform.position;

            print(hitObject.name);

            

            if (hitObject.name == "Door1")
                {
                    hitObject.GetComponent<DoorAnimation>().DoorHolder.GetComponent<DoorAnimation>().CallAnimationClose();
                }

            hitObjectPosition = null;
            hitObjectS = null;

            inPosition = false;
            playerMovement.canWalk = true;
        }

        //Moving animation
        if (moveTowards != null && playerMovement.canWalk == false && inCamera == false)
        {
            transform.position = moveTowards;
            moveTowards = Vector3.MoveTowards(transform.position, hitObjectPosition.transform.position, 1);
        }

        //Exit cameras

        if (inCamera == true && Mouse.current.rightButton.wasPressedThisFrame)
        {
            lastCameraNumber = cameraNumber;
            transform.position = computerChair.position;
            
            Canvas.SetActive(false);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;

            inCamera = false;
        }

        //Holding Objects
        if (holdingObject == true)
        {
            heldObject.transform.position = PlayerCamera.transform.position  + (PlayerCamera.transform.forward * 1) + ((PlayerCamera.transform.right * 1)/2);
            heldObject.transform.rotation = PlayerCamera.rotation;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                heldObject.transform.position = heldObjectReturn;
                heldObject.transform.rotation = quaternion.Euler(0, 0, 0);
                
                heldObject.GetComponent<BoxCollider>().enabled = true;
                heldObject = null;
                heldObjectReturn = new Vector3();
                
                holdingObject = false;
            }
        }

        RayCast();

    }

    private void OnEnable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;
    }
    private void OnDisable()
    {
        moveAction.action.performed -= StoreMovementInput;
        moveAction.action.canceled -= StoreMovementInput;
    }

    private void StoreMovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        var move = PlayerCamera.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y)).normalized;
        var currentSpeed = walkSpeed;
        var finalMove = move * currentSpeed;
        finalMove.y = verticalVelocity;

        _characterController.Move(finalMove * Time.deltaTime);

    }

    private void HandleGravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = initialFallValue;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }



    // private void Raycast()
    // {
    //     RaycastHit hit;

    //     if(Physics.Raycast(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward),out hit, 7, RaycastMask))
    //     {
    //         hitObject = hit.collider.gameObject;

    //         hitObject.GetComponent<RaycastActionS>().DetermineAction();
    //     }
    // }




    private void RayCast()
    {
        
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward), out hit, 7, RaycastMask))
        {
            hitObject = hit.collider.gameObject;

             RayCastMessage message = hitObject.GetComponent<RayCastMessage>();
            messageDisplay1.text = message.raycastMessageHover; 

            if(messageAssistance == true && inPosition == false) MessageAssistance(message.raycastMessageHover);


            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (Mouse.current.leftButton.wasPressedThisFrame && inPosition == false && hitObject.tag == "goPosition")
            {
                if (hitObject.tag == "goPosition")
                {
                    hitObjectS = hitObject.GetComponent<inPositionS>();
                    hitObjectPosition = hitObjectS.inPositionGo;
                }
                
                //transform.position = hitObject.transform.position;
                inPosition = true;
                playerMovement.canWalk = false;
                inMovement = true;
                moveTowards = Vector3.MoveTowards(transform.position, hitObject.transform.position, 1);

                if (Coroutine1 != null) StopCoroutine(Coroutine1);

                if (hitObject.name == "Door1")
                {
                    hitObject.GetComponent<DoorAnimation>().DoorHolder.GetComponent<DoorAnimation>().CallAnimationOpen();
                }
            }

            if (Mouse.current.leftButton.wasPressedThisFrame && inPosition == true && inCamera == false && hitObject.tag == "puter")
            {
                inCamera = true;
                cameraNumber = lastCameraNumber;
                Canvas.SetActive(true);
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                computerChair = transform;

                if (Coroutine1 != null) StopCoroutine(Coroutine1);
            }

            if (Keyboard.current.eKey.wasPressedThisFrame && inPosition == false && holdingObject == false && hitObject.tag == "grabObject")
            {
                if (hitObject.tag == "grabObject")
                {
                    heldObject = hitObject;
                    heldObject.GetComponent<BoxCollider>().enabled = false;
                }
                heldObjectReturn = heldObject.transform.position;
                holdingObject = true;

                if (Coroutine1 != null) StopCoroutine(Coroutine1);
                
            }

            if (Keyboard.current.eKey.wasPressedThisFrame && inPosition == false && holdingObject == false && hitObject.tag == "loreObject")
            {
                RayCastMessage message1 = hitObject.GetComponent<RayCastMessage>();
                messageDisplay.text = message1.raycastMessage; 


                if (Coroutine != null) StopCoroutine(Coroutine);
                
                Coroutine = StartCoroutine(MessageAnimation(messageDisplay, 4));

                if (Coroutine1 != null) StopCoroutine(Coroutine1);
                
            }


        }
        else
        {
            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * 100, Color.white);

        }

        

    }

    private IEnumerator MessageAnimation(TextMeshProUGUI display, float duration)
    {
        yield return Fade(1f, 0f, duration, display);
    }

    private IEnumerator Fade(float from, float to, float duration, TextMeshProUGUI display)
    {
        float t = 0f;

        {
            while (t < duration)
            {
                t += Time.deltaTime;
                display.alpha = Mathf.Lerp(from, to, t / duration);
                yield return null;
            }
            display.alpha = to;
        }

        
    }


    private void CameraVisualizer()
    {
        for (int i = 0; i < CameraVisualizers.Length; i ++)
        {
            CameraVisualizers[i].SetActive(false);
        }
        CameraVisualizers[cameraNumber].SetActive(true);
    }




    public void cameraClickEvent(int camera)
    {
        for (int i = 0; i < CameraVisualizers.Length; i ++)
        {
            CameraVisualizers[i].SetActive(false);
        }
        cameraNumber = camera;
    }
}


