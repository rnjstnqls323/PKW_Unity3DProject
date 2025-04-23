using UnityEngine;

public class PlayerKnight : MonoBehaviour
{
    private static PlayerKnight _instance;
    public static PlayerKnight Instance
    {
        get { return _instance; }
    }

    private Animator animator;
    private int hp = 10;

    private void Awake()
    {
        _instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log($"플레이어 시작 체력: {hp}");
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"플레이어 피격! 현재 HP: {hp}");

        if (hp <= 0)
        {
            hp = 0;
            Debug.Log("플레이어 사망");

            animator.SetTrigger("Death");
        }
        else
        {
            animator.SetTrigger("GetDamage");
        }
    }

    public void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }
}
