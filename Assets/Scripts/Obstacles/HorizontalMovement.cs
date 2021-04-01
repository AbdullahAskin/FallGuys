using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    protected Rigidbody _rb;
    public Transform _forwardTarget, _backwardTarget;
    protected float speed;

    protected void Initialize(float speed)
    {
        _rb = GetComponent<Rigidbody>();
        this.speed = speed;
    }

    public void Movement(Transform _target, float speed)
    {
        _rb.MovePosition(Vector3.MoveTowards(transform.position, _target.position, speed * Time.fixedDeltaTime));
    }

    protected void OnCollisionEnter(Collision collision)
    {
        IDamageable _damageableScript = collision.transform.GetComponent<IDamageable>();
        StartCoroutine(_damageableScript.DamageTaking(collision.contacts[0].point));
    }
}
