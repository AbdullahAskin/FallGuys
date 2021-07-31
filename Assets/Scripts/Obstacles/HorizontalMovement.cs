using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    protected Rigidbody obstacleRb;
    public Transform forwardTargetTrans, backwardTargetTrans;
    protected float Speed;

    protected void Initialize(float Speed)
    {
        obstacleRb = GetComponent<Rigidbody>();
        this.Speed = Speed;
    }

    protected void Movement(Transform target, float speed)
    {
        obstacleRb.MovePosition(Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime));
    }

    protected void OnCollisionEnter(Collision collision)
    {
        var damageableScript = collision.transform.GetComponent<IDamageable>();
        StartCoroutine(damageableScript.DamageTaking(collision.contacts[0].point));
    }
}