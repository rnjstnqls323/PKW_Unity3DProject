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
        if (IsAttacking)
            return;

        if ((_block != null && _block.IsBlocking) || (_skill != null && _skill.IsSkill))
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Attack");
            IsAttacking = true;
        }
    }
    
    public void EndAttack()
    {
        IsAttacking = false;
    }
}
