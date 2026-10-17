using UnityEngine;

public class LarktronicManager : MonoBehaviour
{

    PlayerManagerS playerScript;

    //Walking Through Cameras

    int walkDelay = 500;

    public int currentCamera = 3;

    public CameraGrid LarkGrid;

    [SerializeField] private int startX = 3;
    [SerializeField] private int startY = 4;

    [SerializeField] private int endingX = 4;
    [SerializeField] private int endingY = 0;

    int atDoor = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();






        LarkGrid = new CameraGrid(startX, startY, endingX, endingY);
        LarkGrid.ChangePosition(startX, startY);


        print("Destination: " + endingX + " " + endingY);

        Vector2 cameraInfo = new Vector2(LarkGrid.GetCurrentX(), LarkGrid.GetCurrentY());
        currentCamera = LarkGrid.GetCamera(cameraInfo);
        print("Distance from spawn: " + LarkGrid.PathfinderDistance(LarkGrid.GetCurrentX(), LarkGrid.GetCurrentY()));
        

    }

    // Update is called once per frame
    private void Update()
    {
        if (walkDelay == 0)
        {
            
            if (atDoor != 0)
            {
                atDoorEvent();
            }
            if (currentCamera == 0 && atDoor == 0)
            {
                atDoor = 1;
                currentCamera = 14;

                walkDelay = 600;
            }
            if (currentCamera == 5 && atDoor == 0)
            {
                atDoor = 2;
                currentCamera = 15;
                walkDelay = 600;
            }
            if (currentCamera != 0 || currentCamera !=5) {
                if (atDoor == 0)
                {
                    LarkGrid.AnimatronicMovement1();
                    walkCheck();
                    walkDelay = 500;
                }
            }
        }
        else
        {
            walkDelay -= 1;
        }
    }

    private void atDoorEvent()
    {

        switch (atDoor)
        {
            case 1:
                if (atDoor == 1 && playerScript.isFlashlightOn == true && playerScript.heldObject != null && playerScript.hitObjectPosition != null)
                    {
                        if (playerScript.heldObject.name == "Mask0" && playerScript.hitObjectPosition.name == "Position1")
                        {
                            LarkGrid.SetCurrentX(startX);
                            LarkGrid.SetCurrentY(startY);
                            atDoor = 0;
                            walkCheck();

                        }
                        else
                        {
                        jumpscareEvent();
                        }
  
                    }
                else
                {
                    jumpscareEvent();
                }
                        
                break;
            case 2:
                if (atDoor == 2 && playerScript.isFlashlightOn == true && playerScript.heldObject != null && playerScript.hitObjectPosition != null)
                    {
                        if (playerScript.heldObject.name == "Mask0" && playerScript.hitObjectPosition.name == "Position2")
                            {
                                LarkGrid.SetCurrentX(startX);
                                LarkGrid.SetCurrentY(startY);
                                atDoor = 0;
                                walkCheck();

                            }
                            else
                            {
                            jumpscareEvent();
                            }

                        }
                    else
                    {
                        jumpscareEvent();
                    }
                break;
        }
        
        
    }

private void walkCheck()
    {

        //Here we first get the current newly changed animatronic position and store it in cameraInfo to then convert the native 
        //currentCamera variable into its respective number.
        Vector2 cameraInfo = new Vector2(LarkGrid.GetCurrentX(), LarkGrid.GetCurrentY());
        currentCamera = LarkGrid.GetCamera(cameraInfo);

        //Then we run the renderings for the animatronic to be updated in the cameras.
        WipeAllCameras();
        CameraVisualizer();
    }

    private void CameraVisualizer()
    {
        if (currentCamera >= 0 && currentCamera <= 13)
        {
            GameObject[] CameraVisualizer = playerScript.CameraVisualizers[currentCamera].GetComponent<CameraViewManager>().CameraAnimatronics;
            CameraVisualizer[3].SetActive(true);
        }
        
    }

    private void WipeAllCameras()
    {
        GameObject[] CameraVisualizer = playerScript.CameraVisualizers;
        for (int i = 0; i < CameraVisualizer.Length; i ++)
        {
           CameraVisualizer[i].GetComponent<CameraViewManager>().CameraAnimatronics[1].SetActive(false);
        }
    }


    private void jumpscareEvent()
    {
        print("YOU ARE DEEEEEEAD");
    }







}
