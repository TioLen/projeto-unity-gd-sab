using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    // move settings
    //velocidade, pulo, gravidade
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    
    //references
    public CharacterController controller;
    public Animator anim;
    public Vector3 moveDirection = Vector3.zero;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
        
    }

    
    void Update()
    {
        //chamar as funcoes aqui    
        ApplyGravity();
        controller.Move(moveDirection * Time.deltaTime);
    }

    //criar as funcoes aqui fora
    private void ApplyGravity(){
        if (controller.isGrounded){
            moveDirection.y = -0.5f;
            if (Input.GetButtonDown("Jump")){
                moveDirection.y = jumpSpeed;
            }
        }
        else{
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }
}
