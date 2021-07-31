using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : Move, IMovement
{
    private Jumper _jumperScript;
    public Image jumpBarImg;
    private float _elapsedTime, _jumpAmount;
    private Vector3 _lastTarget;

    private void Start()
    {
        Initialize(20f, 2.2f);
        _jumperScript = GetComponent<Jumper>();
    }

    public void Movement(Vector3 target) //Hareketle ilgili seylerin yapildigi yer.
    {
        var onGround = Jumper.OnGroundControl(charRb);
        if (!onGround || damageTook) // Yerde degil  
            EndMovement();
        else if (target.magnitude == 0) // Yerde dokunma yok
        {
            _jumperScript.JumpControl(charRb, animScr, charGfx, _lastTarget, _jumpAmount);
            EndMovement();
        }
        else if (!animScr.GetJump()) // Yerde dokunma var ve ziplama yok
        {
            StartMovement(charRb, animScr, charGfx, target);
            _jumpAmount = JumpCalculater(ref _elapsedTime, target);
        }
    }

    private void EndMovement()
    {
        animScr.Move(false);
        JumpReset(ref _jumpAmount, ref _elapsedTime);
    }

    private void JumpReset(ref float jumpAmount, ref float elapsedTime)
    {
        jumpAmount = 0;
        elapsedTime = 0;
        ResetJumpBar();
    }

    private float JumpCalculater(ref float elapsedTime, Vector3 target)
    {
        var jumpAmount = Mathf.Lerp(0, 1, elapsedTime / 2);
        elapsedTime += Time.deltaTime;
        _lastTarget = target;
        FillJumpBar(elapsedTime / 2);
        return jumpAmount;
    }

    private void FillJumpBar(float fillAmount)
    {
        if (fillAmount > _jumperScript.minJumpAmount && jumpBarImg.color == Color.yellow)
            jumpBarImg.color = Color.green;
        jumpBarImg.fillAmount = fillAmount;
    }


    private void ResetJumpBar()
    {
        jumpBarImg.color = Color.yellow;
        jumpBarImg.fillAmount = 0;
    }
}