using System;
using UnityEngine;

public class JoshtronicManagerS : MonoBehaviour
{
    System.Random random = new System.Random();

    PlayerManagerS playerScript;

    int walkDelay = 500;

    [SerializeField] private int currentCamera = 0;

    CameraGrid JoshGrid;

    //Coordinates manager for animatronics

    [SerializeField] private int startX = 0;
    [SerializeField] private int startY = 3;

    [SerializeField] private int endingX = 4;
    [SerializeField] private int endingY = 0;

    //At door event check

    int atDoor = 0;
    int walkPattern = 0;

    [SerializeField] private GameObject doorSprite1;
    [SerializeField] private GameObject doorSprite2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

        JoshGrid = new CameraGrid(startX, startY, endingX, endingY);
        JoshGrid.ChangePosition(startX, startY);


        print("Destination: " + endingX + " " + endingY);

        Vector2 cameraInfo = new Vector2(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY());
        currentCamera = JoshGrid.GetCamera(cameraInfo);
    }


    // Update is called once per frame
    private void Update()
    {
        switch (walkPattern)
        {
            case 0:
                WalkPattern0();
            break;

            case 1:
                WalkPattern1();
            break;
        }





        
    }


    private void WalkPattern0()
    {
        //When walkDelay hits zero, the animatronic acts based on the following conditions
        if (walkDelay == 0)
        {

            //Given the animatronic is in the special cameras during the event of atDoor. Once
            //walkDelay runs out the next time, it will finally check for the animatronic's
            //jumpscare with the atDoorEvent() function.            
            if (atDoor != 0)
            {
                atDoorEvent();
            }

            //If the animmatronic is in an event camera, the atDoor event will be active and the
            //animatronic will wait in the special camera until next walkDelay = 0
            if (currentCamera == 0 && atDoor == 0 && CameraGrid.someoneElseAtPosition1 == false)
            {
                CameraGrid.someoneElseAtPosition1 = true;
                atDoor = 1;
                currentCamera = 14;
                walkDelay = 600;
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
                walkDelay = 600;
                doorSprite2.SetActive(true);
                WipeAllCameras();
            }

            //If the animatronic isn't in any of both attack cameras, they will proceed its movement pattern normally
            if (currentCamera != 0 || currentCamera !=5) {
                if (atDoor == 0)
                {
                    JoshGrid.AnimatronicMovement1();
                    walkCheck();
                    walkDelay = 500;
                    int randomizer = random.Next(3);
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
            walkDelay -= 1;
        }
    }

    private void WalkPattern1()
    {
        //When walkDelay hits zero, the animatronic acts based on the following conditions
        if (walkDelay == 0)
        {
            JoshGrid.AnimatronicMovement2();
            walkCheck();
            walkDelay = 500;
            int randomizer = random.Next(3);
                    if (randomizer == 1)
                    {
                        walkPattern = 0;
                    }
            
        }
        else
        {
            walkDelay -= 1;
        }
    }

    private void atDoorEvent()
    {

        //First we check at which door the animatronic is at
        switch (atDoor)
        {
            case 1:

                //We run the proper conditionals related to the player's response. If all met, the animatronic resets position and exits atDoor.
                if (AnimatronicQOL.atDooreEvent1(playerScript, "Mask1", "Position1"))
                    {
                        JoshGrid.SetCurrentX(startX);
                        JoshGrid.SetCurrentY(startY);
                        atDoor = 0;
                        walkCheck();
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
                        JoshGrid.SetCurrentX(startX);
                        JoshGrid.SetCurrentY(startY);
                        atDoor = 0;
                        walkCheck();
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

    private void walkCheck()
    {

        //Here we first get the current newly changed animatronic position and store it in cameraInfo to then convert the native 
        //currentCamera variable into its respective number.
        Vector2 cameraInfo = new Vector2(JoshGrid.GetCurrentX(), JoshGrid.GetCurrentY());
        currentCamera = JoshGrid.GetCamera(cameraInfo);

        //Then we run the renderings for the animatronic to be updated in the cameras.
        WipeAllCameras();
        CameraVisualizer();
    }


    


    private void jumpscareEvent()
    {
        print("YOU ARE DEEEEEEAD");
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
           CameraVisualizer[i].GetComponent<CameraViewManager>().CameraAnimatronics[3].SetActive(false);
        }
    }




}
