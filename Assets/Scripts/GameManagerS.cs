using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerS : MonoBehaviour
{
    public float nightTimer = 300000;
    public float nightTimerMax;

    public int nightNumber = 4;

    [SerializeField] private TextMeshProUGUI TimeHud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nightTimerMax = nightTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > 0 && nightTimer > 0)
        {
            nightTimer -= 1;
            print(nightTimer);
        }



        int currentTime = 0;
        float nightTimerMath = nightTimerMax / 7;


        if(nightTimer == 0)
        {
            currentTime = 6;
        }
        if(nightTimer > nightTimerMath)
        {
            currentTime = 5;
        }
        if(nightTimer > nightTimerMath * 2)
        {
            currentTime = 4;
        }
        if(nightTimer > nightTimerMath * 3)
        {
            currentTime = 3;
        }
        if(nightTimer > nightTimerMath * 4)
        {
            currentTime = 2;
        }
        if(nightTimer > nightTimerMath * 5)
        {
            currentTime = 1;
        }
        if(nightTimer > nightTimerMath * 6)
        {
            currentTime = 0;
        }

        



        TimeHud.text = currentTime.ToString() + " AM";

    }
}
