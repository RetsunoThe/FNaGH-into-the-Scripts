using System;
using UnityEngine;

public class JoshtronicManagerS : MonoBehaviour
{

    [SerializeField] private GameObject doorSprite1;
    [SerializeField] private GameObject doorSprite2;

    AnimatronicType1 Joshtronic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Joshtronic = new AnimatronicType1(0, 3, 4, 0, 10, 3, 0, doorSprite1, doorSprite2);


    }

    // Update is called once per frame
    private void Update()
    {


        Joshtronic.AnimatronicUpdate();


    }



        
}




