using UnityEngine;

public class SoltronicManagerS : MonoBehaviour
{

    PlayerManagerS playerScript;
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
        print("stop fucking bugging");
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();
        larkScript = GameObject.FindGameObjectWithTag("lark").GetComponent<LarktronicManager>();


        SolGrid = new CameraGrid(startX, startY, endingX, endingY);
        SolGrid.ChangePosition(startX, startY);


        Vector2 cameraInfo = new Vector2(SolGrid.GetCurrentX(), SolGrid.GetCurrentY());
        currentCamera = SolGrid.GetCamera(cameraInfo);
        print("Distance from spawn: " + SolGrid.PathfinderDistance(SolGrid.GetCurrentX(), SolGrid.GetCurrentY()));

        CameraVisualizer();
        

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
                FoundLarkEvent();
            }
            if (currentCamera != larkScript.currentCamera)
            {
                SolGrid.AnimatronicMovement1();
                walkCheck();
                walkDelay = 500;
            }
        }
        else
        {
            walkDelay -= 1;
        }
    }


    private void FoundLarkEvent()
    {
        if (playerScript.inCamera == true && playerScript.isFlashlightOn == true && playerScript.cameraNumber == currentCamera)
        {
            
        }
        else
        {
            jumpscareEvent();
        }

    }


    private void PositionRandomizer()
    {
        int min = 1;

        int solPosX = 1;
        int solPosY = 1;

        int larkPosX = 1;
        int larkPosY = 1;

        switch(min)
        {
            case 1:
            solPosX = 0;
            solPosY = 3;

            larkPosX = 4;
            larkPosY = 2;
            break;

            case 2:
            solPosX = 4;
            solPosY = 0;

            larkPosX = 3;
            larkPosY = 4;
            break;

            case 3:
            solPosX = 4;
            solPosY = 0;

            larkPosX = 0;
            larkPosY = 3;
            break;
        }

        SolGrid.SetCurrentX(solPosX);
        SolGrid.SetCurrentY(solPosY);
        larkScript.LarkGrid.SetCurrentX(larkPosX);
        larkScript.LarkGrid.SetCurrentY(larkPosY);
    }

private void walkCheck()
    {

        //Here we first get the current newly changed animatronic position and store it in cameraInfo to then convert the native 
        //currentCamera variable into its respective number.
        Vector2 cameraInfo = new Vector2(SolGrid.GetCurrentX(), SolGrid.GetCurrentY());
        currentCamera = SolGrid.GetCamera(cameraInfo);

        //Then we run the renderings for the animatronic to be updated in the cameras.
        WipeAllCameras();
        CameraVisualizer();
    }

    private void CameraVisualizer()
    {
        if (currentCamera >= 0 && currentCamera <= 13)
        {
            print("bhtekjlsadtgfdsffedsasghytefdadgjreersfadghytersfddgjtyersfdgjtyeafsdhdgty4");
            GameObject[] CameraVisualizer = playerScript.CameraVisualizers[currentCamera].GetComponent<CameraViewManager>().CameraAnimatronics;
            CameraVisualizer[5].SetActive(true);
        }
        
    }

    private void WipeAllCameras()
    {
        GameObject[] CameraVisualizer = playerScript.CameraVisualizers;
        for (int i = 0; i < CameraVisualizer.Length; i ++)
        {
           CameraVisualizer[i].GetComponent<CameraViewManager>().CameraAnimatronics[5].SetActive(false);
        }
    }










    private void jumpscareEvent()
    {
        print("YOU ARE DEEEEEEAD");
    }







}
