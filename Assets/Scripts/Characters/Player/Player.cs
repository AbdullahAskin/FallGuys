using UnityEngine;

public class Player : Character, ICanMove, ICanPaint
{
    private Painting _paintingScript;
    private IDecisionPaint _decisionPaintScript;

    void Start()
    {
        _paintingScript = GetComponent<Painting>();
        _decisionPaintScript = GetComponent<IDecisionPaint>();
        Initialize();
    }

    void FixedUpdate()
    {
        if (!runOver)
            Movement();
        else
            Paint();
    }

    public void Paint()
    {
        Vector3 target = _decisionPaintScript.PaintDecision();
        _paintingScript.Paint(target);
    }
}
