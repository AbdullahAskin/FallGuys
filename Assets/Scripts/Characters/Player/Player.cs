using UnityEngine;

public class Player : Character, ICanMove, ICanPaint
{
    private Painting _paintingScript;
    private IDecisionPaint _decisionPaintScript;

    private void Start()
    {
        _paintingScript = GetComponent<Painting>();
        _decisionPaintScript = GetComponent<IDecisionPaint>();
        Initialize();
    }

    private void FixedUpdate()
    {
        if (!runOver)
            Movement();
        else
            Paint();
    }

    public void Paint()
    {
        var target = _decisionPaintScript.PaintDecision();
        _paintingScript.Paint(target);
    }
}
