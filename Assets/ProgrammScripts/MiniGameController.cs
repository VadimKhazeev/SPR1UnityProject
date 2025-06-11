using Naninovel;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    // Список UI элементов для мини-игр
    public List<GameObject> miniGameUIs;

    // Список имен переменных для каждой мини-игры
    public List<string> customVariableNames;

    private ICustomVariableManager variableManager;
    private List<string> previousVariableValues;

    void Start()
    {
        // Проверяем, что количество переменных соответствует количеству UI элементов
        if (miniGameUIs.Count != customVariableNames.Count)
        {
            Debug.LogError("Количество UI элементов и переменных не совпадает.");
            return;
        }

        // Получаем сервис для работы с переменными
        variableManager = Engine.GetService<ICustomVariableManager>();

        // Инициализируем список предыдущих значений переменных
        previousVariableValues = new List<string>();

        // Для каждой переменной проверяем начальное состояние
        for (int i = 0; i < customVariableNames.Count; i++)
        {
            string initialValue = variableManager.GetVariableValue(customVariableNames[i]);
            previousVariableValues.Add(initialValue);
            CheckMiniGameState(i, initialValue);
        }
    }

    void Update()
    {
        // Для каждой переменной проверяем текущее состояние и обновляем, если оно изменилось
        for (int i = 0; i < customVariableNames.Count; i++)
        {
            string currentVariableValue = variableManager.GetVariableValue(customVariableNames[i]);

            // Если значение изменилось, обновляем состояние соответствующей мини-игры
            if (currentVariableValue != previousVariableValues[i])
            {
                CheckMiniGameState(i, currentVariableValue);
                previousVariableValues[i] = currentVariableValue;
            }
        }
    }

    void CheckMiniGameState(int index, string variableValue)
    {
        // Если переменная не инициализирована или равна пустой строке, отключаем соответствующий UI элемент
        if (string.IsNullOrEmpty(variableValue))
        {
            miniGameUIs[index].SetActive(false);
            return;
        }

        // Если переменная равна "1", активируем соответствующий UI элемент
        if (variableValue == "1")
        {
            miniGameUIs[index].SetActive(true);
        }
        // Если переменная равна "0", деактивируем соответствующий UI элемент
        else if (variableValue == "0")
        {
            miniGameUIs[index].SetActive(false);
        }
    }
}
