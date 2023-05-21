using UnityEngine;
using UnityEngine.AI;

public class DecisionByAlgorithm : MonoBehaviour, AIDecisionMove, IDecisionMove
{
    private MovementDecisionOnRotatingPlatform _movementDecisionOnPlatformScr;
    private NavMeshPath _path;
    private int _iPath = 1;
    public Transform finishTargetTrans;


    public void Start()
    {
        _movementDecisionOnPlatformScr = GetComponent<MovementDecisionOnRotatingPlatform>();
        _path = new NavMeshPath();
        InvokeRepeating(nameof(CalculatePath), 0f, .2f);
    }


    public Vector3 MovementDecision()
    {
        return FindTarget();
    }

    public Vector3 FindTarget()
    {
        if (_path.corners.Length == 0 || _iPath + 2 >= _path.corners.Length) return Vector3.zero;

        if (!Move.DistanceBiggerThanValue(transform.position, _path.corners[_iPath], .3f)) _iPath++;

        var currentTarget = _movementDecisionOnPlatformScr.MovementDecision(_path.corners[_iPath]);
        return currentTarget;
    }


    public void CalculatePath()
    {
        var detectedPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, finishTargetTrans.position, NavMesh.AllAreas, detectedPath);
        if (detectedPath.corners.Length == 0) return;
        _iPath = 1;
        _path = detectedPath;
    }
}