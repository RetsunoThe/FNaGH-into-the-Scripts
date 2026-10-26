// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.Timeline;

// public class RaycastActionS : MonoBehaviour
// {
//     PlayerManagerS playerScript;

//     [SerializeField] int ButtonType; 
//     [SerializeField] int Action;


//     public void DetermineAction()
//     {
//         playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManagerS>();

//         if (ButtonType == 0 && Action == 1) LeftClickDoor();
//         if (ButtonType == 0 && Action == 0) LeftClickCorridor();
//         if (ButtonType == 0 && Action == 2) LeftClickStorage();
//         if (ButtonType == 0 && Action == 3) LeftClickComputer(); 
//     }


//     private void LeftClickDoor()
//     {



//         if (Mouse.current.rightButton.wasPressedThisFrame) Deactivate();


//         void Activate()
//         {
            
//         }

//         void Deactivate()
//         {
            
//         }
//     }


//     private void LeftClickDoor()
//         {
//             void Activate()
//             {
                
//             }

//             void Deactivate()
//             {
                
//             }
//         }



// }