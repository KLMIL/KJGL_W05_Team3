using UnityEngine;

public class CampfireSystem : MonoBehaviour
{
    float _duration = 10f;
    [SerializeField] float _currTime = 0f;
    [SerializeField] bool _isIgnited = false;

    Light light;
    ParticleSystem fireParticle;

    private void Awake()
    {
        light = GetComponent<Light>();
        fireParticle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        light.enabled = false;
        fireParticle.Stop();
    }


    // 지속시간 검사
    private void Update()
    {
        if (_isIgnited)
        {
            _currTime += Time.deltaTime;
            if (_currTime >= _duration)
            {
                PutoutCampfire();
            }
        }
    }


    // 불을 켜는 기능
    public void IgniteCampfire()
    {
        _isIgnited = true;
        _currTime = 0;
        light.enabled = true;
        fireParticle.Play();
        PlayerManager.Instance.NearCampfire = true;
    }

    // 불을 끄는 기능
    public void PutoutCampfire()
    {
        _isIgnited = false;
        light.enabled = false;
        fireParticle.Stop();
        PlayerManager.Instance.NearCampfire = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isIgnited && collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.NearCampfire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.NearCampfire = false;
        }
    }
}
