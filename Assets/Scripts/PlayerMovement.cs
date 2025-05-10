using UnityEditor.Rendering;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float speed = 5f;
    public float speedFast;
    public float jump = 5f;
    
    // RigidBody: componente de física
    public Rigidbody physics;

    // Transform: componente de transformações (posição, rotação e escala)
    public Transform cameraTransform;

    private bool isOnGround;

    public Animator anim;

    void Awake(){
        anim = GetComponentInChildren<Animator>();
        speedFast = speed * 2f;
    }

    void Update()
    {
        // Input: entrada de dados por dispositivos -> teclado, mouse, toque, controle
        // Input retorna um número:
            // -1 -> clique para baixo, para esquerda e para trás
            //  0 -> ausência de clique
            //  1 -> clique para cima, para direita e frente
            
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        // Frente e lado da câmera
        // Vector3: eixo x, y e z
        // Forward = frente, Right = direita
        Vector3 front = cameraTransform.forward;
        Vector3 side = cameraTransform.right;
        front.y = 0; // Garantir que o jogador rotacione com a câmera para baixo ou para cima
        side.y = 0; // Evita movimento indesejado no eixo Y
        front.Normalize(); // Molda o valor para o mesmo tamanho em todos os eixos
        side.Normalize();   // Molda o valor para o mesmo tamanho em todos os eixos

        // Valor de movimentação: soma frente e lado multiplicados pelos inputs e velocidade
        Vector3 movement;

        if(!Input.GetKey(KeyCode.LeftShift))
            movement = (front * inputVertical + side * inputHorizontal) * speed;
        else
            movement = (front * inputVertical + side * inputHorizontal) * speedFast;
        
        // Aplicar a física
        physics.velocity = new Vector3(movement.x, physics.velocity.y, movement.z);

        // Verifica se estou iniciando o movimento
        if (movement.magnitude > 0.1F)
        {
            // Transform.forward é o valor da frente
            transform.forward = 
                Vector3.Slerp(              // Slerp: Método que modifica um valor para outro em x tempo
                    transform.forward,      // Valor modificado: frente do modelo
                    movement.normalized,   // Valor alvo: direção do movimento
                    Time.deltaTime * 10f);  // Time.deltaTime: tempo em segundos
        }

        // PULO
        // GetKeyDown: pega quando a tecla é apertada
        // KeyCode: é o código da tecla
        // && -> E, operador lógico -> verifica se ambas afirmações são true
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            anim.SetBool("isJumping", isOnGround);
            physics.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isOnGround = false;
        }

        


        if (isOnGround){

            if(anim.GetBool("isJumping")){
                anim.SetBool("isJumping", false);
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("velX",inputHorizontal*2);
                anim.SetFloat("velZ",inputVertical*2);
            }
            else{
                anim.SetFloat("velX",inputHorizontal);
                anim.SetFloat("velZ",inputVertical);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}