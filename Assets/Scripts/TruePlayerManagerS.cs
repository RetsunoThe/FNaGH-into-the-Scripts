using UnityEngine;

public class TruePlayerManagerS : MonoBehaviour
{

    [SerializeField] GameObject PlayerCamera;
    LayerMask RaycastMask;

public GameObject hitObject;
    public GameObject hitObject0;
    public GameObject hitObject1;


    public GameObject savedHitObject;

    public bool inPosition = false;
    public bool inCamera = false;



    public int cameraNumber = 0;
    public int lastCameraNumber = 0;
    public GameObject heldObject;



    public bool isFlashlightOn = false;




    public GameObject[] CameraVisualizers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RaycastMask = LayerMask.GetMask("raycastObject");
    }

    // Update is called once per frame
    void Update()
    {



        CharacterController characterController;


        Raycast();
    }


    void Raycast()
    {
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, 7, RaycastMask))
        {
            
            hitObject = hit.collider.gameObject;
            
            if(hitObject.tag == "goPosition")
            {
                inPosition = true;
            }

        }

    }







    private void CameraVisualizer()
    {
        for (int i = 0; i < CameraVisualizers.Length; i ++)
        {
            CameraVisualizers[i].SetActive(false);
        }
        CameraVisualizers[cameraNumber].SetActive(true);
    }




    public void cameraClickEvent(int camera)
    {
        for (int i = 0; i < CameraVisualizers.Length; i ++)
        {
            CameraVisualizers[i].SetActive(false);
        }
        cameraNumber = camera;
    }
}
