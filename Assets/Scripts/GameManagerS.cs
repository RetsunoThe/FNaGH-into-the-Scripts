using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerS : MonoBehaviour
{

    public float nightTimer = 3000;

    public int nightNumber = 4;

    [SerializeField] private TextMeshProUGUI TimeHud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nightTimer > 0)
        {
            nightTimer -= 1;
        }

        int currentTime = 0;
        float nightTimerMath = nightTimer / 6;


        if(nightTimer > nightTimerMath)
        {
            currentTime = 0;
        }
        if(nightTimer > nightTimerMath * 2)
        {
            currentTime = 1;
        }
        if(nightTimer > nightTimerMath * 3)
        {
            currentTime = 2;
        }
        if(nightTimer > nightTimerMath * 4)
        {
            currentTime = 3;
        }
        if(nightTimer > nightTimerMath * 5)
        {
            currentTime = 4;
        }


        print(currentTime);
        TimeHud.text = currentTime.ToString() + " AM";
        print(nightTimer);
    }
}
