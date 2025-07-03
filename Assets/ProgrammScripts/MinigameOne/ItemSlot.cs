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

    private Color HighlightedColor = new Color(0.9960784f, 0.572549f, 0.01568628f); // Захардкодил на время
    private Color BasicOutlineColor = new Color(0.2588235f, 0.2588235f, 0.2588235f);

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
