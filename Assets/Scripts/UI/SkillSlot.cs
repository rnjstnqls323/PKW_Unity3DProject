using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public int AssignedSkillKey { get; private set; } = -1;

    public void AssignSkill(int skillKey)
    {
        AssignedSkillKey = skillKey;
        Debug.Log($"SkillSlot {gameObject.name}¿¡ ½ºÅ³ {skillKey} µî·ÏµÊ");
    }

    public bool HasAssignedSkill(int skillKey)
    {
        return AssignedSkillKey == skillKey;
    }

    public void ClearSlot()
    {
        AssignedSkillKey = -1;
        Debug.Log($"SkillSlot {gameObject.name} ºñ¿öÁü");
    }
}
