using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelButton : MonoBehaviour
{
    public int skillKey;
    public TextMeshProUGUI skillLevelText;
    public Button plusButton;
    public Button minusButton;
    public Button resetButton;
    public Button checkButton;

    private PlayerSkillData skillData;
    private int tempLevel;
    private int savedLevel;

    private void Start()
    {
        skillData = PlayerSkillDataManager.Instance.GetPlayerSkillData(skillKey);
        savedLevel = skillData.CurLevel;
        tempLevel = savedLevel;

        plusButton.onClick.AddListener(OnClickPlus);
        minusButton.onClick.AddListener(OnClickMinus);
        resetButton.onClick.AddListener(OnClickReset);
        checkButton.onClick.AddListener(OnClickCheck);

        UpdateSkillLevelText();
    }

    private void OnClickPlus()
    {
        if (tempLevel >= skillData.MaxLevel)
        {
            Debug.Log($"{skillData.Name} 스킬은 최대 레벨입니다.");
            return;
        }

        if (SkillWindow.Instance.SkillLevelUp(1))
        {
            tempLevel++;
            UpdateSkillLevelText();
        }
    }

    private void OnClickMinus()
    {
        if (tempLevel <= savedLevel)
        {
            Debug.Log("저장된 레벨 이하로 감소할 수 없습니다.");
            return;
        }

        tempLevel--;
        SkillWindow.Instance.AddSkillPoint(1);
        UpdateSkillLevelText();
    }

    private void OnClickReset()
    {
        int refundPoint = tempLevel - savedLevel;
        if (refundPoint > 0)
        {
            tempLevel = savedLevel;
            SkillWindow.Instance.AddSkillPoint(refundPoint);
            UpdateSkillLevelText();
            Debug.Log($"{skillData.Name} 스킬 리셋! 포인트 {refundPoint} 반환");
        }
    }

    private void OnClickCheck()
    {
        savedLevel = tempLevel;
        PlayerSkillDataManager.Instance.UpdateSkillLevel(skillKey, savedLevel);
        Debug.Log($"{skillData.Name} 스킬 레벨 확정: Lv.{savedLevel}");
    }

    private void UpdateSkillLevelText()
    {
        skillLevelText.text = $"Lv.{tempLevel}";
    }
}
