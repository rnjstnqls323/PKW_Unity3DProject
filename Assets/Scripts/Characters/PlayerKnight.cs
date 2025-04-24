using UnityEngine;

public class PlayerKnight : MonoBehaviour
{
    private static PlayerKnight _instance;
    public static PlayerKnight Instance
    {
        get { return _instance; }
    }

    private Animator _animator;
    private int _level = 1;
    private int _hp = 10;
    private int _mp = 5;
    private int _attackPower = 1;

    private void Awake()
    {
        _instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log($"플레이어 시작 스테이터스: 레벨: {_level}, HP: {_hp}, MP: {_mp}, 공격력: {_attackPower}");
    }

    private void Update()
    {
        LevelUp();
    }

    private void LevelUp()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _level++;
            _hp += 50;
            _mp += 10;
            _attackPower += 3;
        }
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
