using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : Move, IMovement
{
    private Jumper _jumperScript;
    public Image _jumpBar;
    private float elapsedTime, jumpAmount;
    private Vector3 lastTarget;

    private void Start()
    {
        Initialize(20f, 2.2f);
        _jumperScript = GetComponent<Jumper>();
    }

    public void Movement(Vector3 target) //Hareketle ilgili seylerin yapildigi yer.
    {
        bool onGround = _jumperScript.OnGroundControl(_gfx);
        if (!onGround || damageTook) // Yerde degil  
            EndMovement();
        else if (target.magnitude == 0)  // Yerde dokunma yok
        {
            _jumperScript.JumpControl(_rb, _animScript, _gfx, lastTarget, onGround, jumpAmount);
            EndMovement();
        }
        else if (!_animScript.GetJump())// Yerde dokunma var ve ziplama yok
        {
            StartMovement(_rb, _animScript, _gfx, target);
            jumpAmount = JumpCalculater(ref elapsedTime, target);
        }
    }

    private void EndMovement()
    {
        _animScript.Move(false);
        JumpReset(ref jumpAmount, ref elapsedTime);
    }

    public void JumpReset(ref float jumpAmount, ref float elapsedTime)
    {
        jumpAmount = 0;
        elapsedTime = 0;
        ResetJumpBar();
    }

    public float JumpCalculater(ref float elapsedTime, Vector3 target)
    {
        float jumpAmount = Mathf.Lerp(0, 1, elapsedTime / 2);
        elapsedTime += Time.deltaTime;
        lastTarget = target;
        FillJumpBar(elapsedTime / 2);
        return jumpAmount;
    }

    private void FillJumpBar(float fillAmount)
    {
        if (fillAmount > _jumperScript.minJumpAmount && _jumpBar.color == Color.yellow)
            _jumpBar.color = Color.green;
        _jumpBar.fillAmount = fillAmount;
    }


    private void ResetJumpBar()
    {
        _jumpBar.color = Color.yellow;
        _jumpBar.fillAmount = 0;
    }

}
