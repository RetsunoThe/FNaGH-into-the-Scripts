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
    public Camera main;
    RaycastHit2D hit;
    Vector2 mousePos = new Vector2();
    Vector3 point = new Vector3();

    private void Start()
    {
        
    }



    private void Update()
    {
        PlayerWalk();
        RayCast();

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
    
    private void RayCast()
    {
        mousePos = Mouse.current.position.ReadValue();
        point = main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        hit = Physics2D.Raycast(transform.position, point);
        Debug.Log(hit.collider);
        Debug.DrawRay(transform.position, point);
    }
}