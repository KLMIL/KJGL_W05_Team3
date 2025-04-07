using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;       // 기본 애니메이터
    public Animator subAnimator;    // 하위(자식) 애니메이터

    public bool Walking { set { walking = value; } }
    private bool walking = false;

    private void Awake()
    {
        // 현재 오브젝트의 애니메이터 가져오기
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 기본 애니메이터: 걷기 애니메이션 실행
        animator.SetBool("isWalking", walking);

        // 공격 모션 실행 (예: 마우스 왼쪽 버튼 클릭)
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attacking");
            subAnimator.SetTrigger("Attack");
        }
        
    }
}