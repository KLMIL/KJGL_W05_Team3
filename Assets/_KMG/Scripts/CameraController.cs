using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineCamera cam;
    CinemachineBasicMultiChannelPerlin camMultiChannelPerlin;
    bool _isShaking = false;

    //이동 중 카메라 변화
    float moveSize = 5;              //움직일 시 카메라 사이즈
    float idleSize = 7;              //서있을 시 카메라 사이즈
    float transitionDuration = 0.5f; //카메라 변화 속도
    Coroutine sizeChangeCoroutine;
    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
        camMultiChannelPerlin = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_isShaking)
            {
                ShakeCamera(5, 5);
                _isShaking = true;
            }
            else
            {
                ShakeCamera(0, 0);
                _isShaking = false;
            }
        }


    }
    /// <summary>
    /// 카메라 흔들림 함수
    /// </summary>
    /// <param name="amplitudeGain">흔들림 크기</param>
    /// <param name="frequencyGain">흔들림 빈도</param>

    public void ShakeCamera(int amplitudeGain, int frequencyGain)
    {
        camMultiChannelPerlin.AmplitudeGain = amplitudeGain;
        camMultiChannelPerlin.FrequencyGain = frequencyGain;
    }

    /// <summary>
    /// 카메라 사이즈 변화 함수
    /// </summary>
    /// <param name="isMoving"></param>
    public void ChangeLensSize(bool isMoving)
    {
        // 변환 중 이동상태 변화 시 코루틴 중지
        if (sizeChangeCoroutine != null)
        {
            StopCoroutine(sizeChangeCoroutine);
        }

        // 부드러운 변환을 위한 코루틴 호출
        float targetSize = isMoving ? moveSize : idleSize;
        sizeChangeCoroutine = StartCoroutine(SmoothLensSizeChange(targetSize));
    }

    /// <summary>
    /// 카메라 사이즈 변화 코루틴
    /// </summary>
    /// <param name="targetSize"></param>
    /// <returns></returns>
    private System.Collections.IEnumerator SmoothLensSizeChange(float targetSize)
    {
        float startSize = cam.Lens.OrthographicSize; // Current size
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration; // Progress (0 to 1)
            cam.Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null; // Wait for the next frame
        }

        // Ensure the final size is exact
        cam.Lens.OrthographicSize = targetSize;
    }
}
