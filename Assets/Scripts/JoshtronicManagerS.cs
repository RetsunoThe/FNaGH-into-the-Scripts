using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class JoshtronicManagerS : MonoBehaviour
{

    [SerializeField] private Transform position0;
    [SerializeField] private Transform position1;

    [SerializeField] public GameObject[] positions;

    int walkDelay = 1000;

    int currentCamera = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        positions = GameObject.FindGameObjectsWithTag("JoshPositions");
        transform.position = positions[currentCamera].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        walkCheck();


        
        

    }















    private void walkCheck()
    {
        if (walkDelay == 0)
        {
            walkDelay = 1000;
            transform.position = positions[currentCamera].transform.position;
            if (currentCamera > 0)
            {
                currentCamera -= 1;    
            }
        }
        else
        {
            walkDelay -= 1;
        }
    }


}
