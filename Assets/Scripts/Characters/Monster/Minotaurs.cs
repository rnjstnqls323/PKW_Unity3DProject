using UnityEngine;

public class Minotaurs : MonoBehaviour
{
    //테스트
    private static int _globalIdCounter = 1;
    private int _minotaurId;

    /// ///////////////////////////////////////////
    
    private Animator _animator;
    private int _hp = 50;

    public bool IsDead => _hp <= 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _minotaurId = _globalIdCounter++;
        gameObject.name = $"Minotaurs_{_minotaurId}";

        Debug.Log($"[{gameObject.name}] 시작 체력: {_hp}");
    }

    public void GetDamage(int damage)
    {
        if (IsDead) return;

        _hp -= damage;

        Debug.Log($"[{gameObject.name}] 피격! 받은 피해: {damage}, 현재 HP: {_hp}");

        if (_hp <= 0)
        {
            _hp = 0;
            _animator.SetTrigger("Death");

            MinotaurAI ai = GetComponent<MinotaurAI>();
            if (ai != null)
                ai.OnDeath();
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
