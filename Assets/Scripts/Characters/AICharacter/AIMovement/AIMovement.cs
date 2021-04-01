using UnityEngine;

public class AIMovement : Move, IMovement
{
    private void Start()
    {
        Initialize(20f, 2.2f);
    }

    public void Movement(Vector3 target) //Hareketle ilgili seylerin yapildigi yer.
    {
        bool onGround = OnGroundControl(_gfx);
        if (!onGround || damageTook || target.magnitude == 0)
            _animScript.Move(false);
        else
            StartMovement(_rb, _animScript, _gfx, target);
    }

    public bool OnGroundControl(GameObject _gameObj)
    {
        bool grounded = Physics.Raycast(_gameObj.transform.position + Vector3.up * .2f, Vector3.down, .43f, 1 << LayerMask.NameToLayer("Ground"));

        if (!grounded && _gameObj.transform.parent.position.y < -3f)
            RespawnPoint.RespawnCharacter(_gameObj);
        return grounded;
    }

}
