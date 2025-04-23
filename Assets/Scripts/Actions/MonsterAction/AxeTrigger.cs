using UnityEngine;

public class AxeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKnight.Instance.TakeDamage(5);
        }
    }
}
