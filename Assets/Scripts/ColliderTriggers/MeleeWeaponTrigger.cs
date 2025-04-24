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
            Minotaurs.Instance.GetDamage(1);
        }
    }
}
