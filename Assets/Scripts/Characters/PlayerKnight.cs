using UnityEngine;

public class PlayerKnight : MonoBehaviour
{
    private static PlayerKnight _instance;
    public static PlayerKnight Instance
    {
        get { return _instance; }
    }

    private Animator _animator;
    private int _level;
    private int _maxLevel = 25;
    private int _hp;
    private int _mp;
    private int _attackPower;
    public int AttackPower { get { return _attackPower; } }

    private void Awake()
    {
        _instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _level = 1;
        _hp = 10;
        _mp = 5;
        _attackPower = 1;
        Debug.Log($"플레이어 시작 스테이터스:\n레벨: {_level}\nHP: {_hp}\nMP: {_mp}\n공격력: {_attackPower}");
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
        _hp += 50;
        _mp += 10;
        _attackPower += 3;
        Debug.Log($"플레이어 현제 스테이터스:\n레벨: {_level}\nHP: {_hp}\nMP: {_mp}\n공격력: {_attackPower}");
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        Debug.Log($"플레이어 피격! 현재 HP: {_hp}");

        if (_hp <= 0)
        {
            _hp = 0;
            Debug.Log("플레이어 사망");

            _animator.SetTrigger("Death");
        }
        else
        {
            _animator.SetTrigger("GetDamage");
        }
    }

    public void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }
}
