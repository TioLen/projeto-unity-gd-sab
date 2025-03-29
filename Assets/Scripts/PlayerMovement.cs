using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;

    // RigidBody: componente de física
    public Rigidbody rb;

    // Transform: componente de transformações (posição, rotação e escala)
    public Transform cameraTransform;

    private bool isOnGround; // verdadeiro ou falso

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
        Vector3 frente = cameraTransform.forward; 
        Vector3 lado = cameraTransform.right;
        frente.y = 1; // Garantir que o jogador rotacione com a câmera para baixo ou para cima
        lado.y = 0; // Evita movimento indesejado no eixo Y
        frente.Normalize(); // Molda o valor para o mesmo tamanho em todos os eixos
        lado.Normalize();   // Molda o valor para o mesmo tamanho em todos os eixos

        // Valor de movimentação: soma frente e lado multiplicados pelos inputs e speed
        Vector3 movimento = (frente * inputVertical + lado * inputHorizontal) * speed;

        // Aplicar a física
        rb.velocity = new Vector3(movimento.x, rb.velocity.y, movimento.z);

        // Verifica se estou iniciando o movimento
        if (movimento.magnitude > 0.1F)
        {
            // Transform.forward é o valor da frente
            transform.forward = 
                Vector3.Slerp(              // Slerp: Método que modifica um valor para outro em x tempo
                    transform.forward,      // Valor modificado: frente do modelo
                    movimento.normalized,   // Valor alvo: direção do movimento
                    Time.deltaTime * 10f);  // Time.deltaTime: tempo em segundos
        }

        // jump
        // GetKeyDown: pega quando a tecla é apertada
        // KeyCode: é o código da tecla
        // && -> E, operador lógico -> verifica se ambas afirmações são true
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isOnGround = false;
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