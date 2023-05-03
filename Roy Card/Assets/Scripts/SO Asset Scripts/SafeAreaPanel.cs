using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{
    RectTransform myPanel;

    private void Awake()
    {
        // set panel fit into safe area
        // and then save all the ui buttons into panel
        myPanel = GetComponent<RectTransform>();

        // left bottom
        Vector2 safeAreaMinPosition = Screen.safeArea.position;
        // right upper
        Vector2 safeAreaMaxPosition = Screen.safeArea.position + Screen.safeArea.size;

        // convert it into pixel
        safeAreaMinPosition.x = safeAreaMinPosition.x / Screen.width;
        safeAreaMinPosition.y = safeAreaMinPosition.y / Screen.height;

        safeAreaMaxPosition.x = safeAreaMaxPosition.x / Screen.width;
        safeAreaMaxPosition.y = safeAreaMaxPosition.y / Screen.height;

        myPanel.anchorMin = safeAreaMinPosition;
        myPanel.anchorMax = safeAreaMaxPosition;
    }
}
