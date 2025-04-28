using System.Collections.Generic;
using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    private Animator _animator;
    private KnightAttack _attack;
    private KnightBlock _block;
    private PlayerSkillData _playerSkillData;
    public static int CurrentSkillAttackPower { get; set; } = 0;
    public static int CurrentSkillKey { get; set; } = -1;

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

        if (IsSkill)
            return;

        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                TryUseSkillInSlot(i);
            }
        }
    }

    private void TryUseSkillInSlot(int slotNumber)
    {
        GameObject slotObj = GameObject.Find($"SkillSlot{slotNumber}");
        if (slotObj == null) return;

        SkillSlot slot = slotObj.GetComponent<SkillSlot>();
        if (slot == null || slot.AssignedSkillKey == -1) return;

        if (slot.IsCoolingDown())
        {
            Debug.Log("스킬 쿨타임 중입니다.");
            return;
        }

        int skillKey = slot.AssignedSkillKey;
        PlayerSkillData skillData = PlayerSkillDataManager.Instance.GetPlayerSkillData(skillKey);

        if (skillData.CurLevel <= 0)
        {
            Debug.Log($"{skillData.Name} 스킬 레벨이 0이라 사용할 수 없습니다.");
            return;
        }

        if (PlayerKnight.Instance.CurMp >= skillData.MpCost)
        {
            PlayerKnight.Instance.ConsumeMp(skillData.MpCost);
            _animator.SetTrigger(GetAnimationTrigger(skillKey));

            slot.StartCoolTime(skillData.CoolTime);

            Debug.Log($"{skillData.Name} 스킬 사용! MP {skillData.MpCost} 소모");
        }
        else
        {
            Debug.Log($"{skillData.Name} 스킬 사용 불가 - MP 부족");
        }

        if (skillKey != 103)
        {
            CurrentSkillAttackPower = skillData.AttackPower;
            CurrentSkillKey = skillKey;
        }
        else
        {
            CurrentSkillAttackPower = 0;
            CurrentSkillKey = -1;
        }
    }

    private string GetAnimationTrigger(int skillKey)
    {
        switch (skillKey)
        {
            case 101: return "TripleSlashSkill";
            case 102: return "JumpSkill";
            case 103: return "PowerUpSkill";
            case 104: return "SpinSlashSkill";
            case 105: return "ChargeSkill";
            default: return "";
        }
    }
}
