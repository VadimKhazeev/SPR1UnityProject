using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class ToggleSoundMobile : MonoBehaviour
{
    // Ссылка на кнопку
    public Button toggleSoundButton;

    // Переменная для хранения состояния звука
    private bool isMuted = false;

    // Ссылка на аудио менеджер Naninovel
    private IAudioManager audioManager;

    // Переменные для хранения начальных значений громкости
    private float initialBgmVolume;
    private float initialSfxVolume;
    private float initialVoiceVolume;

    // Ссылка на Slider для регулировки громкости
    public Slider volumeSlider;

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

        // Добавляем слушатель для изменения значения Slider
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Устанавливаем Slider в активное состояние, если звук включён
        //volumeSlider.gameObject.SetActive(!isMuted);

        // Устанавливаем текущее значение громкости для Slider
        volumeSlider.value = initialBgmVolume;
    }

    // Метод для отключения или включения звука
    void ToggleSoundMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            // Отключаем звук
            audioManager.BgmVolume = 0f;
            audioManager.SfxVolume = 0f;
            audioManager.VoiceVolume = 0f;

            // Деактивируем Slider
            volumeSlider.gameObject.SetActive(false);
        }
        else
        {
            // Включаем звук с исходными уровнями громкости
            audioManager.BgmVolume = initialBgmVolume;
            audioManager.SfxVolume = initialSfxVolume;
            audioManager.VoiceVolume = initialVoiceVolume;

            // Активируем Slider
            volumeSlider.gameObject.SetActive(true);
        }
    }

    // Метод для изменения громкости через Slider
    void SetVolume(float value)
    {
        // Устанавливаем громкость для всех типов звука
        audioManager.BgmVolume = value;
        audioManager.SfxVolume = value;
        audioManager.VoiceVolume = value;

        // Обновляем начальные значения, если их нужно сохранить
        initialBgmVolume = value;
        initialSfxVolume = value;
        initialVoiceVolume = value;
    }
}
