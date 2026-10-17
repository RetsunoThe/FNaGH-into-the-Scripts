using UnityEngine;

public abstract class AnimatronicObject
{

    protected CameraGrid AnimatronicGrid;
    protected static PlayerManagerS playerScript;

    protected int currentCamera;

    protected int startX;
    protected int startY;

    protected int endingX;
    protected int endingY;

    protected int baseWalkDelay;
    protected int difficultyLevel;

    protected int displayNumber;

    protected int walkPattern;
    


    public AnimatronicObject(int startX, int startY, int endingX, int endingY, int difficultyLevel, int displayNumber, int walkPattern)
    {
        this.startX = startX;
        this.startY = startY;

        this.endingX = endingX;
        this.endingY = endingY;

        AnimatronicGrid = new CameraGrid(startX, startY, endingX, endingY);

        this.difficultyLevel = difficultyLevel;
        baseWalkDelay = 500;

        this.displayNumber = displayNumber;

        this.walkPattern = walkPattern;

        
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

        RefreshCameras();
        
    }

    public void RefreshCameras()
    {
        Vector2 cameraInfo = new Vector2(AnimatronicGrid.GetCurrentX(), AnimatronicGrid.GetCurrentY());
        currentCamera = AnimatronicGrid.GetCamera(cameraInfo);

        WipeAllCameras();
        CameraVisualizer();
    }
    public void CameraVisualizer()
    {
        if (currentCamera >= 0 && currentCamera <= 13)
        {
            GameObject[] cameraVisualizer = playerScript.CameraVisualizers[currentCamera].GetComponent<CameraViewManager>().CameraAnimatronics;
            cameraVisualizer[displayNumber].SetActive(true);
        }
    }
    public void WipeAllCameras()
    {
        GameObject[] CameraVisualizer = playerScript.CameraVisualizers;
        for (int i = 0; i < CameraVisualizer.Length; i ++)
        {
           CameraVisualizer[i].GetComponent<CameraViewManager>().CameraAnimatronics[displayNumber].SetActive(false);
        }
    }

    public void WalkPatternRandom()
    {
        if (baseWalkDelay == 0)
        {
            AnimatronicGrid.AnimatronicMovement2();
            RefreshCameras();
            baseWalkDelay = 500;
            int randomizer = AnimatronicQOL.random.Next(3);
                    if (randomizer == 1)
                    {
                        walkPattern = 0;
                    }
        }
        else
        {
            baseWalkDelay -= 1;
        }
    }

}

public class AnimatronicType1 : AnimatronicObject
{

    int atDoor = 0;
    GameObject doorSprite1;
    GameObject doorSprite2;


    public AnimatronicType1(int startX, int startY, int endingX, int endingY, int difficultyLevel, 
    int displayNumber, int walkPattern, GameObject doorSprite1, GameObject doorSprite2)

    : base(startX, startY, endingX, endingY, difficultyLevel, displayNumber, walkPattern)
    {

        this.doorSprite1 = doorSprite1;
        this.doorSprite2 = doorSprite2;

        AnimatronicGrid.ChangePosition(startX, startY);

        Vector2 cameraInfo = new Vector2(AnimatronicGrid.GetCurrentX(), AnimatronicGrid.GetCurrentY());
        currentCamera = AnimatronicGrid.GetCamera(cameraInfo);

    }

