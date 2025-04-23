using UnityEngine;

public class KnightBlock : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private Transform shieldTransform;
    [SerializeField]
    private BoxCollider _shieldCollider;

    public bool IsBlocking { get; private set; }

    private Vector3 originalRotation;
    private bool isRotationSet = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (shieldTransform != null)
            originalRotation = shieldTransform.localEulerAngles;
        if (_shieldCollider != null)
            _shieldCollider.enabled = false;
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
            if (_shieldCollider != null)
                _shieldCollider.enabled = false;
        }

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        IsBlocking = stateInfo.IsTag("Blocking");
    }

    private void StartBlocking()
    {
        _animator.SetBool("isBlocking", true);
    }

    private void StopBlocking()
    {
        _animator.SetBool("isBlocking", false);

        if (shieldTransform != null && isRotationSet)
        {
            shieldTransform.localEulerAngles = originalRotation;
            isRotationSet = false;
        }
    }

    public void SetShieldRotation()
    {
        if (shieldTransform == null)
            return;

        Vector3 currentRotation = shieldTransform.localEulerAngles;
        shieldTransform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, 113.26f);

        isRotationSet = true;
    }

    public void EnableShieldCollider()
    {
        if (_shieldCollider != null)
            _shieldCollider.enabled = true;
    }
}
