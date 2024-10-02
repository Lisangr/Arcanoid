using UnityEngine.UI;
using UnityEngine;

public class ImageSlider : MonoBehaviour
{
    // ������ ��� �������� ���� RawImage
    public RawImage[] images;

    // ������ �������� �����������
    private int currentIndex = 0;

    // ����� ��� ������������� (�������� � Start)
    void Start()
    {
        // ��������, ��� ������ ����������� �� ������
        if (images.Length == 0)
        {
            Debug.LogError("������ ����������� ����!");
            return;
        }

        // �������������: ��� ����������� ����� ������� ������
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex); // ���������� ������ ������ �����������
        }
    }

    // ����� ��� ������������ �� ��������� �����������
    public void ShowNextImage()
    {
        if (images.Length == 0) return;

        // �������� ������� �����������
        images[currentIndex].gameObject.SetActive(false);

        // ����������� ������ (��������)
        currentIndex = (currentIndex + 1) % images.Length;

        // ���������� ��������� �����������
        images[currentIndex].gameObject.SetActive(true);
    }

    // ����� ��� ������������ �� ���������� �����������
    public void ShowPreviousImage()
    {
        if (images.Length == 0) return;

        // �������� ������� �����������
        images[currentIndex].gameObject.SetActive(false);

        // ��������� ������ (��������)
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;

        // ���������� ���������� �����������
        images[currentIndex].gameObject.SetActive(true);
    }
}
