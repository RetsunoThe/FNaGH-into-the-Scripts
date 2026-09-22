using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class JoshtronicManagerS : MonoBehaviour
{

    PlayerManagerS playerScript;
    [SerializeField] public GameObject[] positions;

    int walkDelay = 500;

    int currentCamera = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        transform.position = positions[currentCamera].transform.position;

        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (walkDelay == 0)
        {
            walkCheck();
        }
        else
        {
            walkDelay -= 1;
        }
    }


    private void walkCheck()
    {
        if (currentCamera == 4)
        {
                if(playerScript.isFlashlightOn == true && playerScript.holdingObject == true && playerScript.inPosition == true)
                {
                    if(playerScript.heldObject.name == "Mask0" && playerScript.hitObjectPosition.name == "Position1")
                    {
                        currentCamera = 0;
                        transform.position = positions[currentCamera].transform.position;
                    }
                    else
                    {
                        Jumpscare();
                    }
                }
                else
                {
                    Jumpscare();
                }
        }
        else
        {
            if (currentCamera < positions.Length - 1)
            {
                currentCamera += 1;
                transform.position = positions[currentCamera].transform.position;
            }
        }
        walkDelay = 250;
    }

    private void Jumpscare()
    {
        print("YO'URE DEEEEEEEAD");
    }



}
