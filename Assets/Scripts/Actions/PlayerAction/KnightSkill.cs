using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    private Animator _animator;
    private KnightAttack _attack;
    private KnightBlock _block;

    public bool IsSkill { get; private set; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<KnightAttack>();
        _block = GetComponent<KnightBlock>();
    }

    private void Update()
    {
        if ((_attack != null && _attack.IsAttacking) || (_block != null && _block.IsBlocking))
            return;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        IsSkill = stateInfo.IsTag("KnightSkill");

        if (IsSkill)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _animator.SetTrigger("TripleSlashSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animator.SetTrigger("JumpSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animator.SetTrigger("PowerUpSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _animator.SetTrigger("SpinSlashSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _animator.SetTrigger("ChargeSkill");
        }
    }
}
