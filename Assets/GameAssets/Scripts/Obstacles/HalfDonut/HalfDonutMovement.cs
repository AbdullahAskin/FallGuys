using System.Collections;
using UnityEngine;

public class HalfDonutMovement : HorizontalMovement
{
    private const float MINMoveTime = 1f;
    private const float MAXMoveTime = 4f;

    private void Start()
    {
        Initialize(15f);
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement() 
    {
        while (Move.DistanceBiggerThanValue(backwardTargetTrans.position, transform.position, 0.001f))
        {
            Movement(backwardTargetTrans, Speed / 6);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(Random.Range(MINMoveTime, MAXMoveTime));

        while (Move.DistanceBiggerThanValue(forwardTargetTrans.position, transform.position, 0.001f))
        {
            Movement(forwardTargetTrans, Speed);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(StartMovement());
    }
}
