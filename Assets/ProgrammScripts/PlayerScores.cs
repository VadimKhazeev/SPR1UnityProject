using System;
using System.Collections;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class PlayerScores : MonoBehaviour
{
    public Text tokenText;

    public static string startGame; // Пользователь зашёл в игру
    public static string firstStepsInGame; // Прошёл первые 20 строк сценария
    public static string middleStepsInGame; // Дошел до середины игры
    public static string lateStepsInGame; // Почти дошел до конца игры
    public static string was_finishedValue; // Завершил игру

    public static string playerName;

    private void Start()
    {
        // Генерация уникального имени пользователя и установка его в текстовое поле
        tokenText.text = GenerateUniquePlayerName();
        playerName = tokenText.text;

        StartCoroutine(CoroutineSample());
    }

    private IEnumerator CoroutineSample()
    {
        var variableManager = Engine.GetService<ICustomVariableManager>();

        while (true)
        {
            startGame = variableManager.GetVariableValue("StartG");
            firstStepsInGame = variableManager.GetVariableValue("FirstStepIn");
            middleStepsInGame = variableManager.GetVariableValue("MiddleStepIn");
            lateStepsInGame = variableManager.GetVariableValue("LateStepIn");
            was_finishedValue = variableManager.GetVariableValue("FinishedValue");

            if (startGame == "true")
            {
                IncrementCounter("startGameCounter");
                startGame = "false"; // чтобы не инкрементировать счетчик снова
                PostToDatabase("startGame");
            }

            if (firstStepsInGame == "true")
            {
                IncrementCounter("firstStepsInGameCounter");
                firstStepsInGame = "false"; // чтобы не инкрементировать счетчик снова
                PostToDatabase("firstStepsInGame");
            }

            if (middleStepsInGame == "true")
            {
                IncrementCounter("middleStepsInGameCounter");
                middleStepsInGame = "false"; // чтобы не инкрементировать счетчик снова
                PostToDatabase("middleStepsInGame");
            }

            if (lateStepsInGame == "true")
            {
                IncrementCounter("lateStepsInGameCounter");
                lateStepsInGame = "false"; // чтобы не инкрементировать счетчик снова
                PostToDatabase("lateStepsInGame");
            }

            if (was_finishedValue == "true")
            {
                IncrementCounter("wasFinishedCounter");
                was_finishedValue = "false"; // чтобы не инкрементировать счетчик снова
                PostToDatabase("wasFinished");
            }

            yield return new WaitForSeconds(5);
        }
    }

    private void IncrementCounter(string counterName)
    {
        RestClient.Get<int>($"https://spr-samolov-default-rtdb.europe-west1.firebasedatabase.app/{counterName}.json").Then(response =>
        {
            int currentValue = response;
            RestClient.Put($"https://spr-samolov-default-rtdb.europe-west1.firebasedatabase.app/{counterName}.json", currentValue + 1);
        });
    }

    private void PostToDatabase(string eventType)
    {
        PlayerEvent playerEvent = new PlayerEvent { playerName = playerName, eventType = eventType };
        RestClient.Put("https://spr-samolov-default-rtdb.europe-west1.firebasedatabase.app/players/" + playerName + ".json", playerEvent);
    }

    private string GenerateUniquePlayerName()
    {
        // Получаем текущее время в формате "yyyyMMddHHmm"
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmm");

        // Генерируем случайное число для добавления дополнительной уникальности
        string randomPart = UnityEngine.Random.Range(1000, 9999).ToString();

        // Комбинируем метку времени и случайное число
        string uniquePlayerName = "Player_" + timestamp + "_" + randomPart;

        return uniquePlayerName;
    }
}

public class PlayerEvent
{
    public string playerName;
    public string eventType;
}
