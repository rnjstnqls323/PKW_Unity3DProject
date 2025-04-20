using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator _animator;
    private Attack _attack;
    private float _walkSpeed = 3f;
    private float _runSpeed = 6f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<Attack>();
    }

    void Update()
    {
        if (_attack != null && _attack.IsAttacking)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = moveDir.magnitude > 0f;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        if (_animator != null)
        {
            _animator.SetBool("isWalking", isMoving && !isRunning);
            _animator.SetBool("isRunning", isRunning);
        }

        if (isMoving)
        {
            transform.forward = moveDir;

            float currentSpeed = isRunning ? _runSpeed : _walkSpeed;
            transform.position += moveDir * currentSpeed * Time.deltaTime;
        }
    }
}
