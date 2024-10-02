using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Скорость пули
    public Vector3 vector3 = new Vector3(40, 1, 10);

    private Vector3 moveDirection = new Vector3(1, 0, 1).normalized;  // Направление движения пули
    private bool isPlayer1Turn;
    private bool needToReset;
    private bool isGrounded = true;
    public delegate void AddedScore();
    public static event AddedScore Player1TakePoint;
    public static event AddedScore Player2TakePoint;

    public delegate void WhoiseBall();
    public static event WhoiseBall BallForPlayer1;
    public static event WhoiseBall BallForPlayer2;
    void Start()
    {
        // Задаем начальное направление пули под углом 45 градусов к осям X и Z
        moveDirection = new Vector3(1, 0, 1).normalized;  // Направление 45 градусов в плоскости XZ        
    }

    void Update()
    {
        // Перемещаем пулю каждый кадр по направлению движения
        transform.position += moveDirection * speed * Time.deltaTime;        
    }/*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;

            transform.position = vector3;
        }
    }*/
    void OnCollisionEnter(Collision collision)
    {
        // Получаем нормаль поверхности, с которой столкнулась пуля
        Vector3 normal = collision.contacts[0].normal;

        // Рассчитываем новое направление движения на основе отражения
        moveDirection = Vector3.Reflect(moveDirection, normal);

        if (collision.gameObject.CompareTag("Player2Gate") && isPlayer1Turn == true)     
        {
            Player1TakePoint?.Invoke();
            transform.position = vector3;
        }
        else if (collision.gameObject.CompareTag("Player1Gate") && isPlayer1Turn == false)
        {
            Player2TakePoint?.Invoke();
            transform.position = vector3;
        }

        if (collision.gameObject.CompareTag("Player1"))
        {
            isPlayer1Turn = true;
            BallForPlayer1?.Invoke();
            Debug.Log("МЯЧ ПРИНАДЛЕЖИТ ИГРОКУ 1");
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            isPlayer1Turn = false;
            BallForPlayer2?.Invoke();  
            Debug.Log("МЯЧ ПРИНАДЛЕЖИТ ИГРОКУ 2");
        }
    }
}




























/*
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Скорость пули
    private Vector3 moveDirection;  // Направление движения пули

    void Start()
    {
        // Задаем начальное направление пули по оси Z
        moveDirection = Vector3.forward;
    }

    void Update()
    {
        // Перемещаем пулю каждый кадр по направлению движения
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Получаем нормаль поверхности, с которой столкнулась пуля
        Vector3 normal = collision.contacts[0].normal;

        // Рассчитываем новое направление движения на основе отражения
        moveDirection = Vector3.Reflect(moveDirection, normal);
    }
}
*/