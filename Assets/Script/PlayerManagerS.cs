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



    private void Start()
    {
        
    }



    private void Update()
    {
        PlayerWalk();

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