using UnityEngine;

public class DecisionByTouch : MonoBehaviour, IDecisionMove, IDecisionPaint
{
    public Camera _paintCamera;
    private float minMoveDistance = 0.2f;
    public Vector3 MovementDecision()
    {
        Vector3 target = Vector3.zero;
        //if (Input.touchCount == 0)
        //    return target;

        if (!Input.GetMouseButton(0))
            return target;

        //Vector3 touchPos = Input.GetTouch(0).position;
        Vector3 touchPos = Input.mousePosition;

        if (TouchHit(touchPos, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("platform") || hit.collider.CompareTag("rotatingPlatform"))
            {
                target = hit.point;
                if (!Move.DistanceBiggerThanValue(target, transform.position, minMoveDistance))
                    target = Vector3.zero;
            }
        }
        return target;
    }

    public Vector3 PaintDecision()
    {
        Vector3 target = Vector3.zero;
        //if (Input.touchCount == 0)
        //    return target;        
        if (!Input.GetMouseButton(0))
            return target;

        //Vector2 touchPos = Input.GetTouch(0).position;
        Vector2 touchPos = Input.mousePosition;
        if (TouchHit(touchPos, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("paintableObject"))
            {
                Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                target = new Vector2(pixelUV.x - _paintCamera.orthographicSize, pixelUV.y - _paintCamera.orthographicSize);
            }
        }
        return target;
    }

    private bool TouchHit(Vector3 touchPos, out RaycastHit hit)
    {
        Ray touchRay = Camera.main.ScreenPointToRay(touchPos);
        return Physics.Raycast(touchRay, out hit);
    }
}
