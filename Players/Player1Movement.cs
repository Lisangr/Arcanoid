using System.Collections;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 10f;  // �������� �������� ���������
    public float limitMin = -24f;
    public float limitMax = 0f;
    public float detectionRadius = 10f;  // ������ ����������� ����
    private Animator animator;
    private UseAnimation useAnimation;  // ������ �� ��������� UseAnimation
    private Transform bulletTarget;  // ������ �� ����

    private void Start()
    {
        animator = GetComponent<Animator>();
        useAnimation = GetComponent<UseAnimation>();
    }

    void Update()
    {
        // �������� ���� ������������
        float moveHorizontal = Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");

        // ������� ������ ��������
        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, moveVertical);

        // ���� �������� �������� ����� 0 (����� ����� �� �����)
        if (movement == Vector3.zero)
        {
            // ��������� � ��������� Idle
            animator.SetTrigger("Idle");

            // ������ ������ Bullet � ������� ��������
            GameObject bullet = FindBulletNearby();

            if (bullet != null)
            {
                // ������������ ������ � ������� ����
                bulletTarget = bullet.transform;
                RotateTowardsBullet(bulletTarget);
            }
        }
        else
        {
            // ���� ����� ���������, ����������� �������� �� Run
            animator.SetTrigger("Run");

            // ������������ ��������� � ����������� ��������
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            // ������� ���������
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        // ����������� �� ��� Z (�� -24 �� 0)
        Vector3 currentPosition = transform.position;
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitMin, limitMax);
        transform.position = currentPosition;
    }

    // ����� ��� ������ ���� ����������
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

    // ����� ��� �������� ������ � ����
    private void RotateTowardsBullet(Transform bulletTransform)
    {
        Vector3 directionToBullet = bulletTransform.position - transform.position;
        directionToBullet.y = 0;  // ���������� ��� Y, ����� ����� �� �������� �� ���������
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToBullet, speed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // ����� ��� ��������� ������������
    private IEnumerator TriggerIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletTarget = collision.transform;  // ��������� ������ �� ������ ����
            RotateTowardsBullet(bulletTarget);  // �������������� � ���� ��� ������������
            useAnimation.AutoAttack();
            StartCoroutine(TriggerIdleAfterDelay(0.5f));  // �������� � 0.5 �������
        }
    }
}








/*
using System.Collections;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 10f;  // �������� �������� ���������
    public float limitMin = -24f;
    public float limitMax = 0f;
    private Animator animator;
    private UseAnimation useAnimation;  // ������ �� ��������� UseAnimation
    private void Start()
    {
        animator = GetComponent<Animator>();
        useAnimation = GetComponent<UseAnimation>();
    }
    void Update()
    {
        // �������� ���� ������������
        float moveHorizontal = Input.GetAxis("Vertical");
        float moveVertical = Input.GetAxis("Horizontal");

        // ������� ������ ��������
        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, moveVertical);
        if (moveHorizontal != 0.0f || moveVertical != 0.0f)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        // ���� ���� ��������, ��������� ��������� � ��� �����������
        if (movement != Vector3.zero)
        {
            // ����������� ������ ����������� ��� ��������
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        // ������� ���������
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // ����������� �� ��� Z (�� -24 �� 0)
        Vector3 currentPosition = transform.position;
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitMin, limitMax);
        transform.position = currentPosition;
    }
    // ����� ��� ��������� ������������
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
            StartCoroutine(TriggerIdleAfterDelay(0.5f));  // �������� � 0.5 �������, ��������� �� �������������
        }
    }
}
*/