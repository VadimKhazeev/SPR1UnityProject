using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class ToggleSound : MonoBehaviour
{
    // Это ссылка на кнопку
    public Button toggleSoundButton;

    // Ссылки на изображения для состояний звука
    public Image soundButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    // Переменная для хранения состояния звука
    private bool isMuted = false;

    // Ссылка на аудио менеджер Naninovel
    private IAudioManager audioManager;

    // Переменные для хранения начальных значений громкости
    private float initialBgmVolume;
    private float initialSfxVolume;
    private float initialVoiceVolume;

    void Start()
    {
        // Получаем ссылку на аудио менеджер Naninovel
        audioManager = Engine.GetService<IAudioManager>();

        // Сохраняем начальные значения громкости
        initialBgmVolume = audioManager.BgmVolume;
        initialSfxVolume = audioManager.SfxVolume;
        initialVoiceVolume = audioManager.VoiceVolume;

        // Добавляем слушатель для кнопки
        toggleSoundButton.onClick.AddListener(ToggleSoundMute);

        // Устанавливаем начальное изображение кнопки
        UpdateButtonImage();
    }

    void Update()
    {
        // Проверяем, была ли нажата клавиша M
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Вызываем метод переключения звука
            ToggleSoundMute();
        }
    }

    void ToggleSoundMute()
    {
        // Переключаем состояние звука
        isMuted = !isMuted;

        if (isMuted)
        {
            // Отключаем звук
            audioManager.BgmVolume = 0f;
            audioManager.SfxVolume = 0f;
            audioManager.VoiceVolume = 0f;
        }
        else
        {
            // Включаем звук
            audioManager.BgmVolume = initialBgmVolume;
            audioManager.SfxVolume = initialSfxVolume;
            audioManager.VoiceVolume = initialVoiceVolume;
        }

        // Обновляем изображение кнопки
        UpdateButtonImage();
    }

    void UpdateButtonImage()
    {
        if (isMuted)
        {
            soundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            soundButtonImage.sprite = soundOnSprite;
        }
    }
}
