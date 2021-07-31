using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform paintCameraTrans;

    public void PaintingOn()
    {
        StartCoroutine(MoveToTargetPosition(transform.position, paintCameraTrans.position, transform));
        StartCoroutine(RotateToTargetAngle(transform.eulerAngles, paintCameraTrans.eulerAngles, transform));
    }

    private IEnumerator MoveToTargetPosition(Vector3 startPosition, Vector3 endPosition, Transform movingObject)
    {
        var elapsedTime = 0f;
        while (elapsedTime < 2f)
        {   
            movingObject.position = Vector3.Slerp(startPosition, endPosition, elapsedTime / 2);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator RotateToTargetAngle(Vector3 startAngle, Vector3 endAngle, Transform rotatingObjTrans)
    {
        var elapsedTime = 0f;
        while (elapsedTime / 2 < 1f)
        {
            rotatingObjTrans.eulerAngles = Vector3.Slerp(startAngle, endAngle, elapsedTime / 2);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

}
