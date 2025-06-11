using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class ProgressBar : MonoBehaviour
{
    public Slider slider; // Ссылка на компонент Slider в Unity UI
    //public Text valueText; // Ссылка на компонент Text, который содержит значение в формате строки
    public static string ProgressScore;
   // public GameObject EncyclopediaMechanic; // задаём энциклопедию
   // public GameObject SaveMechanic; // задаём Кнопку сохранения
   // public GameObject SoundAndMusic;
   // public GameObject FullScrean;

    private void Start()
    {
        StartCoroutine(CoroutineSample());
        // Устанавливаем начальное значение слайдера
        slider.value = 0; // Например, установим значение по умолчанию в 0f
    }

    private IEnumerator CoroutineSample()
    {
        while (true)
        {
            var variableManager = Engine.GetService<ICustomVariableManager>();
            ProgressScore = variableManager.GetVariableValue("Progress");
            // Получаем значение в формате строки из компонента Text
            string valueString = ProgressScore;

            // Пытаемся преобразовать значение в тип int
            if (int.TryParse(valueString, out int valueInt))
            {
                //EncyclopediaMechanic.SetActive(true); // Включаем энциклопедию
               // SaveMechanic.SetActive(true);
               // SoundAndMusic.SetActive(true);
               // FullScrean.SetActive(true);
                // Если преобразование прошло успешно, устанавливаем его в Slider
                slider.value = valueInt;
            }
            else
            {
                // Обработка ошибки, если значение не может быть преобразовано в int
                Debug.Log("Ошибка преобразования строки в int");
            }

            yield return new WaitForSeconds(3);
        }
    }
}
