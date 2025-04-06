using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OutroMenuController : MonoBehaviour
{
    [Header("Assign Image UI")]
    [SerializeField] Image _backgroundImage;       // 배경 이미지를 표시할 Image 컴포넌트
    [SerializeField] Sprite _firstImage;           // 첫 번째 이미지 (Sprite)
    [SerializeField] Sprite _secondImage;          // 두 번째 이미지 (Sprite)

    [Header("Assign Text UI")]
    [SerializeField] TextMeshProUGUI _gameEndText; // Game End 텍스트

    private void Start()
    {
        Initialize(); // 처음 시작 시 초기화
    }

    private void OnEnable()
    {
        Initialize(); // Scene 재진입 시 초기화
    }

    public void Initialize()
    {
        Debug.Log("Outro Scene 초기화");

        // 초기화: 첫 번째 이미지 표시, 텍스트 숨김
        if (_backgroundImage != null && _firstImage != null)
        {
            _backgroundImage.sprite = _firstImage;
        }

        if (_gameEndText != null)
        {
            _gameEndText.text = "Game End";
            _gameEndText.gameObject.SetActive(false); // 텍스트 숨김
        }

        // 기존 Invoke 취소 후 새로 설정
        CancelInvoke();
        Invoke("ShowSecondImage", 2f); // 1초 후 두 번째 이미지 표시
    }

    private void ShowSecondImage()
    {
        if (_backgroundImage != null && _secondImage != null)
        {
            _backgroundImage.sprite = _secondImage; // 두 번째 이미지로 전환
        }

        // 두 번째 이미지 표시 후 1초 뒤 텍스트 표시
        Invoke("ShowGameEndText", 1f);
    }

    private void ShowGameEndText()
    {
        if (_gameEndText != null)
        {
            _gameEndText.gameObject.SetActive(true); // 텍스트 활성화
            SetupTextEvents(_gameEndText, "Back to Main", OnGameEndClick); // 이벤트 설정
        }
    }

    // 텍스트에 이벤트 설정하는 헬퍼 함수
    private void SetupTextEvents(TextMeshProUGUI text, string hoverText, UnityEngine.Events.UnityAction clickAction = null)
    {
        EventTrigger trigger = text.gameObject.GetComponent<EventTrigger>() ?? text.gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Clear(); // 기존 이벤트 제거 후 새로 설정

        // PointerEnter 이벤트
        EventTrigger.Entry enterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enterEntry.callback.AddListener((data) =>
        {
            text.text = hoverText;
        });
        trigger.triggers.Add(enterEntry);

        // PointerExit 이벤트
        EventTrigger.Entry exitEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exitEntry.callback.AddListener((data) =>
        {
            text.text = "Game End"; // 기본 텍스트로 복구
        });
        trigger.triggers.Add(exitEntry);

        // PointerClick 이벤트
        if (clickAction != null)
        {
            EventTrigger.Entry clickEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            clickEntry.callback.AddListener((data) => { clickAction.Invoke(); });
            trigger.triggers.Add(clickEntry);
        }
    }

    // 클릭 이벤트 핸들러
    private void OnGameEndClick()
    {
        SceneController.Instance.LoadIntroScene(); // Intro Scene으로 돌아감
    }
}