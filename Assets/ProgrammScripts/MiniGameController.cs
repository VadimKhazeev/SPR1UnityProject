using Naninovel;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [SerializeField]
    private DeviceDetection Detector;

    public GameObject[] miniGameUIs;
    public GameObject[] mobileMiniGameUIs;

    public List<string> customVariableNames;

    private ICustomVariableManager variableManager;
    private List<string> previousVariableValues;

    void Start()
    {
        if (miniGameUIs.Length != customVariableNames.Count)
        {
            Debug.LogError("Количество UI элементов и переменных не совпадает.");
            return;
        }

        variableManager = Engine.GetService<ICustomVariableManager>();

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
        for (int i = 0; i < customVariableNames.Count; i++)
        {
            string currentVariableValue = variableManager.GetVariableValue(customVariableNames[i]);

            if (currentVariableValue != previousVariableValues[i])
            {
                CheckMiniGameState(i, currentVariableValue);
                previousVariableValues[i] = currentVariableValue;
            }
        }
    }

    void CheckMiniGameState(int index, string variableValue)
    {
        if (string.IsNullOrEmpty(variableValue))
        {
            if(Detector.isMobile)
                mobileMiniGameUIs[index].SetActive(false);
            else
                miniGameUIs[index].SetActive(false);
            return;
        }

        if (Detector.isMobile)
            mobileMiniGameUIs[index].SetActive(variableValue == "1");
        else
            miniGameUIs[index].SetActive(variableValue == "1");
    }
}
