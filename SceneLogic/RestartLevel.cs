using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    // ���� ����� ����� ��������� � ������ ����� Unity Editor
    public void RestartOnClick()
    {
        Time.timeScale = 1.0f;
        // �������� ������ ������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ������������� ������� �����
        SceneManager.LoadScene(currentSceneIndex);        
    }
}
