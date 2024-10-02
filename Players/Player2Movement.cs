using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public float speed = 10f;  // Скорость движения платформы
    public float limitMin = 26f;
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
        // Создаем векторы для движения по оси X и Z
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Проверяем нажатия клавиш для движения по оси X
        if (Input.GetKey(KeyCode.Keypad5))  // Клавиша 5
        {
            moveHorizontal = 1f;
        }
        else if (Input.GetKey(KeyCode.Keypad8))  // Клавиша 8
        {
            moveHorizontal = -1f;
        }

        // Проверяем нажатия клавиш для движения по оси Z
        if (Input.GetKey(KeyCode.Keypad4)) // Клавиша 4
        {
            moveVertical = -1f;
        }
        else if (Input.GetKey(KeyCode.Keypad6))  // Клавиша 6
        {
            moveVertical = 1f;
        }

        // Создаем вектор движения
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Проверка, если игрок стоит на месте
        if (moveHorizontal == 0.0f && moveVertical == 0.0f)
        {
            animator.SetTrigger("Idle");

            // Если анимация "Idle" включена, ищем пулю
            if (IsIdleAnimationPlaying())
            {
                GameObject bullet = FindBulletNearby();
                if (bullet != null)
                {
                    bulletTarget = bullet.transform;
                    RotateTowardsBullet(bulletTarget);
                }
            }
        }
        else
        {
            animator.SetTrigger("Run");

            // Если есть движение, повернуть персонажа в его направлении
            if (movement != Vector3.zero)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }

            // Двигаем персонажа
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        // Ограничение по оси Z (от 0 до 26)
        Vector3 currentPosition = transform.position;
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitMin, limitMax);
        transform.position = currentPosition;
    }

    // Проверяем, включена ли анимация Idle
    private bool IsIdleAnimationPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
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
            useAnimation.AutoAttack();
            StartCoroutine(TriggerIdleAfterDelay(0.5f));  // Задержка в 0.5 секунды, настроить по необходимости
        }
    }
}