    private void WalkPatternAggressive()
    {
        //When walkDelay hits zero, the animatronic acts based on the following conditions
        if (baseWalkDelay == 0)
        {

            //Given the animatronic is in the special cameras during the event of atDoor. Once
            //walkDelay runs out the next time, it will finally check for the animatronic's
            //jumpscare with the atDoorEvent() function.            
            if (atDoor != 0)
            {
                AtDoorEvent();
            }

            //If the animmatronic is in an event camera, the atDoor event will be active and the
            //animatronic will wait in the special camera until next walkDelay = 0
            if (currentCamera == 0 && atDoor == 0 && CameraGrid.someoneElseAtPosition1 == false)
            {
                CameraGrid.someoneElseAtPosition1 = true;
                atDoor = 1;
                currentCamera = 14;
                baseWalkDelay = 600;
                doorSprite1.SetActive(true);
                WipeAllCameras();
            }

            //If the animmatronic is in an event camera, the atDoor event will be active and the
            //animatronic will wait in the special camera until next walkDelay = 0
            if (currentCamera == 5 && atDoor == 0 && CameraGrid.someoneElseAtPosition2 == false)
            {
                CameraGrid.someoneElseAtPosition2 = true;
                atDoor = 2;
                currentCamera = 15;
                baseWalkDelay = 600;
                doorSprite2.SetActive(true);
                WipeAllCameras();
            }

            //If the animatronic isn't in any of both attack cameras, they will proceed its movement pattern normally
            if (currentCamera != 0 || currentCamera !=5) {
                if (atDoor == 0)
                {
                    AnimatronicGrid.AnimatronicMovement1();
                    RefreshCameras();
                    baseWalkDelay = 500;
                    int randomizer = AnimatronicQOL.random.Next(3);
                    if (randomizer == 1)
                    {
                        walkPattern = 1;
                    }
                }
            }
        }
        //If the walkDelay hasn't hit zero, it will decrease the value until so
        else
        {
            baseWalkDelay -= 1;
        }
    }

    private void AtDoorEvent()
    {

        //First we check at which door the animatronic is at
        switch (atDoor)
        {
            case 1:

                //We run the proper conditionals related to the player's response. If all met, the animatronic resets position and exits atDoor.
                if (AnimatronicQOL.atDooreEvent1(playerScript, "Mask1", "Position1"))
                    {
                        AnimatronicGrid.SetCurrentX(startX);
                        AnimatronicGrid.SetCurrentY(startY);
                        atDoor = 0;
                        RefreshCameras();
                        doorSprite1.SetActive(false);
                        CameraGrid.someoneElseAtPosition1 = false;
                    }

                    //If the conditions aren't met, jumpscareEvent() is triggered.
                    else
                    {
                    jumpscareEvent();
                    }
                break;

            case 2:
                if (AnimatronicQOL.atDooreEvent1(playerScript, "Mask1", "Position2"))
                    {
                        AnimatronicGrid.SetCurrentX(startX);
                        AnimatronicGrid.SetCurrentY(startY);
                        atDoor = 0;
                        RefreshCameras();
                        doorSprite2.SetActive(false);
                        CameraGrid.someoneElseAtPosition2 = false;
                    }
                    else
                    {
                    jumpscareEvent();
                    }
   
                break;
        }
        
    }

    public void AnimatronicUpdate()
    {
        switch (walkPattern)
        {
            case 0:
                WalkPatternAggressive();
            break;

            case 1:
                WalkPatternRandom();
            break;
        }
    }






    private void jumpscareEvent()
    {
        //print("YOU ARE DEEEEEEAD");
    }




}


public class AnimatronicType2 : AnimatronicObject
{

    //SolNLarkManagerS theOtherScript;



    string theOtherScriptName;

    public AnimatronicType2(int startX, int startY, int endingX, int endingY, int difficultyLevel, 
    int displayNumber, int walkPattern, string theOtherScriptName)

    : base(startX, startY, endingX, endingY, difficultyLevel, displayNumber, walkPattern)
    {
        this.theOtherScriptName = theOtherScriptName;

        //theOtherScript = GameObject.FindGameObjectWithTag("TheOtherScriptName").GetComponent<LarktronicManager>();
    }

    private void WalkPatternPassive()
    {
        if (baseWalkDelay == 0)
        {
            AnimatronicGrid.AnimatronicMovement1();
            baseWalkDelay = 500;
        }
        else
        {
            baseWalkDelay -= 1;
        }
    }

    public void AnimatronicUpdate()
    {
        switch (walkPattern)
        {
            case 0:
                WalkPatternPassive();
            break;

            case 1:
                WalkPatternRandom();
            break;
        }
    }
}