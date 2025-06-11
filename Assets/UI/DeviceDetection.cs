using UnityEngine;

public class DeviceDetection : MonoBehaviour
{
    public GameObject mobileUIElement;
    public GameObject desktopUIElement;

    private bool isMobile;
    private ScreenOrientation lastOrientation;

    void Start()
    {
        isMobile = Application.isMobilePlatform;
        lastOrientation = Screen.orientation;

        // Изначальная проверка ориентации экрана
        UpdateUIBasedOnOrientation();
    }

    void OnRectTransformDimensionsChange()
    {
        // Проверка изменения ориентации экрана только при изменении ориентации
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            UpdateUIBasedOnOrientation();
        }
    }

    void UpdateUIBasedOnOrientation()
    {
        if (isMobile)
        {
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight || Screen.width > Screen.height)
            {
                mobileUIElement.SetActive(false);
                desktopUIElement.SetActive(true);
            }
            else
            {
                mobileUIElement.SetActive(true);
                desktopUIElement.SetActive(false);
            }
        }
        else
        {
            mobileUIElement.SetActive(false);
            desktopUIElement.SetActive(true);
        }
    }
}
