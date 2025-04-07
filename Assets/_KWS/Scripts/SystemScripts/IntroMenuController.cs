using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroMenuController : MonoBehaviour
{
    [Header("Assign Sprites")]
    [SerializeField] Sprite _defaultBackground;
    [SerializeField] Sprite _startHoverBackground;
    [SerializeField] GameObject HowToPlayBackground;

    private TextMeshProUGUI _startText;
    private TextMeshProUGUI _howToPlayText;
    //private TextMeshProUGUI _settingsText;
    private TextMeshProUGUI _quitText;
    private Image _backgroundImage;

    private IntroMenuUIManager _uiManager;

    private void Awake()
    {
        Application.targetFrameRate = 61;
    }

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        Debug.Log("IntroMenuController.Initialize 호출");

        // IntroMenuUIManager에서 UI 요소 가져오기
        if (_uiManager == null)
        {
            _uiManager = FindAnyObjectByType<IntroMenuUIManager>();
            if (_uiManager == null)
            {
                Debug.LogError("IntroMenuUIManager를 찾을 수 없습니다!");
                return;
            }
            else
            {
                Debug.Log("IntroMenuUIManager 찾음");
            }
        }

        _startText = _uiManager.startText;
        _howToPlayText = _uiManager.howToPlayText;
        //_settingsText = _uiManager.settingsText;
        _quitText = _uiManager.quitText;
        _backgroundImage = _uiManager.backgroundImage;

        // UI 초기화 및 할당 확인
        if (_startText != null) _startText.text = "게임 시작";
        else Debug.LogError("_startText가 null입니다!");

        if (_howToPlayText != null) _howToPlayText.text = "조작법 설명";
        else Debug.LogError("_howToPlayText가 null입니다!");

        //if (_settingsText != null) _settingsText.text = "설정";
        //else Debug.LogError("_settingsText가 null입니다!");

        if (_quitText != null) _quitText.text = "게임 종료";
        else Debug.LogError("_quitText가 null입니다!");

        if (_backgroundImage != null && _defaultBackground != null)
            _backgroundImage.sprite = _defaultBackground;
        else Debug.LogError("_backgroundImage 또는 _defaultBackground가 null입니다!");

        // 이벤트 설정
        SetupTextEvents(_startText, "시작!", OnStartClick, OnStartHover, OnExitHover);
        SetupTextEvents(_howToPlayText, "조작법!", OnHowToPlayClick);
        //SetupTextEvents(_settingsText, "설정!", OnSettingsClick);
        SetupTextEvents(_quitText, "종료!", OnQuitClick);
    }

    private void SetupTextEvents(TextMeshProUGUI text, string hoverText, UnityEngine.Events.UnityAction clickAction = null,
                                 UnityEngine.Events.UnityAction hoverAction = null, UnityEngine.Events.UnityAction exitAction = null)
    {
        if (text == null)
        {
            Debug.LogError("SetupTextEvents: Text가 null입니다!");
            return;
        }

        EventTrigger trigger = text.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = text.gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Clear();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enterEntry.callback.AddListener((data) =>
        {
            text.text = hoverText;
            if (hoverAction != null) hoverAction.Invoke();
        });
        trigger.triggers.Add(enterEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exitEntry.callback.AddListener((data) =>
        {
            if (text == _startText) text.text = "게임 시작";
            else if (text == _howToPlayText) text.text = "조작법 설명";
            //else if (text == _settingsText) text.text = "설정";
            else if (text == _quitText) text.text = "게임 종료";
            if (exitAction != null) exitAction.Invoke();
        });
        trigger.triggers.Add(exitEntry);

        if (clickAction != null)
        {
            EventTrigger.Entry clickEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            clickEntry.callback.AddListener((data) => { clickAction.Invoke(); });
            trigger.triggers.Add(clickEntry);
        }

        Debug.Log($"이벤트 설정 완료: {text.name}");
    }

    private void OnStartClick()
    {
        SceneController.Instance.LoadLoadingScene();
    }

    private void OnHowToPlayClick()
    {
        HowToPlayBackground.SetActive(true);
    }

    //private void OnSettingsClick()
    //{
    //    Debug.Log("환경 설정 표시");
    //}

    private void OnQuitClick()
    {
        SceneController.Instance.QuitGame();
    }

    private void OnStartHover()
    {
        if (_backgroundImage != null && _startHoverBackground != null)
        {
            _backgroundImage.sprite = _startHoverBackground;
        }
    }

    private void OnExitHover()
    {
        if (_backgroundImage != null && _defaultBackground != null)
        {
            _backgroundImage.sprite = _defaultBackground;
        }
    }
}