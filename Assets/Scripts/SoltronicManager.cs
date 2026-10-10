using UnityEngine;

public class SoltronicManagerS : MonoBehaviour
{

    PlayerManagerS playerScript;
    [SerializeField] public GameObject[] positions;
    LarktronicManager larkScript;

    //Walking Through Cameras

    int walkDelay = 500;

    public int currentCamera = 0;

    CameraGrid SolGrid;

    [SerializeField] private int startX = 0;
    [SerializeField] private int startY = 3;

    [SerializeField] private int endingX = 4;
    [SerializeField] private int endingY = 0;

    int atLark = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();
        larkScript = GameObject.FindGameObjectWithTag("lark").GetComponent<LarktronicManager>();





        SolGrid = new CameraGrid(startX, startY, endingX, endingY);
        SolGrid.ChangePosition(startX, startY);


        print("Destination: " + endingX + " " + endingY);

        Vector2 cameraInfo = new Vector2(SolGrid.GetCurrentX(), SolGrid.GetCurrentY());
        currentCamera = SolGrid.GetCamera(cameraInfo);
        transform.position = positions[currentCamera].transform.position;
        print("Distance from spawn: " + SolGrid.PathfinderDistance(SolGrid.GetCurrentX(), SolGrid.GetCurrentY()));
        

    }

    // Update is called once per frame
    private void Update()
    {
        if (walkDelay == 0)
        {

            if (currentCamera == larkScript.currentCamera && atLark == 0)
            {
                atLark = 1;
                walkDelay = 500;
            }
            if (atLark == 1)
            {
                foundLarkEvent();
            }
            if (currentCamera != larkScript.currentCamera)
            {
                SolGrid.AnimatronicMovement();
                walkCheck();
                walkDelay = 500;
            }
        }
        else
        {
            walkDelay -= 1;
        }
    }


    private void foundLarkEvent()
    {
        if (playerScript.inCamera == true && playerScript.isFlashlightOn == true && playerScript.cameraNumber == currentCamera)
        {
            
        }
        else
        {
            jumpscareEvent();
        }

    }

    private void walkCheck()
    {
        SolGrid.SetDestionation(larkScript.LarkGrid.GetCurrentX(), larkScript.LarkGrid.GetCurrentY());

        Vector2 cameraInfo = new Vector2(SolGrid.GetCurrentX(), SolGrid.GetCurrentY());
        currentCamera = SolGrid.GetCamera(cameraInfo);
        transform.position = positions[currentCamera].transform.position;
        //print("Distance from spawn: " + JoshGrid.PathfinderDistance(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY()));
    }










    private void jumpscareEvent()
    {
        print("YOU ARE DEEEEEEAD");
    }







}
