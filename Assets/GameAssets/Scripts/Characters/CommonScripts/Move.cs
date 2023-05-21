using UnityEngine;

public abstract class Move : MonoBehaviour, IMove
{
    [HideInInspector] public float speed, maxVelocity;
    [HideInInspector] public GameObject charGfx;
    [HideInInspector] public Rigidbody charRb;
    [HideInInspector] public CharacterAnimation animScr;
    [HideInInspector] public bool damageTook = false;
    private ParticleSystem _footStepParticleSys;

    protected void Initialize(float speed, float maxVelocity)
    {
        this.speed = speed;
        this.maxVelocity = maxVelocity;
        animScr = GetComponentInChildren<CharacterAnimation>();
        charRb = GetComponent<Rigidbody>();
        charGfx = animScr.gameObject;
        _footStepParticleSys = transform.Find("Particles").transform.Find("Footstep").GetComponent<ParticleSystem>();
    }

    public void StartMovement(Rigidbody charRb, CharacterAnimation animScr, GameObject charGfx, Vector3 target)
    {
        var targetDirection = TargetToDirection(target);
        Rotate(charGfx, targetDirection);
        VelocityChange(charRb, targetDirection);
        animScr.Move(true);
        _footStepParticleSys.Play();
    }

    public void Rotate(GameObject playerGfx, Vector3 target)
    {
        playerGfx.transform.rotation = Quaternion.LookRotation(target);
    }

    public Vector3 TargetToDirection(Vector3 target)
    {
        Vector3 targetDirection = (target - transform.position).normalized;
        return targetDirection;
    }

    public void VelocityChange(Rigidbody playerRb, Vector3 targetDirection)
    {
        targetDirection.y = 0;
        var movementVelocity = (speed * Time.fixedDeltaTime) * targetDirection;
        playerRb.velocity += movementVelocity;
        if (playerRb.velocity.magnitude > maxVelocity)
            playerRb.velocity = maxVelocity * playerRb.velocity.normalized;
    }

    public static bool DistanceBiggerThanValue(Vector3 a, Vector3 b, float maxDistance)
    {
        var distance = Vector3.Distance(a, b);
        return (distance > maxDistance);
    }
}