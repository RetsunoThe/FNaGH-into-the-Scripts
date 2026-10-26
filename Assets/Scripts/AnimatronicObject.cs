using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public abstract class AnimatronicObject: MonoBehaviour
{

    protected CameraGrid AnimatronicGrid;
    protected static PlayerManagerS playerScript;

    protected int currentCamera;

    protected int currentX;
    protected int currentY;

    protected int endingX;
    protected int endingY;

    protected int walkDelay;
    protected int baseWalkDelay;

    protected int difficultyLevel;

    protected int displayNumber;

    protected int walkPattern;

    protected string name;

    protected AnimatronicObject(int currentX, int currentY, int endingX, int endingY, int difficultyLevel, int walkDelay, int displayNumber, int walkPattern, string name)
    {
        this.currentX = currentX;
        this.currentY = currentY;
        this.endingX = endingX;
        this.endingY = endingY;

        AnimatronicGrid = new CameraGrid(currentX, currentY, endingX, endingY);
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

        this.difficultyLevel = difficultyLevel;

        this.walkDelay = walkDelay;
        this.baseWalkDelay = walkDelay;


        this.displayNumber = displayNumber;
        this.walkPattern = walkPattern;

        this.name = name;
    }

    protected AnimatronicObject(int difficultyLevel, int walkDelay, int displayNumber, int walkPattern, string name)
    {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

        this.difficultyLevel = difficultyLevel;

        this.walkDelay = walkDelay;
        this.baseWalkDelay = walkDelay;


        this.displayNumber = displayNumber;
        this.walkPattern = walkPattern;

        this.name = name;
    }

    protected bool WalkDelayCounter()
    {
        //Debug.Log(walkDelay + " name");
        if (walkDelay == 0)
        {
            
            RandomizeWalkDelay(1);
            return true;
        }
        else
        {
            walkDelay -= 1;
            return false;
        }
    }


    protected void RefreshCameras()
    {
        Vector2 cameraInfo = new Vector2(AnimatronicGrid.GetCurrentX(), AnimatronicGrid.GetCurrentY());
        currentCamera = AnimatronicGrid.GetCamera(cameraInfo);

        WipeAllCameras();
        CameraVisualizer();
    }
    protected void CameraVisualizer()
    {
        if (currentCamera >= 0 && currentCamera <= 13)
        {
            GameObject[] cameraVisualizer = playerScript.CameraVisualizers[currentCamera].GetComponent<CameraViewManager>().CameraAnimatronics;
            cameraVisualizer[displayNumber].SetActive(true);
        }
    } 
    protected void CameraVisualizer(int currentCameraImport)
    {
        if (currentCameraImport >= 0 && currentCameraImport <= 13)
        {
            GameObject[] cameraVisualizer = playerScript.CameraVisualizers[currentCameraImport].GetComponent<CameraViewManager>().CameraAnimatronics;
            cameraVisualizer[displayNumber].SetActive(true);
        }
    } 

    protected void WipeAllCameras()
    {
        GameObject[] CameraVisualizer = playerScript.CameraVisualizers;
        for (int i = 0; i < CameraVisualizer.Length; i ++)
        {
           CameraVisualizer[i].GetComponent<CameraViewManager>().CameraAnimatronics[displayNumber].SetActive(false);
        }
    }

    protected void RandomizeWalkDelay(int mult3)
    {
        int mult1 = AnimatronicQOL.random.Next(1, 4);
        int mult2 = AnimatronicQOL.random.Next(-1, 2);
        walkDelay += baseWalkDelay + ((baseWalkDelay / 100) * (mult1 * 10) * mult2) * mult3;
    }
    protected bool DifficultyCheck()


    {
        int randomizer = AnimatronicQOL.random.Next(21);
        //Debug.Log("Number rolled: " + randomizer);
        if (randomizer <= difficultyLevel)
        {
            Debug.Log("Proceed");
            return true;
        }
        //Debug.Log("Failed");
        return false;
    }

    protected void WalkRandom()
    {
        AnimatronicGrid.AnimatronicMovement2();
        RefreshCameras();
    }

    protected void JumpscareEvent()
    {
        Debug.Log("YOU'RE DEEEEEAD " + name);
    }

}

