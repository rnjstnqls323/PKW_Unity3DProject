using UnityEngine;

public class Skill : MonoBehaviour
{
    private Animator _animator;
    private Attack _attack;
    private Block _block;

    public bool IsSkill { get; private set; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<Attack>();
        _block = GetComponent<Block>();
    }

    private void Update()
    {
        if ((_attack != null && _attack.IsAttacking) || (_block != null && _block.IsBlocking))
            return;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        IsSkill = stateInfo.IsTag("Skill");
        if (IsSkill)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animator.SetTrigger("Skill3");
        }
    }
}
