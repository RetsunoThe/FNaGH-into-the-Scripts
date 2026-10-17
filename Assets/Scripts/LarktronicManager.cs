using UnityEngine;

public class LarktronicManager : MonoBehaviour
{

    PlayerManagerS playerScript;

    //Walking Through Cameras

    int walkDelay = 500;

    public int currentCamera = 0;

    public CameraGrid LarkGrid;

    [SerializeField] private int startX = 3;
    [SerializeField] private int startY = 3;

    [SerializeField] private int endingX = 4;
    [SerializeField] private int endingY = 0;

    int atDoor = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();






        LarkGrid = new CameraGrid(startX, startY, endingX, endingY);
        LarkGrid.ChangePosition(startX, startY);

        print("lark first" + currentCamera);
        Vector2 cameraInfo = new Vector2(LarkGrid.GetCurrentX(), LarkGrid.GetCurrentY());
        currentCamera = LarkGrid.GetCamera(cameraInfo);
        
        CameraVisualizer();

    }

    // Update is called once per frame
    private void Update()
    {
        if (walkDelay == 0)
        {
            
           walkCheck();
           walkDelay = 500;
        }
        else
        {
            walkDelay -= 1;
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
            CameraVisualizer[1].SetActive(true);
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
        print("YOU ARE DEEEEEEAD (lark)");
    }







}
