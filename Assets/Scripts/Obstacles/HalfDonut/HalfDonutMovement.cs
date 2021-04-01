using System.Collections;
using UnityEngine;

public class HalfDonutMovement : HorizontalMovement
{
    private readonly float minMoveTime = 1f, maxMoveTime = 4f;

    private void Start()
    {
        Initialize(15f);
        StartCoroutine(StartMovement());
    }

    public IEnumerator StartMovement() // Daha sonra kisalt.
    {
        while (Move.DistanceBiggerThanValue(_backwardTarget.position, transform.position, 0.001f))
        {
            Movement(_backwardTarget, speed / 6);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));

        while (Move.DistanceBiggerThanValue(_forwardTarget.position, transform.position, 0.001f))
        {
            Movement(_forwardTarget, speed);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(StartMovement());
    }
}
