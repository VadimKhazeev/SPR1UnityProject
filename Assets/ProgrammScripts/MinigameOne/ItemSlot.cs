using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private int slotID; // Уникальный идентификатор слота
    [SerializeField]
    private List<GameObject> correctObjects;
    [SerializeField]
    private List<GameObject> incorrectObjects;
    [SerializeField]
    private GameObject Highlighter;

    private bool isCorrect = false; // Проверка правильности ответа

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            Highlight(false);

            AnswerCard answerCard = eventData.pointerDrag.GetComponent<AnswerCard>();
            if (answerCard != null)
            {
                isCorrect = answerCard.cardID == slotID;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Highlight(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Highlight(false);
        }
    }

    private void Highlight(bool On)
    {
        Highlighter.SetActive(On);
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
