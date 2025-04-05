using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_DamageEffect : MonoBehaviour
{
    Image effectImage;
    Coroutine flickerCor;
    void Start()
    {
        effectImage = GetComponent<Image>();
        //이미지 추가
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDamaged(true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerDamaged(false);
        }
    }
    /// <summary>
    /// PlayerDamaged = true, false
    /// Plese Just One Play Corutine Plese
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public void PlayerDamaged(bool isTakingDamage)
    {
        if (!isTakingDamage)
        {
            StopCoroutine(flickerCor);
            effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 0);
            return;
        }
        flickerCor = StartCoroutine(FlickerCor());

    }


    IEnumerator FlickerCor()
    {
        while (true)
        {
            Color color = effectImage.color;
            color.a = 1f;
            while (color.a > 0f)
            {
                float alpha = Mathf.MoveTowards(color.a, 0, Time.fixedDeltaTime * 3f);
                color.a = alpha;
                effectImage.color = color;
                //Debug.Log("FlickerCor daltaTime" + alpha);
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}