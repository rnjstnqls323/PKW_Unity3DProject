using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerKnight : MonoBehaviour
{
    private static PlayerKnight _instance;
    public static PlayerKnight Instance { get { return _instance; } }

    private Animator _animator;
    private Image _hpBarImage;
    private Image _mpBarImage;
    private TextMeshProUGUI _hpBarText;
    private TextMeshProUGUI _mpBarText;

    private int _level;
    private int _maxLevel = 25;
    private int _curHp;
    private int _maxHp;
    private int _curMp;
    private int _maxMp;
    private int _attackPower;
    public int AttackPower { get { return _attackPower; } }
    public int CurMp { get { return _curMp; } }

    private void Awake()
    {
        _instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _hpBarImage = GameObject.Find("PlayerHpBar").GetComponent<Image>();
        _hpBarText = GameObject.Find("PlayerHpBarText").GetComponentInChildren<TextMeshProUGUI>();
        _mpBarImage = GameObject.Find("PlayerMpBar").GetComponent<Image>();
        _mpBarText = GameObject.Find("PlayerMpBarText").GetComponentInChildren<TextMeshProUGUI>();

        _level = 1;
        _maxHp = 10;
        _curHp = _maxHp;
        _maxMp = 5;
        _curMp = _maxMp;
        _attackPower = 1;

        UpdateHpBar();
        UpdateMpBar();

        Debug.Log($"플레이어 시작 스테이터스:\n레벨: {_level}\nHP: {_curHp}\nMP: {_curMp}\n공격력: {_attackPower}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (_level >= _maxLevel)
        {
            Debug.Log($"최고레벨{_maxLevel} 달성");
            return;
        }

        _level += 1;
        _maxHp += 50;
        _curHp = _maxHp;
        _maxMp += 10;
        _curMp = _maxMp;
        _attackPower += 3;

        if (SkillWindow.Instance != null)
        {
            SkillWindow.Instance.AddSkillPoint(3);
        }

        UpdateHpBar();
        UpdateMpBar();

        Debug.Log($"플레이어 현제 스테이터스:\n레벨: {_level}\nHP: {_curHp}\nMP: {_curMp}\n공격력: {_attackPower}");
    }

    public void GetDamage(int damage)
    {
        _curHp -= damage;
        Debug.Log($"플레이어 피격! 현재 HP: {_curHp}");

        UpdateHpBar();

        if (_curHp <= 0)
        {
            _curHp = 0;
            Debug.Log("플레이어 사망");

            _animator.SetTrigger("Death");
        }
        else
        {
            _animator.SetTrigger("GetDamage");
        }
    }

    private void UpdateHpBar()
    {
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = (float)_curHp / _maxHp;
        }

        if (_hpBarText != null)
        {
            _hpBarText.text = $"{_curHp} / {_maxHp}";
        }
    }

    private void UpdateMpBar()
    {
        if (_mpBarImage != null)
        {
            _mpBarImage.fillAmount = (float)_curMp / _maxMp;
        }

        if (_mpBarText != null)
        {
            _mpBarText.text = $"{_curMp} / {_maxMp}";
        }
    }

    public void ConsumeMp(int amount)
    {
        _curMp -= amount;
        if (_curMp < 0)
            _curMp = 0;

        UpdateMpBar();
    }

    public void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }
}
