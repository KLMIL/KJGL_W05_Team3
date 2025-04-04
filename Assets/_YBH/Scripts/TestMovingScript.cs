using UnityEngine;

public class TestMovingScript : MonoBehaviour
{
    public Animator animator;       // 기본 애니메이터
    public Animator subAnimator;    // 하위(자식) 애니메이터

    void Start()
    {
        // 현재 오브젝트의 애니메이터 가져오기
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 기본 애니메이터: 걷기 애니메이션 실행
        animator.SetBool("isWalking", Input.GetKey(KeyCode.Space));

        // 공격 모션 실행 (예: 마우스 왼쪽 버튼 클릭)
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Attacking");
            subAnimator.SetTrigger("Attack");
        }

    }
}