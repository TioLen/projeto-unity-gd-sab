// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO.Ports;
// using UnityEditor.Rendering;

// // make sure to name your C# file the same as your class name. In this case Move.cs 
// public class ArduinoController : MonoBehaviour
// {
//     SerialPort sp = new SerialPort("COM8", 9600);
//     public float speed = 10f;
//     public bool isButtonPressed;
//     PlayerMovement pm;
//     // Start is called before the first frame update
//     void Start()
//     {
//         sp.Open();
//         sp.ReadTimeout = 100; // In my case, 100 was a good amount to allow quite smooth transition.
//         pm = FindObjectOfType<PlayerMovement>();
//         isButtonPressed = false;
//     }

//     // Update is called once per frame
//     void LateUpdate()
//     {
//         if (sp.IsOpen){
//             try{
//                 // When left button is pushed
//                 // if(sp.ReadByte()==1){
//                 //     print(sp.ReadByte());
//                 //     transform.Translate(Vector3.left * Time.deltaTime * speed);
//                 // }
//                 // // When right button is pushed
//                 // else if(sp.ReadByte()==2){
//                 //     print(sp.ReadByte());
//                 //     transform.Translate(Vector3.right * Time.deltaTime * speed);
//                 // }

//                 switch (sp.ReadByte())
//                 {
                    
//                     case 1:
//                         isButtonPressed = true;
//                         transform.Translate(Vector3.left * Time.deltaTime * speed);
//                         break;
//                     case 2:
//                         isButtonPressed = true;
//                         transform.Translate(Vector3.right * Time.deltaTime * speed);
//                         break;
//                     case 3:
//                         isButtonPressed = true;
//                         // pm.Jump();
//                         break;
//                     case 4:
//                         isButtonPressed = true;
//                         transform.Translate(Vector3.right * Time.deltaTime * speed);
//                         break;
                    

//                     case 5:
//                         transform.Translate(Vector3.right * Time.deltaTime * speed);
//                         break;
//                     case 6:
//                         transform.Translate(Vector3.right * Time.deltaTime * speed);
//                         break;
//                     default:
//                         Debug.ClearDeveloperConsole();
//                         break;
//                 }
                

//             }
//             catch (System.Exception){
//                 isButtonPressed = false;
//                 print("waiting for button");
//             }
//         print("isbuttonpressed: "+ isButtonPressed);
//         }
//     }
// }
