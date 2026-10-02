using UnityEngine;

public class ReneetronicManagerS : MonoBehaviour
{

    PlayerManagerS playerScript;
    [SerializeField] public GameObject[] positions;

    //Walking Through Cameras

    int walkDelay = 500;

    [SerializeField] private int currentCamera = 0;

    CameraGrid ReneeGrid;

    [SerializeField] private int startX = 0;
    [SerializeField] private int startY = 3;

    [SerializeField] private int endingX = 4;
    [SerializeField] private int endingY = 0;

    int atDoor = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();






        ReneeGrid = new CameraGrid(startX, startY, endingX, endingY);
        ReneeGrid.ChangePosition(startX, startY);


        print("Destination: " + endingX + " " + endingY);

        Vector2 cameraInfo = new Vector2(ReneeGrid.GetCurrentX(), ReneeGrid.GetCurrentY());
        currentCamera = ReneeGrid.GetCamera(cameraInfo);
        transform.position = positions[currentCamera].transform.position;
        print("Distance from spawn: " + ReneeGrid.PathfinderDistance(ReneeGrid.GetCurrentX(), ReneeGrid.GetCurrentY()));
        

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
                transform.position = positions[currentCamera].transform.position;
                walkDelay = 600;
            }
            if (currentCamera == 5 && atDoor == 0)
            {
                atDoor = 2;
                currentCamera = 15;
                transform.position = positions[currentCamera].transform.position;
                walkDelay = 600;
            }
            if (currentCamera != 0 || currentCamera !=5) {
                if (atDoor == 0)
                {
                    ReneeGrid.AnimatronicMovement();
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
                        if (playerScript.heldObject.name == "Mask1" && playerScript.hitObjectPosition.name == "Position1")
                        {
                            ReneeGrid.SetCurrentX(startX);
                            ReneeGrid.SetCurrentY(startY);
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
                        if (playerScript.heldObject.name == "Mask1" && playerScript.hitObjectPosition.name == "Position2")
                            {
                                ReneeGrid.SetCurrentX(startX);
                                ReneeGrid.SetCurrentY(startY);
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

        Vector2 cameraInfo = new Vector2(ReneeGrid.GetCurrentX(), ReneeGrid.GetCurrentY());
        currentCamera = ReneeGrid.GetCamera(cameraInfo);
        transform.position = positions[currentCamera].transform.position;
        //print("Distance from spawn: " + JoshGrid.PathfinderDistance(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY()));
    }


    private void jumpscareEvent()
    {
        print("YOU ARE DEEEEEEAD");
    }







}
