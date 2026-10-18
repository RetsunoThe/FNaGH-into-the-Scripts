using System;
using UnityEngine;

public abstract class AnimatronicObject : MonoBehaviour
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

    protected string name;
    


    public AnimatronicObject(int startX, int startY, int endingX, int endingY, int difficultyLevel, int displayNumber, int walkPattern, string name)
    {
        this.startX = startX;
        this.startY = startY;

        this.endingX = endingX;
        this.endingY = endingY;

        this.name = name;

        AnimatronicGrid = new CameraGrid(startX, startY, endingX, endingY);

        this.difficultyLevel = difficultyLevel;
        baseWalkDelay = 500;

        this.displayNumber = displayNumber;

        this.walkPattern = walkPattern;

        
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

        RefreshCameras();
        
    }

    protected bool DifficultyCheck()
    {
        if (AnimatronicQOL.random.Next(20) <= difficultyLevel)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    protected int WalkDelayRandomizer()
    {
        int randomizer = AnimatronicQOL.random.Next(300, 501);
        return randomizer;
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
        }
        else
        {
            baseWalkDelay -= 1;
        }
    }



    protected void jumpscareEvent()
    {
        Debug.Log("YOU'RE DEAAAAAAAADD " + name);
    }

}

public class AnimatronicType1 : AnimatronicObject
{

    int atDoor = 0;
    GameObject doorSprite1;
    GameObject doorSprite2;


    public AnimatronicType1(int startX, int startY, int endingX, int endingY, int difficultyLevel, 
    int displayNumber, int walkPattern, string name, GameObject doorSprite1, GameObject doorSprite2)

    : base(startX, startY, endingX, endingY, difficultyLevel, displayNumber, walkPattern, name)
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
        switch (atDoor)
        {
            case 1:

                if (AnimatronicQOL.atDooreEvent1(playerScript, "Mask1", "Position1"))
                    {
                        AnimatronicGrid.SetCurrentX(startX);
                        AnimatronicGrid.SetCurrentY(startY);
                        atDoor = 0;
                        RefreshCameras();
                        doorSprite1.SetActive(false);
                        CameraGrid.someoneElseAtPosition1 = false;
                    }
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
        int randomizer;

        switch (walkPattern)
        {
            case 0:

                if(FlagEvent())
                {
                    WalkPatternAggressive();
                    randomizer = AnimatronicQOL.random.Next(3);
                    if (randomizer == 1)
                    {
                        walkPattern = 1;
                    }
                }
            break;

            case 1:
                WalkPatternRandom();
                randomizer = AnimatronicQOL.random.Next(3);
                    if (randomizer == 1)
                    {
                        walkPattern = 0;
                    }
            break;
        }
    }

    private bool FlagEvent()
    {
        if (atDoor == 1)
        {
            AtDoorEvent();
            return false;
        }
        if (currentCamera == 0 && atDoor == 0)
        {
            atDoor = 1;
            baseWalkDelay = 500;
            WipeAllCameras();
            doorSprite1.SetActive(true);
            return false;
            
        }
        if (currentCamera == 5 && atDoor == 0)
        {
            atDoor = 2;
            baseWalkDelay = 500;
            WipeAllCameras();
            doorSprite2.SetActive(true);
            return false;
        }
        else
        {
            atDoor = 0;
            return true;
        }
    }
}


public class AnimatronicType2 : AnimatronicObject
{
    AnimatronicType2 theOtherScript;

    bool atOtherScript;

    int agent = 0;

    string theOtherScriptName;

    public AnimatronicType2(int startX, int startY, int endingX, int endingY, int difficultyLevel, 
    int displayNumber, int walkPattern, string name)

    : base(startX, startY, endingX, endingY, difficultyLevel, displayNumber, walkPattern, name)
    {
        this.name = name;
    }

    public int GetAgent()
    {
        return agent;
    }

