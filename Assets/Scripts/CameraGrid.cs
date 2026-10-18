using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

//If you're seing this code, I'm so sorry.



public class CameraGrid
{

    int leftCameraD = int.MaxValue;
    int rightCameraD = int.MaxValue;
    int upperCameraD = int.MaxValue;
    int lowerCameraD = int.MaxValue;

    int initialX;
    int initialY;
    int finalX;
    int finalY;

    int currentX;
    int currentY;

    public static bool someoneElseAtPosition1 = false;
    public static bool someoneElseAtPosition2 = false;

    private Dictionary<Vector2, int> cameraNumber = new Dictionary<Vector2, int>();

    static int[,] cameraLayout = new int[5, 5]
    {
        {0, 0, 1, 1, 0},
        {0, 1, 1, 0, 0},
        {1, 1, 0, 1, 0},
        {1, 1, 1, 1, 1},
        {1, 0, 1, 0, 0}
    };

    public CameraGrid(int initialX, int initialY, int finalX, int finalY)
    {
        this.initialX = initialX;
        this.initialY = initialY;
        this.finalX = finalX;
        this.finalY = finalY;

        currentX = initialX;
        currentY = initialY;

        CameraDictionary();

    }

    public void SetDestionation(int x, int y)
    {
        finalX = x;
        finalY = y;
    }
    

    public static int[,] GetCameraLayout()
    {
        return cameraLayout;
    }
    public int GetCurrentX()
    {
        return currentX;
    }
    public int GetCurrentY()
    {
        return currentY;
    }
    public void SetCurrentX(int value)
    {
        currentX = value;
    }
    public void SetCurrentY(int value)
    {
        currentY = value;
    }



    public void AnimatronicMovement1()
    {
        int min = 0;

        if (currentY != 0 && cameraLayout[currentX, currentY - 1] == 1)
        {
            leftCameraD = PathfinderDistance(currentX, currentY - 1);
        }
        if (currentY != 4 && cameraLayout[currentX, currentY + 1] == 1)
        {
            rightCameraD = PathfinderDistance(currentX, currentY + 1);
        }
        if (currentX != 0 && cameraLayout[currentX - 1, currentY] == 1)
        {
            upperCameraD = PathfinderDistance(currentX - 1, currentY);
        }
        if (currentX != 4 && cameraLayout[currentX + 1, currentY] == 1)
        {
            lowerCameraD = PathfinderDistance(currentX + 1, currentY);
        }


        //Set of ifs for specific pathfinding scenarios
        if (finalX == 1 && finalY == 1 || finalX == 1 && finalY == 2 || finalX == 0 && finalY == 2 || finalX == 0 && finalY == 3) {
            if(currentX == 3 && currentY == 3)
            {
                ChangePosition(currentX, currentY - 1);
                ResetDirections();
                return;
            }
            if(currentX == 3 && currentY == 2)
            {
                ChangePosition(currentX, currentY - 1);
                ResetDirections();
                return;
            }
        }
        if (finalX == 1 && finalY == 2 || finalX == 0 && finalY == 2 || finalX == 0 && finalY == 3) {
            if(currentX == 3 && currentY == 1)
            {
                ChangePosition(currentX - 1, currentY);
                ResetDirections();
                return;
            } 
        }

        //Set positions
        min = math.min(math.min(leftCameraD, rightCameraD), math.min(upperCameraD, lowerCameraD));
        if (currentX != 4 && lowerCameraD == min)
        {
            ChangePosition(currentX + 1, currentY);
            ResetDirections();
            return;
        }
        if (currentY != 0 && leftCameraD == min)
        {
            ChangePosition(currentX, currentY - 1);
            ResetDirections();
            return;
        }
        if (currentY != 4 && rightCameraD == min)
        {
            ChangePosition(currentX, currentY + 1);
            ResetDirections();
            return;
        }
        if (currentX != 0 && upperCameraD == min)
        {
            ChangePosition(currentX - 1, currentY);
            ResetDirections();
            return;
        }

        

    }


    public void AnimatronicMovement2()
    {
        List<string> availableCases = new List<string>();
        
        if (currentY != 0 && cameraLayout[currentX, currentY - 1] == 1)
        {
            availableCases.Add("left");
        }
        if (currentY != 4 && cameraLayout[currentX, currentY + 1] == 1)
        {
            availableCases.Add("right");
        }
        if (currentX != 0 && cameraLayout[currentX - 1, currentY] == 1)
        {
            availableCases.Add("up");
        }
        if (currentX != 4 && cameraLayout[currentX + 1, currentY] == 1)
        {
            availableCases.Add("down");
        }

        System.Random random = new System.Random();

        int index = random.Next(availableCases.Count);

        if (availableCases[index] == "left")
        {
            ChangePosition(currentX, currentY - 1);
            return;
        }
        if (availableCases[index] == "right")
        {
            ChangePosition(currentX, currentY + 1);
            return;
        }
        if (availableCases[index] == "up")
        {
            ChangePosition(currentX - 1, currentY);
            return;
        }
        if (availableCases[index] == "down")
        {
            ChangePosition(currentX + 1, currentY);
            return;
        }

    }




    private void ResetDirections()
    {
        leftCameraD = int.MaxValue;
        rightCameraD = int.MaxValue;
        upperCameraD = int.MaxValue;
        lowerCameraD = int.MaxValue;
    }


    public int GetCamera(Vector2 camera)
    {
        return cameraNumber[camera];
    }




    private void CameraDictionary()
    {
        Vector2 camera = new Vector2();
        camera = new Vector2(4, 2);
        cameraNumber.Add(camera, 0);
        camera = new Vector2(3, 2);
        cameraNumber.Add(camera, 1);
        camera = new Vector2(3, 3);
        cameraNumber.Add(camera, 2);
        camera = new Vector2(3, 4);
        cameraNumber.Add(camera, 3);
        camera = new Vector2(2, 3);
        cameraNumber.Add(camera, 4);
        camera = new Vector2(4, 0);
        cameraNumber.Add(camera, 5);
        camera = new Vector2(3, 0);
        cameraNumber.Add(camera, 6);
        camera = new Vector2(3, 1);
        cameraNumber.Add(camera, 7);
        camera = new Vector2(2, 0);
        cameraNumber.Add(camera, 8);
        camera = new Vector2(2, 1);
        cameraNumber.Add(camera, 9);
        camera = new Vector2(1, 1);
        cameraNumber.Add(camera, 10);
        camera = new Vector2(1, 2);
        cameraNumber.Add(camera, 11);
        camera = new Vector2(0, 2);
        cameraNumber.Add(camera, 12);
        camera = new Vector2(0, 3);
        cameraNumber.Add(camera, 13);
    }




    public int PathfinderDistance(int x, int y)
        {
            int pathDistanceX = math.abs(x - finalX);
            int pathDistanceY = math.abs(y - finalY);

            int distance = pathDistanceX + pathDistanceY;

            return distance;
        }

    public void ChangePosition(int x, int y)
    {
        currentX = x;
        currentY = y;


    }
}
