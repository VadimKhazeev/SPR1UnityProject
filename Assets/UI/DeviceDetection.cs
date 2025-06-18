using System;
using UnityEngine;

public class DeviceDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject mobileUIElement;
    [SerializeField]
    private GameObject desktopUIElement;

    public Action<ScreenOrientation> OnChoseOrientation;

    [SerializeField]
    private bool OverrideIsMobile;

    public bool isMobile { get; private set; }
    private ScreenOrientation lastOrientation;

    void Start()
    {
        isMobile = Application.isMobilePlatform;
        lastOrientation = Screen.orientation;

#if UNITY_EDITOR
        isMobile = OverrideIsMobile;
#endif

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
        OnChoseOrientation?.Invoke(Screen.orientation);
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
