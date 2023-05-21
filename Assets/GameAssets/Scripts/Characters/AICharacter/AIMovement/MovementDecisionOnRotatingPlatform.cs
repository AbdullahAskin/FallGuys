using System.Collections.Generic;
using UnityEngine;

public class MovementDecisionOnRotatingPlatform : MonoBehaviour
{
    private Vector3 _defaultMovementAddition;
    private List<RotatingPlatform> _rotatingPlatformsScr;

    private void Start()
    {
        _defaultMovementAddition = new Vector3(1f, 0, 1f);

        _rotatingPlatformsScr = new List<RotatingPlatform>();
        foreach (var rotatingPlatformGo in GameObject.FindGameObjectsWithTag("rotatingPlatform"))
        {
            _rotatingPlatformsScr.Add(rotatingPlatformGo.GetComponent<RotatingPlatform>());
        }
    }

    public Vector3 MovementDecision(Vector3 currentTarget)
    {
        if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z),
            Vector3.down, out RaycastHit hit, .43f)) return currentTarget;
        return hit.collider.CompareTag("rotatingPlatform") ? OnRotatingPlatform(hit, currentTarget) : currentTarget;
    }

    private Vector3 OnRotatingPlatform(RaycastHit hit, Vector3 currentTarget)
    {
        var rotateSign =
            _rotatingPlatformsScr.Find(rotatingPlatformScr => rotatingPlatformScr.transform == hit.transform)
                .rotateSign;
        var target = transform.position + _defaultMovementAddition;
        target.y = currentTarget.y;
        target.z = -(rotateSign);
        return target;
    }
}