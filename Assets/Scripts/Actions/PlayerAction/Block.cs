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
        _animator.Play("Block", 0, 0f);
        StartCoroutine(FreezeOnLastFrame());
    }

    private void StopBlocking()
    {
        _isBlocking = false;
        _animator.Play("Idle", 0, 0f);
        _animator.speed = 1f;
    }

    private System.Collections.IEnumerator FreezeOnLastFrame()
    {
        float blockDuration = _animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(blockDuration);

        if (_isBlocking)
        {
            _animator.speed = 0f;
        }
    }
}
