using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [SerializeField]
    private DeviceDetection Detector;
    [Space]
    [SerializeField]
    private RectTransform WhatToChange;
    [SerializeField]
    private Vector2 MobileMinOffsets;
    [SerializeField]
    private Vector2 MobileMaxOffsets;

    private void Awake()
    {
        Detector.OnChoseOrientation += AdjustUI;
    }

    private void AdjustUI(ScreenOrientation Orientation)
    {
        if(Detector.isMobile && !(Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight))
        {
            WhatToChange.offsetMin = new Vector2(MobileMinOffsets.x, MobileMinOffsets.y);
            WhatToChange.offsetMax = new Vector2(-MobileMaxOffsets.x, -MobileMaxOffsets.y);
        }
    }
}
