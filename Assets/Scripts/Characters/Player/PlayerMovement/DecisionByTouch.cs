using System;
using UnityEngine;

public class DecisionByTouch : MonoBehaviour, IDecisionMove, IDecisionPaint
{
    public Camera paintCam;
    private Camera _standardCam;
    private const float MinMoveDistance = 0.2f;

    private void Start()
    {
        _standardCam = Camera.main;
    }

    public Vector3 MovementDecision()
    {
        var target = Vector3.zero;
        if (Input.touchCount == 0)
            return target;

        Vector3 touchPos = Input.GetTouch(0).position;

        if (!IsTouchHit(touchPos, out var hit)) return target;
        if (!hit.collider.CompareTag("platform") && !hit.collider.CompareTag("rotatingPlatform")) return target;
        target = hit.point;
        if (!Move.DistanceBiggerThanValue(target, transform.position, MinMoveDistance))
            target = Vector3.zero;
        return target;
    }

    public Vector3 PaintDecision()
    {
        var target = Vector3.zero;
        if (Input.touchCount == 0)
            return target;

        //Vector2 touchPos = Input.GetTouch(0).position;
        Vector2 touchPos = Input.mousePosition;
        if (!IsTouchHit(touchPos, out RaycastHit hit)) return target;
        if (!hit.transform.CompareTag("paintableObject")) return target;
        var pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
        target = new Vector2(pixelUV.x - paintCam.orthographicSize, pixelUV.y - paintCam.orthographicSize);
        return target;
    }

    private bool IsTouchHit(Vector3 touchPos, out RaycastHit hit)
    {
        var touchRay = _standardCam.ScreenPointToRay(touchPos);
        return Physics.Raycast(touchRay, out hit);
    }
}