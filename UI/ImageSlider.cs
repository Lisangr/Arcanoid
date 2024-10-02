using UnityEngine.UI;
using UnityEngine;

public class ImageSlider : MonoBehaviour
{
    // Массив для хранения всех RawImage
    public RawImage[] images;

    // Индекс текущего изображения
    private int currentIndex = 0;

    // Метод для инициализации (вызываем в Start)
    void Start()
    {
        // Проверка, что массив изображений не пустой
        if (images.Length == 0)
        {
            Debug.LogError("Массив изображений пуст!");
            return;
        }

        // Инициализация: все изображения кроме первого скрыты
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex); // Показываем только первое изображение
        }
    }

    // Метод для переключения на следующее изображение
    public void ShowNextImage()
    {
        if (images.Length == 0) return;

        // Скрываем текущее изображение
        images[currentIndex].gameObject.SetActive(false);

        // Увеличиваем индекс (циклично)
        currentIndex = (currentIndex + 1) % images.Length;

        // Показываем следующее изображение
        images[currentIndex].gameObject.SetActive(true);
    }

    // Метод для переключения на предыдущее изображение
    public void ShowPreviousImage()
    {
        if (images.Length == 0) return;

        // Скрываем текущее изображение
        images[currentIndex].gameObject.SetActive(false);

        // Уменьшаем индекс (циклично)
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;

        // Показываем предыдущее изображение
        images[currentIndex].gameObject.SetActive(true);
    }
}
