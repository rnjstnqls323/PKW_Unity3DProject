using UnityEngine;

public class PlayerKnight : MonoBehaviour
{
    private static PlayerKnight _instance;
    public static PlayerKnight Instance
    {
        get { return _instance; }
    }

    private Animator _animator;
    private int _hp = 10;

    private void Awake()
    {
        _instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log($"플레이어 시작 체력: {_hp}");
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
