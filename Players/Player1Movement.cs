using System.Collections;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 10f;  // Скорость движения платформы
    public float limitMin = -24f;
    public float limitMax = 0f;
    public float detectionRadius = 10f;  // Радиус обнаружения пули
    private Animator animator;
    private UseAnimation useAnimation;  // Ссылка на компонент UseAnimation
    private Transform bulletTarget;  // Ссылка на пулю

    private void Start()
    {
        animator = GetComponent<Animator>();
        useAnimation = GetComponent<UseAnimation>();
    }

    void Update()
    {
        // Получаем ввод пользователя
        float moveHorizontal = Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");

        // Создаем вектор движения
        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, moveVertical);

        // Если скорость движения равна 0 (игрок стоит на месте)
        if (movement == Vector3.zero)
        {
            // Переходим в состояние Idle
            animator.SetTrigger("Idle");

            // Найдем объект Bullet в радиусе действия
            GameObject bullet = FindBulletNearby();

            if (bullet != null)
            {
                // Поворачиваем игрока в сторону пули
                bulletTarget = bullet.transform;
                RotateTowardsBullet(bulletTarget);
            }
        }
        else
        {
            // Если игрок двигается, переключаем анимацию на Run
            animator.SetTrigger("Run");

            // Поворачиваем персонажа в направлении движения
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Двигаем персонажа
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        // Ограничение по оси Z (от -24 до 0)
        Vector3 currentPosition = transform.position;
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitMin, limitMax);
        transform.position = currentPosition;
    }

    // Метод для поиска пули поблизости
    private GameObject FindBulletNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Bullet"))
            {
                return hitCollider.gameObject;
            }
        }
        return null;
    }

    // Метод для поворота игрока к пуле
    private void RotateTowardsBullet(Transform bulletTransform)
    {
        Vector3 directionToBullet = bulletTransform.position - transform.position;
        directionToBullet.y = 0;  // Игнорируем ось Y, чтобы игрок не вращался по вертикали
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToBullet, speed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // Метод для обработки столкновений
    private IEnumerator TriggerIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletTarget = collision.transform;  // Сохраняем ссылку на объект пули
            RotateTowardsBullet(bulletTarget);  // Поворачиваемся к пуле при столкновении
            useAnimation.AutoAttack();
            StartCoroutine(TriggerIdleAfterDelay(0.5f));  // Задержка в 0.5 секунды
        }
    }
}








/*
using System.Collections;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 10f;  // Скорость движения платформы
    public float limitMin = -24f;
    public float limitMax = 0f;
    private Animator animator;
    private UseAnimation useAnimation;  // Ссылка на компонент UseAnimation
    private void Start()
    {
        animator = GetComponent<Animator>();
        useAnimation = GetComponent<UseAnimation>();
    }
    void Update()
    {
        // Получаем ввод пользователя
        float moveHorizontal = Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");

        // Создаем вектор движения
        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, moveVertical);
        if (moveHorizontal != 0.0f || moveVertical != 0.0f)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        // Если есть движение, повернуть персонажа в его направлении
        if (movement != Vector3.zero)
        {
            // Нормализуем вектор направления для поворота
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        // Двигаем персонажа
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Ограничение по оси Z (от -24 до 0)
        Vector3 currentPosition = transform.position;
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitMin, limitMax);
        transform.position = currentPosition;
    }
    // Метод для обработки столкновений
    private IEnumerator TriggerIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            useAnimation.AutoAttack();
            StartCoroutine(TriggerIdleAfterDelay(0.5f));  // Задержка в 0.5 секунды, настроить по необходимости
        }
    }
}
*/