public class AnimatronicType1 : AnimatronicObject
{

    GameObject doorSprite1;
    GameObject doorSprite2;

    int flagDoor = 0;

    static bool holdingDoor1 = false;
    static bool holdingDoor2 = false; 

    string maskName;

    public AnimatronicType1(int currentX, int currentY, int endingX, int endingY, int difficultyLevel, int walkDelay, int displayNumber, int walkPattern, string name,
    GameObject doorSprite1, GameObject doorSprite2, string maskName)
    : base(currentX, currentY, endingX, endingY, difficultyLevel, walkDelay, displayNumber, walkPattern, name)
    {

        this.doorSprite1 = doorSprite1;
        this.doorSprite2 = doorSprite2;

        this.maskName = maskName;

        AnimatronicGrid.ChangePosition(currentX, currentY);

        Vector2 cameraInfo = new Vector2(AnimatronicGrid.GetCurrentX(), AnimatronicGrid.GetCurrentY());
        currentCamera = AnimatronicGrid.GetCamera(cameraInfo);
        CameraVisualizer();

    }

    public void RandomizeRespawn()
    {
        int spawnX = 0;
        int spawnY = 3;

        int destinationX = 4;
        int destinationY = 0;

        switch (AnimatronicQOL.random.Next(7))
        {
            case 1:     spawnX = 0;     spawnY = 3;     break;

            case 2:     spawnX = 2;     spawnY = 0;     break;

            case 3:     spawnX = 3;     spawnY = 4;     break;

            case 4:     spawnX = 2;     spawnY = 3;     break;

            case 5:     spawnX = 0;     spawnY = 2;     break;
            
            case 6:     spawnX = 1;     spawnY = 2;     break;
        }

        if (AnimatronicQOL.random.Next(3) == 1)
        {
            destinationX = 4;
            destinationY = 0;
        }
        else
        {
            destinationX = 4;
            destinationY = 2;
        }

        AnimatronicGrid.ChangePosition(spawnX, spawnY);
        AnimatronicGrid.SetDestionation(destinationX, destinationY);
    }
    private void FlagEvent()
    {
        void FlagEventSwitch(string position, GameObject doorSprite, bool holdingDoor)
        {
            void FlagEventSwitchUpdate()
            {
                flagDoor = 0;
                doorSprite.SetActive(false);
                RandomizeRespawn();
                RefreshCameras();
                RandomizeWalkDelay(2);
                holdingDoor = false;
            }
        if(maskName == null)
        {
            if(playerScript.isFlashlightOn == true && playerScript.heldObject == null && playerScript.hitObjectPosition?.name == position)
            {
                FlagEventSwitchUpdate();
            }
            if(playerScript.isFlashlightOn == true && playerScript.heldObject != null && playerScript.hitObjectPosition?.name == position)
            {
                JumpscareEvent();
            }
        }   
        else if (maskName != null)
        {
            if(playerScript.isFlashlightOn == true && playerScript.heldObject?.name == maskName && playerScript.hitObjectPosition?.name == position)
            {
                FlagEventSwitchUpdate();
            }
            if(playerScript.isFlashlightOn == true && playerScript.heldObject?.name != maskName && playerScript.hitObjectPosition?.name == position)
            {
                JumpscareEvent();
            }
        }
    }
        switch (flagDoor)
        {
            case 1:

            FlagEventSwitch("Position1", doorSprite1, holdingDoor1);
            break;
            
            case 2:
            FlagEventSwitch("Position2", doorSprite2, holdingDoor2);
            break;
        }
    }

