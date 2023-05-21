using System.Collections;
using UnityEngine;

public class HorizontalObstacleMovement : HorizontalMovement
{
    private Vector3 rotateSpeed;
    private void Start()
    {
        Initialize(2f);
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement() // Daha sonra kisalt.
    {
        while (Move.DistanceBiggerThanValue(backwardTargetTrans.position, transform.position, 0.001f))
        {
            Movement(backwardTargetTrans, Speed);
            yield return new WaitForFixedUpdate();
        }
        RotateObject(180);
        yield return new WaitForSeconds(.5f);

        while (Move.DistanceBiggerThanValue(forwardTargetTrans.position, transform.position, 0.001f))
        {
            Movement(forwardTargetTrans, Speed);
            yield return new WaitForFixedUpdate();
        }

        RotateObject(180);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(StartMovement());
    }

    private void RotateObject(float angle)
    {
        var deltaRotation = Quaternion.Euler(Vector3.up * angle);
        obstacleRb.MoveRotation(obstacleRb.rotation * deltaRotation);
    }
}
