using UnityEngine;

public class Card : MonoBehaviour {
    public int cardID; // Уникальный ID карточки
    public Transform originalParent; // Изначальная группа для возврата

    private void Awake() {
        // Сохраняем изначального родителя на старте
        if (originalParent == null) {
            originalParent = transform.parent;
        }
    }
}
