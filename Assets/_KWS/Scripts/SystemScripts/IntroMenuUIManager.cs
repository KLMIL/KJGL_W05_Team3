using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroMenuUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI startText;
    public TextMeshProUGUI howToPlayText;
    public TextMeshProUGUI settingsText;
    public TextMeshProUGUI quitText;
    public Image backgroundImage;

    private void Awake()
    {
        // Inspector에서 연결된 요소 확인
        if (startText == null) Debug.LogError("UIManager: startText가 연결되지 않았습니다!");
        if (howToPlayText == null) Debug.LogError("UIManager: howToPlayText가 연결되지 않았습니다!");
        if (settingsText == null) Debug.LogError("UIManager: settingsText가 연결되지 않았습니다!");
        if (quitText == null) Debug.LogError("UIManager: quitText가 연결되지 않았습니다!");
        if (backgroundImage == null) Debug.LogError("UIManager: backgroundImage가 연결되지 않았습니다!");
    }
}