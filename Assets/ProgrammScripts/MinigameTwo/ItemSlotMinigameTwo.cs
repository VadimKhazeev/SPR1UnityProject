using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotMinigameTwo : MonoBehaviour, IDropHandler {
    public List<int> validCardIDs; // Список ID подходящих карточек
    public bool allowEmptyAsCorrect = false; // Учитывать ли пустой слот как правильный
    public List<GameObject> successObjects; // Список объектов при успехе
    public List<GameObject> failureObjects; // Список объектов при ошибке
    private Card currentCard; // Текущая вставленная карточка

    // Устанавливаем карточку в слот
    public void SetCard(Card card) {
        currentCard = card;
    }

    // Удаляем карточку из слота
    public void RemoveCard() {
        currentCard = null;
    }

    // Проверяем, является ли слот правильным
    public bool IsCorrect() {
        // Пустой слот считается правильным, если галочка `allowEmptyAsCorrect` активна
        if (currentCard == null && allowEmptyAsCorrect) {
            return true;
        }

        // Проверяем карточку, если слот не пустой
        return currentCard != null && validCardIDs.Contains(currentCard.cardID);
    }

    // Включаем SuccessObjects при правильной карточке
    public void ActivateSuccess() {
        foreach (var obj in successObjects) {
            if (obj != null) obj.SetActive(true);
        }
        foreach (var obj in failureObjects) {
            if (obj != null) obj.SetActive(false);
        }
    }

    // Включаем FailureObjects при неправильной карточке
    public void ActivateFailure() {
        foreach (var obj in failureObjects) {
            if (obj != null) obj.SetActive(true);
        }
        foreach (var obj in successObjects) {
            if (obj != null) obj.SetActive(false);
        }
    }

    // Логика при перетаскивании карточки в слот
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");

        if (eventData.pointerDrag != null) {
            // Проверяем, есть ли уже карточка в слоте
            if (currentCard != null) {
                Debug.Log("Слот уже занят");
                return;
            }

            // Получаем компонент карточки
            Card card = eventData.pointerDrag.GetComponent<Card>();
            if (card != null) {
                // Устанавливаем карточку дочерним объектом слота
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // Сохраняем ссылку на текущую карточку
                SetCard(card);
            }
        }
    }

    // Удаляем карточку, если она покидает слот
    public void OnCardRemoved(Card card) {
        if (currentCard == card) {
            RemoveCard(); // Удаляем ссылку на карточку
            if (card.originalParent != null) {
                card.transform.SetParent(card.originalParent); // Возвращаем в исходную группу
            }
        }
    }

    // Проверка карточки по нажатию кнопки "Готово"
    public void CheckCard() {
        if (IsCorrect()) {
            ActivateSuccess();
        } else {
            ActivateFailure();
        }
    }

    // Сброс состояния слота
    public void ResetSlot() {
        if (currentCard != null) {
            currentCard.transform.SetParent(currentCard.originalParent);
            currentCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            currentCard = null;
        }

        // Выключаем Success и Failure объекты
        foreach (var obj in successObjects) {
            if (obj != null) obj.SetActive(false);
        }
        foreach (var obj in failureObjects) {
            if (obj != null) obj.SetActive(false);
        }
    }
}
