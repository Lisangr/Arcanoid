using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LevelChanger : MonoBehaviour
{
    // Массив для хранения ссылок на изображения (Raw Image)
    public RawImage[] images;

    // Названия сцен, которые должны быть загружены
    public string[] sceneNames;

    void Start()
    {
        // Проверка, чтобы количество картинок и сцен совпадало
        if (images.Length != sceneNames.Length)
        {
            Debug.LogError("Количество изображений и сцен должно совпадать!");
            return;
        }

        // Цикл, в котором добавляем обработчики клика на каждую картинку
        for (int i = 0; i < images.Length; i++)
        {
            int index = i;  // Локальная переменная для использования в замыкании

            // Добавляем к каждой картинке слушатель события клика
            images[i].gameObject.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }
    }

    // Метод, вызываемый при клике на картинку
    void OnImageClick(int index)
    {
        if (index >= 0 && index < sceneNames.Length)
        {
            Debug.Log("Загрузка сцены: " + sceneNames[index]);

            //YandexGame.FullscreenShow();
            SceneManager.LoadScene(sceneNames[index]);
        }
        else
        {
            Debug.LogError("Некорректный индекс изображения!");
        }
    }
}
