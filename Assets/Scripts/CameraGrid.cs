using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
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





    public void AnimatronicMovement()
    {
        int min = 0;

        List<int> randomList = new List<int>();

        if (UnityEngine.Random.Range(1, 4) == 3)
        {
            if (cameraLayout[currentX, currentY - 1] == 1)
            {
                randomList.Add(1);
            }
            if (cameraLayout[currentX, currentY + 1] == 1)
            {
                randomList.Add(2);
            }
            if (cameraLayout[currentX - 1, currentY] == 1)
            {
                randomList.Add(3);
            }
            if (cameraLayout[currentX + 1, currentY] == 1)
            {
                randomList.Add(4);
            }

            int randomIndex = UnityEngine.Random.Range(1, randomList.Count);

            switch (randomList[randomIndex])
            {
                case 1:
                    ChangePosition(currentX, currentY - 1);
                    ResetDirections();
                    break;
                case 2:
                    ChangePosition(currentX, currentY + 1);
                    ResetDirections();
                    break;
                case 3:
                    ChangePosition(currentX - 1, currentY);
                    ResetDirections();
                    break;
                case 4:
                    ChangePosition(currentX + 1, currentY);
                    ResetDirections();
                    break;
            }
            return;

        }
        
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