    private void WalkAggressive()
    {
        if (holdingDoor1 ==  true || holdingDoor2 == true && currentCamera == 0 || currentCamera == 5 || currentCamera == 6 || currentCamera == 1)
        {
            RandomizeWalkDelay(2);
        }

        if (flagDoor != 0)
        {
            JumpscareEvent();
        }

        if (currentCamera == 0 && flagDoor == 0 && holdingDoor1 == false)
        {
            flagDoor =  1;
            doorSprite1.SetActive(true);
            currentCamera = 15;
            WipeAllCameras();
            holdingDoor1 = true;
            RandomizeWalkDelay(1);
        }
        else if (currentCamera == 5 && flagDoor == 0 && holdingDoor2 == false)
        {
            flagDoor = 2;
            doorSprite2.SetActive(true);
            currentCamera = 15;
            WipeAllCameras();
            holdingDoor2 = true;
            RandomizeWalkDelay(1);
        }
        else
        {
            AnimatronicGrid.AnimatronicMovement1();
            RefreshCameras();
             if (AnimatronicQOL.random.Next(4) == 1)
             {
                 walkPattern = 2;
            }
        }
    }

    public void AnimatronicUpdate()
    {
        if (flagDoor != 0)
        {
            FlagEvent();
        }

        if (WalkDelayCounter())
        {
            if (DifficultyCheck())
            {
                switch (walkPattern)
                {
                    case 1:
                    WalkAggressive();
                    break;
                    
                    case 2:
                    WalkRandom();
                    if (AnimatronicQOL.random.Next(3) == 1)
                        {
                            walkPattern = 1;
                        }
                    break;
                }
            }

                
        }
        
    }
}

public class AnimatronicType2 : AnimatronicObject
{
    AnimatronicType2 otherScript;
    int agent = 0;

    bool atOtherScript = false;
    

    public AnimatronicType2(int currentX, int currentY, int endingX, int endingY, int difficultyLevel, int walkDelay, int displayNumber, int walkPattern, string name)
    : base(currentX, currentY, endingX, endingY, difficultyLevel, walkDelay, displayNumber, walkPattern, name)
    {

        Vector2 cameraInfo = new Vector2(AnimatronicGrid.GetCurrentX(), AnimatronicGrid.GetCurrentY());
        currentCamera = AnimatronicGrid.GetCamera(cameraInfo);
        CameraVisualizer();

    }

    public void SetupScripts(AnimatronicType2 otherScript)
    {
        this.otherScript = otherScript;
        if (otherScript.agent == 1)
        {
            agent = 2;
        }
        else
        {
            agent = 1;
        }

        //print(agent + " " + name + " " + otherScript.agent + " " + otherScript.name);
    }

    private void WalkPassive()
    {
        if (atOtherScript == true)
        {
            JumpscareEvent();
        }

        if (atOtherScript == false)
        {
            AnimatronicGrid.SetDestionation(otherScript.AnimatronicGrid.GetCurrentX(), otherScript.AnimatronicGrid.GetCurrentY());
            AnimatronicGrid.AnimatronicMovement1();
            RefreshCameras();

            if (currentCamera == otherScript.currentCamera)
            {
                atOtherScript = true;
                RandomizeWalkDelay(2);
            }
        }

    }
    private void FlagEvent()
    {
        if (playerScript.isFlashlightOn == true && playerScript.cameraNumber == currentCamera)
        {
            RandomizeRespawn();
            RandomizeWalkDelay(2);
            otherScript.RandomizeWalkDelay(2);
            atOtherScript = false;
        }
    }

    public void AnimatronicUpdate()
    {
        if (atOtherScript == true)
        {
            FlagEvent();
        }


        if (WalkDelayCounter())
        {
            if (agent == 1)
            {
                switch (walkPattern)
                {
                    case 1:
                        WalkPassive();
                        if (AnimatronicQOL.random.Next(4) == 1)
                        {
                            walkPattern = 2;
                        }
                    break;

                    case 2:
                    WalkRandom();
                    if (AnimatronicQOL.random.Next(3) == 1)
                        {
                            walkPattern = 1;
                        }
                        break;
                }
            }
            if (agent == 2)
            {
                if(AnimatronicGrid.PathfinderDistance(otherScript.AnimatronicGrid.GetCurrentX(), otherScript.AnimatronicGrid.GetCurrentY()) > 1 & 
                otherScript.atOtherScript == false)
                {
                    WalkRandom();
                    RandomizeWalkDelay(3);
                }
            }
            
        }
    }

