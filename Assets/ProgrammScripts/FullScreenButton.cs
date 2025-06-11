using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class FullScreenToggleButton : MonoBehaviour
{
    public Image fullScreenButtonImage;  // Ссылка на UI элемент кнопки (картинка)
    public Sprite fullScreenSprite;      // Спрайт для полноэкранного режима
    public Sprite windowedSprite;        // Спрайт для оконного режима

    // Метод для вызова при нажатии кнопки
    public void ToggleFullScreen()
    {
        // Проверка, поддерживает ли браузер полноэкранный режим
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        // Обновляем изображение кнопки
        UpdateButtonImage();
    }

    // Метод для обновления изображения кнопки
    void UpdateButtonImage()
    {
        if (Screen.fullScreen)
        {
            fullScreenButtonImage.sprite = fullScreenSprite;  // Изображение для полноэкранного режима
        }
        else
        {
            fullScreenButtonImage.sprite = windowedSprite;    // Изображение для оконного режима
        }
    }

    // Устанавливаем слушатель на кнопку
    void Start()
    {
        // Вешаем событие на кнопку (при условии, что у вас есть Button компонент)
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleFullScreen);
        }

        UpdateButtonImage();
    }
}
