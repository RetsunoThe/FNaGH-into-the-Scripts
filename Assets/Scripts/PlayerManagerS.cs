using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerS : MonoBehaviour
{

    //PlayerWalk
    [SerializeField] private Transform PlayerCamera;
    float walkSpeed = 3f;
    Vector3 test = new Vector3(0, 0, 0);

    //Raycast



    LayerMask RaycastMask;



    Vector2 mousePos = new Vector2();
    Vector3 point = new Vector3();

    private void Awake()
    {
        RaycastMask = LayerMask.GetMask("goPosition");

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerWalk();
        RayCast();

    }

    private void RayCast()
    {
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward), out hit, RaycastMask))
        {
            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            print("Did hit");
        }
        else
        {
            Debug.DrawRay(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward) * 100, Color.white);
            print("Didont hit");
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