    private void WalkPatternPassive()
    {
        if (baseWalkDelay == 0)
        {
            AnimatronicGrid.AnimatronicMovement1();
            RefreshCameras();
            baseWalkDelay = 500;
        }
        else
        {
            baseWalkDelay -= 1;
        }
    }

    public void SetupScripts(AnimatronicType2 receiveScript)
    {
        theOtherScript = receiveScript;
        Debug.Log(theOtherScript);

        if (theOtherScript.GetAgent() == 1)
            {
                agent = 2;
            }
            else
            {
                agent = 1;
            }

        PositionRandomizer();

        Debug.Log(GetAgent());
            
        
    }

    public void AnimatronicUpdate()
    {
            int randomizer;


            if (agent == 1)
            {
                // Debug.Log("This is agent 1 " + name);
                if (FlagEvent())
                {
                    // Debug.Log("Agent 1 passed the flag");
                    switch (walkPattern)
                    {
                        case 0:
                        
                            WalkPatternPassive();
                            randomizer = AnimatronicQOL.random.Next(3);
                            if (randomizer == 1)
                            {
                                walkPattern = 1;
                            }
                        break;

                        case 1:
                            WalkPatternRandom();
                            randomizer = AnimatronicQOL.random.Next(3);
                            if (randomizer == 1)
                            {
                                walkPattern = 0;
                            }
                        break;
                    }
                }
            }
            else
            {
                // Debug.Log("This is agent 2 " + name);
                WalkPatternPassive();
                walkPattern = 1;
            }
    }



    private bool FlagEvent()
    {
        if (atOtherScript == true)
        {
            FoundOther();
            return false;
        }
        if (currentCamera == theOtherScript.currentCamera && atOtherScript == false)
        {
            atOtherScript = true;
            baseWalkDelay = 500;
            return false;
            
        }
        else
        {
            atOtherScript = false;
            return true;
        }
    }


    private void FoundOther()
    {
        if (playerScript.inCamera == true && playerScript.isFlashlightOn == true && playerScript.cameraNumber == currentCamera)
        {
            PositionRandomizer();
        }
        else
        {
            jumpscareEvent();
        }

    }
    private void PositionRandomizer()
    {

        int randomizer = AnimatronicQOL.random.Next(7);

        int agent1X = 1;
        int agent1Y = 1;

        int agent2X = 1;
        int agent2Y = 1;

        switch(randomizer)
        {
            case 1:
            agent1X = 0;
            agent1Y = 3;

            agent2X = 4;
            agent2Y = 2;
            break;

            case 2:
            agent1X = 4;
            agent1Y = 0;

            agent2X = 3;
            agent2Y = 4;
            break;

            case 3:
            agent1X = 4;
            agent1Y = 0;

            agent2X = 0;
            agent2Y = 3;
            break;

            case 4:
            agent1X = 3;
            agent1Y = 4;
            
            agent2X = 4;
            agent2Y = 0;
            break;

            case 5:
            agent1X = 2;
            agent1Y = 3;
            
            agent2X = 0;
            agent2Y = 3;
            break;

            case 6:
            agent1X = 3;
            agent1Y = 4;
            
            agent2X = 2;
            agent2Y = 0;
            break;

            case 7:
            agent1X = 2;
            agent1Y = 0;
            
            agent2X = 3;
            agent2Y = 4;
            break;
        }

        if (GetAgent() == 1)
        {
            AnimatronicGrid.SetCurrentX(agent1X);
            AnimatronicGrid.SetCurrentY(agent1Y);
            theOtherScript.AnimatronicGrid.SetCurrentX(agent2X);
            theOtherScript.AnimatronicGrid.SetCurrentY(agent2Y);
        }
        else
        {
            AnimatronicGrid.SetCurrentX(agent2X);
            AnimatronicGrid.SetCurrentY(agent2Y);
            theOtherScript.AnimatronicGrid.SetCurrentX(agent1X);
            theOtherScript.AnimatronicGrid.SetCurrentY(agent1Y);
        }

        RefreshCameras();

    }
}