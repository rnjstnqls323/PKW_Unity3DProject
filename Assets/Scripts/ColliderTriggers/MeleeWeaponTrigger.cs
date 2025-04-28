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
            int damage = 0;

            if (KnightSkill.CurrentSkillKey != -1)
            {
                damage = KnightSkill.CurrentSkillAttackPower;
            }
            else
            {
                damage = PlayerKnight.Instance.AttackPower;
            }

            Minotaurs.Instance.GetDamage(damage);
        }
    }
}
