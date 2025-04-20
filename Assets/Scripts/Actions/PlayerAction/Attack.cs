using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator _animator;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Attack");
        }

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        IsAttacking = stateInfo.IsTag("Attack");
    }
}
