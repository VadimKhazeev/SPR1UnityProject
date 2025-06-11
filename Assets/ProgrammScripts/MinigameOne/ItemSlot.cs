using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int slotID; // Уникальный идентификатор слота

    private bool isCorrect = false; // Проверка правильности ответа

    // Списки объектов для управления видимостью
    public List<GameObject> correctObjects;
    public List<GameObject> incorrectObjects;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            AnswerCard answerCard = eventData.pointerDrag.GetComponent<AnswerCard>();
            if (answerCard != null)
            {
                isCorrect = answerCard.cardID == slotID;
            }
        }
    }

    public bool CheckAnswer()
    {
        return isCorrect;
    }

    public void HandleObjectsActivation()
    {
        // Включить/выключить правильные объекты
        foreach (GameObject obj in correctObjects)
        {
            obj.SetActive(isCorrect);
        }

        // Включить/выключить неправильные объекты
        foreach (GameObject obj in incorrectObjects)
        {
            obj.SetActive(!isCorrect);
        }
    }
}
