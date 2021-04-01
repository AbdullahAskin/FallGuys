using System.Collections;
using UnityEngine;

public class HorizontalObstacleMovement : HorizontalMovement
{
    private Vector3 rotateAmount;
    private void Start()
    {
        Initialize(2f);
        StartCoroutine(StartMovement());
    }

    public IEnumerator StartMovement() // Daha sonra kisalt.
    {
        while (Move.DistanceBiggerThanValue(_backwardTarget.position, transform.position, 0.001f))
        {
            Movement(_backwardTarget, speed);
            yield return new WaitForFixedUpdate();
        }
        RotateObject(180);
        yield return new WaitForSeconds(.5f);

        while (Move.DistanceBiggerThanValue(_forwardTarget.position, transform.position, 0.001f))
        {
            Movement(_forwardTarget, speed);
            yield return new WaitForFixedUpdate();
        }

        RotateObject(180);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(StartMovement());
    }

    public void RotateObject(float angle)
    {
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * angle);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    }
}
