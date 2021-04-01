
using UnityEngine;
using UnityEngine.AI;

public class DecisionByAlgorithm : MonoBehaviour, AIDecisionMove, IDecisionMove
{
    private MovementDecisionOnRotatingPlatform _movDecOnRotPlatScript;
    private NavMeshPath _path;
    private int currentIndex = 1;
    public Vector3 finishTarget;


    public void Start()
    {
        _movDecOnRotPlatScript = GetComponent<MovementDecisionOnRotatingPlatform>();
        _path = new NavMeshPath();
        finishTarget += Vector3.forward * transform.position.z;
        InvokeRepeating(nameof(CalculatePath), 0f, .2f);
    }


    public Vector3 MovementDecision()
    {
        return FindTarget();
    }

    public Vector3 FindTarget()
    {
        if (_path.corners.Length == 0 || currentIndex >= _path.corners.Length)
            return Vector3.zero;
        else if (!Move.DistanceBiggerThanValue(transform.position, _path.corners[currentIndex], .3f))
            currentIndex++;

        Vector3 currentTarget = _movDecOnRotPlatScript.MovementDecision(_path.corners[currentIndex]);
        return currentTarget;
    }



    public void CalculatePath()
    {
        NavMeshPath _detectedPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, finishTarget, NavMesh.AllAreas, _detectedPath);
        if (_detectedPath.corners.Length != 0)
        {
            currentIndex = 1;
            _path = _detectedPath;
        }
    }


}
