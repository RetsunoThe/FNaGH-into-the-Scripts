using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;




public class DoorAnimation : MonoBehaviour
{
    public GameObject DoorHolder;

    float openAngle; 
    public float openSpeed = 4f;
    public bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine currentCoroutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame


    public void CallAnimationOpen()
    {
        closedRotation = transform.rotation;
        openRotation = (Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0)));

        openAngle = -45;

        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ToggleDoor());

    }

    public void CallAnimationClose()
    {
        closedRotation = transform.rotation;
        openRotation = (Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0)));

        openAngle = 45;

        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ToggleDoor());

    }

    private IEnumerator ToggleDoor()
    {
        print("Door activating");

        Quaternion targetRotation = isOpen ? closedRotation : openRotation;
        isOpen = !isOpen;

        while(Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed );
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
