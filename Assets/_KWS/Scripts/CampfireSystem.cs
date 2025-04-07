using UnityEngine;

public class CampfireSystem : MonoBehaviour
{
    float _duration = 10f;
    [SerializeField] float _currTime = 0f;
    [SerializeField] bool _isIgnited = false;

    [SerializeField] Sprite campfireOnSprite;
    [SerializeField] Sprite campfireOffSprite;

    Light lightRef;
    ParticleSystem fireParticle;
    SpriteRenderer spriteRenderer;



    private void Awake()
    {
        lightRef = GetComponent<Light>();
        fireParticle = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        lightRef.enabled = false;
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
        lightRef.enabled = true;
        fireParticle.Play();
        spriteRenderer.sprite = campfireOnSprite;
        PlayerManager.Instance.NearCampfire = true;
    }

    // 불을 끄는 기능
    public void PutoutCampfire()
    {
        _isIgnited = false;
        lightRef.enabled = false;
        fireParticle.Stop();
        spriteRenderer.sprite = campfireOffSprite;
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
