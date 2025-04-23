using UnityEngine;

public class MinotaursAttack : MonoBehaviour
{
    [SerializeField]
    private BoxCollider axeCollider;

    private void Start()
    {
        if (axeCollider != null)
            axeCollider.enabled = false;
    }

    public void EnableAxeCollider()
    {
        if (axeCollider != null)
            axeCollider.enabled = true;
    }

    public void DisableAxeCollider()
    {
        if (axeCollider != null)
            axeCollider.enabled = false;
    }
}
