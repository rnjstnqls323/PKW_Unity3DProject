using UnityEngine;

public class MeleeWeaponTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKnight.Instance.GetDamage(5);
        }

        if (other.CompareTag("Monster"))
        {
            if (other.gameObject == transform.root.gameObject)
                return;

            int damage = 0;

            if (KnightSkill.CurrentSkillKey != -1)
            {
                damage = KnightSkill.CurrentSkillAttackPower;
            }
            else
            {
                damage = PlayerKnight.Instance.AttackPower;
            }

            Minotaurs monster = other.GetComponent<Minotaurs>();
            if (monster != null)
            {
                monster.GetDamage(damage);
            }
        }
    }
}
