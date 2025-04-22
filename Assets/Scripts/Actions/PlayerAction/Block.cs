using UnityEngine;

public class Block : MonoBehaviour
{
    private Animator _animator;

    public bool IsBlocking { get; private set; }

    [SerializeField]
    private Transform shieldTransform;

    private Vector3 originalRotation;
    private bool isRotationSet = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (shieldTransform != null)
            originalRotation = shieldTransform.localEulerAngles;
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
}