    public void RandomizeRespawn()
    {
        int agent1X = 0;
        int agent1Y = 3;

        int agent2X = 3;
        int agent2Y = 4;

        switch (AnimatronicQOL.random.Next(11))
        {
            case 1:     agent1X = 0;        agent1Y = 3;        agent2X = 3;        agent2Y = 4;        break;

            case 2:     agent1X = 4;        agent1Y = 0;        agent2X = 2;        agent2Y = 3;        break;

            case 3:     agent1X = 2;        agent1Y = 0;       agent2X = 3;        agent2Y = 4;         break;

            case 4:     agent1X = 2;        agent1Y = 0;        agent2X = 0;        agent2Y = 3;        break;

            case 5:     agent1X = 2;        agent1Y = 0;        agent2X = 3;        agent2Y = 4;        break;

            case 6:     agent1X = 2;        agent1Y = 3;        agent2X = 2;        agent2Y = 0;        break;

            case 7:     agent1X = 3;        agent1Y = 4;        agent2X = 0;        agent2Y = 3;        break;

            case 8:     agent1X = 2;        agent1Y = 0;        agent2X = 3;        agent2Y = 4;        break;

            case 9:     agent1X = 0;        agent1Y = 3;        agent2X = 4;        agent2Y = 2;        break;

            case 10:    agent1X = 2;        agent1Y = 0;        agent2X = 4;        agent2Y = 2;        break;
        }

        if (agent == 1)
        {
            AnimatronicGrid.ChangePosition(agent1X, agent1Y);
            otherScript.AnimatronicGrid.ChangePosition(agent2X, agent2Y);

            agent = 2;
            otherScript.agent = 1;
        }
        else
        {
            AnimatronicGrid.ChangePosition(agent2X, agent2Y);
            otherScript.AnimatronicGrid.ChangePosition(agent1X, agent1Y);

            agent = 1;
            otherScript.agent = 2;
        }

        RefreshCameras();
        otherScript.RefreshCameras();
    }
}

public class AnimatronicType3 : AnimatronicObject
{
    bool isAtCamera = false;
    int baseCameraDelay = 200;
    int cameraDelay;
    
    public AnimatronicType3(int difficultyLevel, int walkDelay, int displayNumber, int walkPattern, string name)
    : base(difficultyLevel, walkDelay, displayNumber, walkPattern, name)
    {
        cameraDelay = baseCameraDelay;
        currentCamera = -1;

        CameraRandomizer();
    }

    public void AnimatronicUpdate()
    {

        if (isAtCamera)
        {
            FlagEvent();
        }

        if (playerScript.cameraNumber == currentCamera && playerScript.inCamera == true)
        {
            
            isAtCamera = true;
        }
        else
        {
            cameraDelay = baseCameraDelay;

            if (WalkDelayCounter())
            {
                CameraRandomizer();
                RandomizeWalkDelay(1);

            }
        }
    }

    private void FlagEvent()
    {
        if (playerScript.cameraNumber == currentCamera)
        {
        if (cameraDelay > 0)
            {
                cameraDelay -= 1;
            }
            else
            {
            JumpscareEvent();
            }
        }
        else
        {
            isAtCamera = false;
            RandomizeWalkDelay(3);
            WipeAllCameras();
            currentCamera = -1;
        }
        
    }

    private void CameraRandomizer()
    {
        int randomCamera = AnimatronicQOL.random.Next(14);
                if (randomCamera == playerScript.cameraNumber && randomCamera != 0)
                {
                    randomCamera += 1;
                }
                if (randomCamera == playerScript.cameraNumber && randomCamera == 0)
                {
                    randomCamera -= 1;
                }
                currentCamera = randomCamera;
                WipeAllCameras();
                CameraVisualizer(randomCamera);
    }

    
}



public class AnimatronicType4
{
    public AnimatronicType4()
    {
        
    }
}