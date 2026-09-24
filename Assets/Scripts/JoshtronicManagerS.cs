using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class JoshtronicManagerS : MonoBehaviour
{

    PlayerManagerS playerScript;
    [SerializeField] public GameObject[] positions;

    int walkDelay = 500;

    int currentCamera = 0;

    CameraGrid JoshGrid;

    int startX = 3;
    int startY = 4;

    int endingX = 1;
    int endingY = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        

        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

        JoshGrid = new CameraGrid(startX, startY, endingX, endingY);
        JoshGrid.ChangePosition(startX, startY);


        print("Destination: " + endingX + " " + endingY);

        Vector2 cameraInfo = new Vector2(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY());
        currentCamera = JoshGrid.GetCamera(cameraInfo);
        transform.position = positions[currentCamera].transform.position;
        print("Distance from spawn: " + JoshGrid.PathfinderDistance(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY()));
        

    }

    // Update is called once per frame
    private void Update()
    {

        if (walkDelay == 0)
        {
            walkCheck();
            walkDelay = 500;
        }
        else
        {
            walkDelay -= 1;
        }
    }


    private void walkCheck()
    {
        JoshGrid.AnimatronicMovement();

        Vector2 cameraInfo = new Vector2(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY());
        currentCamera = JoshGrid.GetCamera(cameraInfo);
        transform.position = positions[currentCamera].transform.position;
        print("Distance from spawn: " + JoshGrid.PathfinderDistance(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY()));
    }


    







}
