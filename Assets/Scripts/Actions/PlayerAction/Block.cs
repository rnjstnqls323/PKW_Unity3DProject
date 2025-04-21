using UnityEngine;

public class Block : MonoBehaviour
{
    private Animator _animator;
    private bool _isBlocking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartBlocking();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            StopBlocking();
        }
    }

    private void StartBlocking()
    {
        _isBlocking = true;
        _animator.SetBool("isBlocking", true);
    }

    private void StopBlocking()
    {
        _isBlocking = false;
        _animator.SetBool("isBlocking", false);
    }
}
