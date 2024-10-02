using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LevelChanger : MonoBehaviour
{
    // ������ ��� �������� ������ �� ����������� (Raw Image)
    public RawImage[] images;

    // �������� ����, ������� ������ ���� ���������
    public string[] sceneNames;

    void Start()
    {
        // ��������, ����� ���������� �������� � ���� ���������
        if (images.Length != sceneNames.Length)
        {
            Debug.LogError("���������� ����������� � ���� ������ ���������!");
            return;
        }

        // ����, � ������� ��������� ����������� ����� �� ������ ��������
        for (int i = 0; i < images.Length; i++)
        {
            int index = i;  // ��������� ���������� ��� ������������� � ���������

            // ��������� � ������ �������� ��������� ������� �����
            images[i].gameObject.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }
    }

    // �����, ���������� ��� ����� �� ��������
    void OnImageClick(int index)
    {
        if (index >= 0 && index < sceneNames.Length)
        {
            Debug.Log("�������� �����: " + sceneNames[index]);

            //YandexGame.FullscreenShow();
            SceneManager.LoadScene(sceneNames[index]);
        }
        else
        {
            Debug.LogError("������������ ������ �����������!");
        }
    }
}
