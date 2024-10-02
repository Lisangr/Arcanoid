using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    // Этот метод можно привязать к кнопке через Unity Editor
    public void RestartOnClick()
    {
        Time.timeScale = 1.0f;
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneIndex);        
    }
}
