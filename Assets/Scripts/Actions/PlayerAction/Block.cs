using UnityEngine;

public class Block : MonoBehaviour
{
    private Animator _animator;

    public bool IsBlocking { get; private set; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartBlocking();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            StopBlocking();
        }

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        IsBlocking = stateInfo.IsTag("Blocking");
    }

    private void StartBlocking()
    {
        _animator.SetBool("isBlocking", true);
    }

    private void StopBlocking()
    {
        _animator.SetBool("isBlocking", false);
    }
}
