using UnityEngine;

public class Minotaurs : MonoBehaviour
{
    private static Minotaurs _instance;
    public static Minotaurs Instance
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
        Debug.Log($"미노타우르스 시작 체력: {_hp}");
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        Debug.Log($"미노타우르스 피격! 현재 HP: {_hp}");

        if (_hp <= 0)
        {
            _hp = 0;
            Debug.Log("미노타우르스 사망");

            _animator.SetTrigger("Death");
        }
        else
        {
            _animator.SetTrigger("GetDamage");
        }
    }

    public void DeactivateMinotaurs()
    {
        gameObject.SetActive(false);
    }
}
