using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenHotKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Переключаем режим полноэкранного отображения
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
