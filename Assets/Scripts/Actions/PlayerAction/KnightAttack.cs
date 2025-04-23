using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    private Animator _animator;
    private KnightBlock _block;
    private KnightSkill _skill;
    [SerializeField]
    private BoxCollider _swordCollider;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _block = GetComponent<KnightBlock>();
        _skill = GetComponent<KnightSkill>();

        if (_swordCollider != null)
            _swordCollider.enabled = false;
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

    public void EnableSwordCollider()
    {
        if (_swordCollider != null)
            _swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        if (_swordCollider != null)
            _swordCollider.enabled = false;
    }
}
