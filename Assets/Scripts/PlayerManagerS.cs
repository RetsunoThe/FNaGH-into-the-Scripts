
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
    bool inCamera = false;
    int cameraNumber;
    int lastCameraNumber = 0;
    public GameObject[] cameras;
    Transform computerChair;

    //Smoothmoving
    Vector3 moveTowards;
    bool inMovement = false;

    //PlayerWalk
    bool canWalk = true;
    [SerializeField] private Transform PlayerCamera;
    float walkSpeed = 3f;
    Vector3 test = new Vector3(0, 0, 0);

    //Raycast
    GameObject hitObject;
    public GameObject hitObjectPosition;
    LayerMask RaycastMask;

    //Position checks
    public bool inPosition = false;
    inPositionS _inPositionReturnS;
    Transform _inPositionReturn;

    //Flashlight
    public bool isFlashlightOn = false;



    private void Awake()
    {
        RaycastMask = LayerMask.GetMask("raycastObject");
        cameras = GameObject.FindGameObjectsWithTag("playerCameras");

    }
    private void Start()
    {
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
        if (canWalk == true)
        {
            PlayerWalk();
        }

        //Return from position
        if (inPosition == true && inCamera == false && Mouse.current.rightButton.wasPressedThisFrame)
        {
            _inPositionReturnS = hitObjectPosition.GetComponent<inPositionS>();
            _inPositionReturn = _inPositionReturnS.inPositionReturn;

            transform.position = _inPositionReturn.transform.position;

            hitObjectPosition = null;

            inPosition = false;
            canWalk = true;
        }

        //Moving animation
        if (moveTowards != null && canWalk == false && inCamera == false)
        {
            transform.position = moveTowards;
            moveTowards = Vector3.MoveTowards(transform.position, hitObjectPosition.transform.position, 1);
        }

        //Moving through cameras
        if (inCamera == true)
        {
            if (Keyboard.current.aKey.wasPressedThisFrame && cameraNumber > 0)
            {
                cameraNumber -= 1;
                transform.position = cameras[cameraNumber].transform.position;
            }
            if (Keyboard.current.dKey.wasPressedThisFrame && cameraNumber < 3)
            {
                cameraNumber += 1;
                transform.position = cameras[cameraNumber].transform.position;
            }
        }

        //Exit cameras

        if (inCamera == true && Mouse.current.rightButton.wasPressedThisFrame)
        {
            lastCameraNumber = cameraNumber;
            transform.position = computerChair.position;
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
                
                heldObject = null;
                heldObjectReturn = new Vector3();
                
                holdingObject = false;
            }
        }




        RayCast();
    }









    private void RayCast()
    {
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, RaycastMask))
        {
            hitObject = hit.collider.gameObject;

            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            //print("did hit");

            if (Mouse.current.leftButton.wasPressedThisFrame && inPosition == false && hitObject.tag == "goPosition")
            {
                if (hitObject.tag == "goPosition")
                {
                    hitObjectPosition = hitObject;
                }
                
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
                computerChair = transform;
                transform.position = cameras[cameraNumber].transform.position;
            }

            if (Keyboard.current.eKey.wasPressedThisFrame && inPosition == false && holdingObject == false && hitObject.tag == "grabObject")
            {
                if (hitObject.tag == "grabObject")
                {
                    heldObject = hitObject;
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
    
}