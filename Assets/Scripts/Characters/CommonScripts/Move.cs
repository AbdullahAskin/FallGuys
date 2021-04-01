using UnityEngine;

public abstract class Move : MonoBehaviour, IMove
{
    [HideInInspector] public float speed, maxVelocity;
    [HideInInspector] public GameObject _gfx;
    [HideInInspector] public Rigidbody _rb;
    [HideInInspector] public CharacterAnimation _animScript;
    [HideInInspector] public bool damageTook = false;
    [HideInInspector] private ParticleSystem _footStep;

    public void Initialize(float speed, float maxVelocity)
    {
        this.speed = speed;
        this.maxVelocity = maxVelocity;
        _animScript = GetComponentInChildren<CharacterAnimation>();
        _rb = GetComponent<Rigidbody>();
        _gfx = _animScript.gameObject;
        _footStep = transform.Find("Particles").transform.Find("Footstep").GetComponent<ParticleSystem>();
    }

    public void StartMovement(Rigidbody _rb, CharacterAnimation _animScript, GameObject _gfx, Vector3 target)
    {
        Vector3 targetDirection = TargetToDirection(target);
        Rotate(_gfx, targetDirection);
        VelocityChange(_rb, targetDirection);
        _animScript.Move(true);
        _footStep.Play();
    }

    public void Rotate(GameObject _gfx, Vector3 target)
    {
        _gfx.transform.rotation = Quaternion.LookRotation(target);
    }

    public Vector3 TargetToDirection(Vector3 target)
    {
        Vector3 targetDirection = (target - transform.position).normalized;
        return targetDirection;
    }

    public void VelocityChange(Rigidbody _rb, Vector3 targetDirection)
    {
        Vector3 movementVelocity = new Vector3(targetDirection.x, 0, targetDirection.z) * speed * Time.fixedDeltaTime;
        _rb.velocity += movementVelocity;
        if (_rb.velocity.magnitude > maxVelocity)
            _rb.velocity = _rb.velocity.normalized * maxVelocity;
    }

    public static bool DistanceBiggerThanValue(Vector3 a, Vector3 b, float maxDistance)
    {
        float distance = Vector3.Distance(a, b);
        return (distance > maxDistance);
    }

}
