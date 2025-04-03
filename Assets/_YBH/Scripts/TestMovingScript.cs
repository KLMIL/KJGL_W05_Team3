using UnityEngine;

public class TestMovingScript : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", Input.GetKey(KeyCode.Space));
    }
}

