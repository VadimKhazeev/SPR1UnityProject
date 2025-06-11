using UnityEngine;
using UnityEngine.UI;
using UISwitcher; // Подключаем пространство имен UISwitcher

public class ToggleSoundController : MonoBehaviour
{
    // Ссылка на кнопку
    public Button toggleSoundButton;

    // Ссылка на UISwitcher
    public UISwitcher.UISwitcher soundSwitcher;

    // Переменная для хранения состояния (включен/выключен)
    private bool isSoundOn = true;

    void Start()
    {
        // Добавляем слушатель для кнопки
        toggleSoundButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        // Переключаем состояние
        isSoundOn = !isSoundOn;

        if (isSoundOn)
        {
            // Включаем состояние UISwitcher
            soundSwitcher.Activate();
        }
        else
        {
            // Выключаем состояние UISwitcher
            soundSwitcher.Deactivate();
        }
    }
}
