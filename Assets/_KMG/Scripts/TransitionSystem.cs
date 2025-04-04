using System.Collections;
using UnityEngine;

public class TransitionSystem : MonoBehaviour
{
    [SerializeField] Transform target;

    public void Transition()
    {
        UIManager.Instance.CallFadeInFadeOut();
        StartCoroutine(WaitUntilFadeout());
    }

    IEnumerator WaitUntilFadeout()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.PlayerManager.PlayerController. transform.position = target.position;
    }
}
