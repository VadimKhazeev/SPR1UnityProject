using UnityEngine;

public class BacklogAnchor : MonoBehaviour
{
    public RectTransform pcUI;   // UI для ПК
    public RectTransform mobileUI;  // UI для мобильных

    void Start()
    {
        AdjustUI();
    }

    void AdjustUI()
    {
        if (Application.isMobilePlatform)
        {
            // Для мобильных устройств, UI будет занимать весь экран
            mobileUI.anchorMin = new Vector2(0, 0);
            mobileUI.anchorMax = new Vector2(1, 1);
            mobileUI.offsetMin = Vector2.zero; // Убираем отступы
            mobileUI.offsetMax = Vector2.zero; // Убираем отступы
        }
        else
        {
            // Для ПК, UI будет занимать половину экрана
            pcUI.anchorMin = new Vector2(0.4f, 0);  // Половина экрана
            pcUI.anchorMax = new Vector2(1, 1);    // Верхняя половина
            pcUI.offsetMin = Vector2.zero;
            pcUI.offsetMax = Vector2.zero;
        }
    }
}
