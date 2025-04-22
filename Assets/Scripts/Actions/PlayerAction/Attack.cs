using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator _animator;
    private Block _block;
    private Skill _skill;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _block = GetComponent<Block>();
        _skill = GetComponent<Skill>();
    }

    private void Update()
    {
        if ((_block != null && _block.IsBlocking) || (_skill != null && _skill.IsSkill))
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Attack");
        }

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        IsAttacking = stateInfo.IsTag("Attacking");
    }
}
