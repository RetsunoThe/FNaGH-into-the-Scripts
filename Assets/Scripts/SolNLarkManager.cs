using UnityEngine;

public class SolNLarkManager : MonoBehaviour
{

    AnimatronicType2 Soltronic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Soltronic = new AnimatronicType2(0, 3, 4, 0, 10, 3, 0);
        

    }

    // Update is called once per frame
    private void Update()
    {



    }


    private void FoundLarkEvent()
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


    private void walkPattern0()
    {
        if (walkDelay == 0)
        {
            



            if (atLark == 1)
            {
                FoundLarkEvent();
            }
            
            if (currentCamera == larkScript.currentCamera && atLark == 0)
            {
                atLark = 1;
                walkDelay = 500;
            }
            if (currentCamera != larkScript.currentCamera)
            {
                SolGrid.SetDestionation(larkScript.LarkGrid.GetCurrentX(), larkScript.LarkGrid.GetCurrentY());
                SolGrid.AnimatronicMovement1();
                walkCheck();
                walkDelay = 500;
                int randomizer = AnimatronicQOL.random.Next(3);
                    if (randomizer == 1)
                    {
                        walkPattern = 1;
                    }
            }
        }
        else
        {
            walkDelay -= 1;
        }
    }
    private void walkPattern1()
    {
        if (walkDelay == 0)
        {
            SolGrid.AnimatronicMovement2();
            walkDelay = 500;
            int randomizer = AnimatronicQOL.random.Next(3);
            if (randomizer == 1)
            {
                walkPattern = 0;
            }
        }
    }


    private void PositionRandomizer()
    {
        int min = AnimatronicQOL.random.Next(7);

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

            case 4:
            solPosX = 3;
            solPosY = 4;
            
            larkPosX = 4;
            larkPosY = 0;
            break;

            case 5:
            solPosX = 2;
            solPosY = 3;
            
            larkPosX = 0;
            larkPosY = 3;
            break;

            case 6:
            solPosX = 3;
            solPosY = 4;
            
            larkPosX = 2;
            larkPosY = 0;
            break;

            case 7:
            solPosX = 2;
            solPosY = 0;
            
            larkPosX = 3;
            larkPosY = 4;
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
        print("YOU ARE DEEEEEEAD (sol)");
    }



}

