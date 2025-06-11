using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour {
    [System.Serializable]
    public class SlotGroup {
        public List<ItemSlotMinigameTwo> slots; // Слоты в группе
        public GameObject successObject; // Объект при успехе
        public GameObject failureObject; // Объект при ошибке
    }

    public List<ItemSlotMinigameTwo> slots; // Все слоты
    public List<GameObject> globalSuccessObjects; // Глобальные объекты при успехе
    public List<GameObject> globalFailureObjects; // Глобальные объекты при ошибке
    public List<SlotGroup> slotGroups; // Группы слотов

    // Проверка всех слотов и групп при нажатии кнопки "Готово"
    public void OnReadyButton() {
        bool allGlobalCorrect = true;

        // Проверяем глобальные слоты
        foreach (var slot in slots) {
            slot.CheckCard(); // Проверяем карточку в слоте

            if (!slot.IsCorrect()) {
                allGlobalCorrect = false;
            }
        }

        // Активируем глобальные объекты в зависимости от результата
        if (allGlobalCorrect) {
            foreach (var obj in globalSuccessObjects) obj.SetActive(true);
            foreach (var obj in globalFailureObjects) obj.SetActive(false);
        } else {
            foreach (var obj in globalFailureObjects) obj.SetActive(true);
            foreach (var obj in globalSuccessObjects) obj.SetActive(false);
        }

        // Проверяем группы слотов
        foreach (var group in slotGroups) {
            bool allGroupCorrect = true;

            foreach (var slot in group.slots) {
                if (!slot.IsCorrect()) {
                    allGroupCorrect = false;
                    break;
                }
            }

            // Активируем объекты для группы
            if (allGroupCorrect) {
                if (group.successObject != null) group.successObject.SetActive(true);
                if (group.failureObject != null) group.failureObject.SetActive(false);
            } else {
                if (group.failureObject != null) group.failureObject.SetActive(true);
                if (group.successObject != null) group.successObject.SetActive(false);
            }
        }
    }

    // Сброс всех слотов и групп при нажатии кнопки "Сброс"
    public void OnResetButton() {
        // Сбрасываем все глобальные слоты
        foreach (var slot in slots) {
            slot.ResetSlot();
        }

        // Отключаем глобальные объекты
        foreach (var obj in globalSuccessObjects) obj.SetActive(false);
        foreach (var obj in globalFailureObjects) obj.SetActive(false);

        // Сбрасываем слоты в группах
        foreach (var group in slotGroups) {
            foreach (var slot in group.slots) {
                slot.ResetSlot();
            }

            // Выключаем объекты успеха и ошибки для групп
            if (group.successObject != null) group.successObject.SetActive(false);
            if (group.failureObject != null) group.failureObject.SetActive(false);
        }
    }
}
