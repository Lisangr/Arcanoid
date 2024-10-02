using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public float speed = 10f;  // �������� �������� ���������
    public float limitMin = 26f;
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
        // ������� ������� ��� �������� �� ��� X � Z
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // ��������� ������� ������ ��� �������� �� ��� X
        if (Input.GetKey(KeyCode.Keypad5))  // ������� 5
        {
            moveHorizontal = 1f;
        }
        else if (Input.GetKey(KeyCode.Keypad8))  // ������� 8
        {
            moveHorizontal = -1f;
        }

        // ��������� ������� ������ ��� �������� �� ��� Z
        if (Input.GetKey(KeyCode.Keypad4)) // ������� 4
        {
            moveVertical = -1f;
        }
        else if (Input.GetKey(KeyCode.Keypad6))  // ������� 6
        {
            moveVertical = 1f;
        }

        // ������� ������ ��������
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // ��������, ���� ����� ����� �� �����
        if (moveHorizontal == 0.0f && moveVertical == 0.0f)
        {
            animator.SetTrigger("Idle");

            // ���� �������� "Idle" ��������, ���� ����
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

            // ���� ���� ��������, ��������� ��������� � ��� �����������
            if (movement != Vector3.zero)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }

            // ������� ���������
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        // ����������� �� ��� Z (�� 0 �� 26)
        Vector3 currentPosition = transform.position;
        currentPosition.z = Mathf.Clamp(currentPosition.z, limitMin, limitMax);
        transform.position = currentPosition;
    }

    // ���������, �������� �� �������� Idle
    private bool IsIdleAnimationPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
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
            useAnimation.AutoAttack();
            StartCoroutine(TriggerIdleAfterDelay(0.5f));  // �������� � 0.5 �������, ��������� �� �������������
        }
    }
}
