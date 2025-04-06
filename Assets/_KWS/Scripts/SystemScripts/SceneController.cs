using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("SceneController Awake - 이벤트 구독 완료");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("SceneController Destroy - 이벤트 구독 해제");
    }

    public void LoadIntroScene()
    {
        Debug.Log("LoadIntroScene 호출");
        SceneManager.LoadScene("IntroScene");
    }

    public void LoadMainScene()
    {
        Debug.Log("LoadMainScene 호출");
        SceneManager.LoadScene("MainScene");
    }

    public void LoadOutroScene()
    {
        Debug.Log("LoadOutroScene 호출");
        SceneManager.LoadScene("OutroScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene 로드됨: {scene.name}, Mode: {mode}");
        if (scene.name == "IntroScene")
        {
            StartCoroutine(InitializeIntroMenuAfterLoad());
        }
        else if (scene.name == "OutroScene")
        {
            StartCoroutine(InitializeOutroMenuAfterLoad());
        }
    }

    private System.Collections.IEnumerator InitializeIntroMenuAfterLoad()
    {
        yield return new WaitForEndOfFrame(); // 첫 프레임 대기
        IntroMenuController introMenu = FindObjectOfType<IntroMenuController>();
        if (introMenu != null)
        {
            Debug.Log("IntroMenuController 초기화 시작");
            introMenu.Initialize();
        }
        else
        {
            Debug.LogError("IntroMenuController를 찾을 수 없습니다!");
        }
    }

    private System.Collections.IEnumerator InitializeOutroMenuAfterLoad()
    {
        yield return new WaitForEndOfFrame(); // 첫 프레임 대기
        OutroMenuController outroMenu = FindObjectOfType<OutroMenuController>();
        if (outroMenu != null)
        {
            Debug.Log("OutroMenuController 초기화 시작");
            outroMenu.Initialize();
        }
        else
        {
            Debug.LogError("OutroMenuController를 찾을 수 없습니다!");
        }
    }
}