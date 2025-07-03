using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Outline))]
public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private int slotID; // Уникальный идентификатор слота
    [SerializeField]
    private List<GameObject> correctObjects;
    [SerializeField]
    private List<GameObject> incorrectObjects;

    private UnityEngine.UI.Outline Highlighter;

    private Color HighlightedColor = new Color(254f, 146f, 4f); // Захардкодил на время
    private Color BasicOutlineColor = new Color(66f, 66f, 66f);

    private bool isCorrect;

    private void Awake()
    {
        Highlighter = GetComponent<UnityEngine.UI.Outline>();
    }

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
        Highlighter.effectColor = On ? HighlightedColor : BasicOutlineColor;
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
