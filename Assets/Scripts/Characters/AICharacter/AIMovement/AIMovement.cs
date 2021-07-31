using UnityEngine;

public class AIMovement : Move, IMovement
{
    private void Start()
    {
        Initialize(20f, 2.2f);
    }

    public void Movement(Vector3 target) //Hareketle ilgili seylerin yapildigi yer.
    {
        var onGround = OnGroundControl();
        if (!onGround || damageTook || target.magnitude == 0)
            animScr.Move(false);
        else
            StartMovement(charRb, animScr, charGfx, target);
    }

    private bool OnGroundControl()
    {
        var grounded = Physics.Raycast(charGfx.transform.position + Vector3.up * .2f, Vector3.down, .43f,
            1 << LayerMask.NameToLayer("Ground"));

        if (!grounded && charGfx.transform.parent.position.y < -3f)
            RespawnPoint.RespawnCharacter(charRb);
        return grounded;
    }
}