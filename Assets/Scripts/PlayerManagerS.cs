
using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerS : MonoBehaviour
{

    //Holding Objects
    public bool holdingObject = false;
    public GameObject heldObject;
    Vector3 heldObjectReturn;
    
    //Cameras
    [SerializeField] private GameObject Canvas;
    public bool inCamera = false;
    int cameraNumber;
    int lastCameraNumber = 0;
    public GameObject[] cameras;
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
    bool canWalk = true;
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



    private void Awake()
    {
        RaycastMask = LayerMask.GetMask("raycastObject");
        _characterController = GetComponent<CharacterController>();

    }
    private void Start()
    {
        Canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
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
        if (canWalk == true)
        {
            //PlayerWalk();
            HandleGravity();
            HandleMovement();
        }

        //Return from position
        if (inPosition == true && inCamera == false && Mouse.current.rightButton.wasPressedThisFrame)
        {
            _inPositionReturn = hitObjectS.inPositionReturn.transform;

            transform.position = _inPositionReturn.transform.position;

            hitObjectPosition = null;
            hitObjectS = null;

            inPosition = false;
            canWalk = true;
        }

        //Moving animation
        if (moveTowards != null && canWalk == false && inCamera == false)
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
            Cursor.lockState = CursorLockMode.Locked;

            inCamera = false;
        }

        RayCast();

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




    private void RayCast()
    {
        
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward), out hit, 7, RaycastMask))
        {
            hitObject = hit.collider.gameObject;

            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (Mouse.current.leftButton.wasPressedThisFrame && inPosition == false && hitObject.tag == "goPosition")
            {
                if (hitObject.tag == "goPosition")
                {
                    hitObjectS = hitObject.GetComponent<inPositionS>();
                    hitObjectPosition = hitObjectS.inPositionGo;
                }
                print("It's selecting");
                
                //transform.position = hitObject.transform.position;
                inPosition = true;
                canWalk = false;
                inMovement = true;
                moveTowards = Vector3.MoveTowards(transform.position, hitObject.transform.position, 1);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame && inPosition == true && hitObject.tag == "puter")
            {
                inCamera = true;
                cameraNumber = lastCameraNumber;
                Canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                computerChair = transform;
                transform.position = cameras[cameraNumber].transform.position;
                PlayerCamera.rotation = cameras[cameraNumber].transform.rotation;
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
                
            }


        }
        else
        {
            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * 100, Color.white);

        }

        

    }

    private void PlayerWalk()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            test += PlayerCamera.forward * walkSpeed;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            test -= PlayerCamera.forward * walkSpeed;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            test -= PlayerCamera.right * walkSpeed;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            test += PlayerCamera.right * walkSpeed;
        }
        test.y = 0;
        transform.position += test * Time.deltaTime * walkSpeed;

        test = new Vector3(0, 0, 0);
    }
    





    public void cameraClickEvent(int camera)
    {
        cameraNumber = camera;
        print(cameras[cameraNumber]);
        transform.position = cameras[cameraNumber].transform.position;
        PlayerCamera.rotation = cameras[cameraNumber].transform.rotation;
    }
}