using System.Collections.Generic;
using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    private Animator _animator;
    private KnightAttack _attack;
    private KnightBlock _block;

    public bool IsSkill { get; private set; }

    private Dictionary<string, int> _skillMpCosts = new Dictionary<string, int>()
    {
        { "TripleSlashSkill", 3 },
        { "JumpSkill", 5 },
        { "PowerUpSkill", 2 },
        { "SpinSlashSkill", 4 },
        { "ChargeSkill", 6 }
    };

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
            UseSkill("TripleSlashSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSkill("JumpSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSkill("PowerUpSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseSkill("SpinSlashSkill");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseSkill("ChargeSkill");
        }
    }

    private void UseSkill(string triggerName)
    {
        PlayerKnight player = PlayerKnight.Instance;

        if (player == null)
            return;

        int mpCost = 0;
        if (_skillMpCosts.TryGetValue(triggerName, out mpCost))
        {
            if (player.CurMp >= mpCost)
            {
                player.ConsumeMp(mpCost);
                _animator.SetTrigger(triggerName);
                Debug.Log($"{triggerName} 스킬 사용! MP {mpCost} 소모 (남은 MP: {player.CurMp})");
            }
            else
            {
                Debug.Log($"MP가 부족하여 {triggerName} 스킬을 사용할 수 없습니다. (필요 MP: {mpCost})");
            }
        }
    }
}
