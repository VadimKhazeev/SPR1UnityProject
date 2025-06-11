using UnityEngine;
using UnityEngine.UI;

public class WebLinkOpener : MonoBehaviour
{
    // Кнопка для открытия ссылки
    public Button openLinkButton;

    // URL для открытия
    public string url = "https://example.com";

    void Start()
    {
        // Привязываем действие к кнопке
        openLinkButton.onClick.AddListener(OpenInCurrentTab);
    }

    // Открывает ссылку в текущей вкладке
    void OpenInCurrentTab()
    {
        Application.OpenURL(url);
    }
}
