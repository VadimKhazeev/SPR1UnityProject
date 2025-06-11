using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SoundButtonWithSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider volumeSlider;    // Ссылка на Slider
    public float hideDelay = 5.0f; // Задержка перед скрытием Slider
    private Coroutine hideCoroutine;

    void Start()
    {
        // Убедитесь, что Slider изначально скрыт
        volumeSlider.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Останавливаем скрытие, если оно запущено
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        // Показываем Slider при наведении
        volumeSlider.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Запускаем корутину для скрытия Slider через заданное время
        hideCoroutine = StartCoroutine(HideSliderAfterDelay());
    }

    private IEnumerator HideSliderAfterDelay()
    {
        // Ждем заданное количество времени
        yield return new WaitForSeconds(hideDelay);

        // Скрываем Slider, если мышка не вернулась
        volumeSlider.gameObject.SetActive(false);
    }
}
