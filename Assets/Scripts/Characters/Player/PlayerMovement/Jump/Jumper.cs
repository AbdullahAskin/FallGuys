using System;
using System.Collections;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float maxJumpForce = 800f;
    [HideInInspector] public float minJumpAmount = 0.4f;

    private float jumpPrepOffset = 0.3f;
    private float jumpLandingOffset = 0.233f;
    private ParticleSystem _jumpStartingPart, _jumpLandingPart;

    private void Start()
    {
        _jumpStartingPart = transform.Find("Particles").Find("JumpStarting").GetComponent<ParticleSystem>();
        _jumpLandingPart = transform.Find("Particles").Find("JumpLanding").GetComponent<ParticleSystem>();
    }

    public void JumpControl(Rigidbody _rb, CharacterAnimation _animScript, GameObject _gfx, Vector3 target, bool onGround, float jumpAmount)
    {
        JumpMaker(_rb, _animScript, _gfx, target, onGround, jumpAmount);
    }

    private void JumpMaker(Rigidbody _rb, CharacterAnimation _animScript, GameObject _gfx, Vector3 target, bool onGround, float jumpAmount)
    {
        if (jumpAmount > 0 && jumpAmount > minJumpAmount && onGround)
        {
            StartCoroutine(Jump(_rb, _animScript, _gfx, target, jumpAmount));
        }
    }

    private IEnumerator Jump(Rigidbody _rb, CharacterAnimation _animScript, GameObject _gfx, Vector3 target, float jumpAmount)
    {
        _animScript.Jump(true);
        yield return new WaitForSeconds(jumpPrepOffset);
        Vector3 targetDirection = LookingDirection(_gfx.transform.eulerAngles.y + 90);
        Vector3 forceDirection = new Vector3(targetDirection.x * maxJumpForce, maxJumpForce, targetDirection.z * maxJumpForce);
        _rb.AddForce(forceDirection * jumpAmount);
        _jumpStartingPart.Play();
        yield return new WaitForSeconds(0.3f);
        while (!OnGroundControl(_gfx))
            yield return new WaitForFixedUpdate();

        _animScript.Jump(false);
        _jumpLandingPart.Play();
        yield return new WaitForSeconds(jumpLandingOffset);
    }

    public bool OnGroundControl(GameObject _gameObj)
    {
        bool grounded = Physics.Raycast(_gameObj.transform.position + Vector3.up * .2f, Vector3.down, .43f, 1 << LayerMask.NameToLayer("Ground"));
        if (!grounded)
            grounded = Physics.Raycast(_gameObj.transform.position + Vector3.up * .2f, Vector3.down, .43f, 1 << LayerMask.NameToLayer("Ignore Raycast"));
        if (!grounded && _gameObj.transform.parent.position.y < -3f)
            RespawnPoint.RespawnCharacter(_gameObj);
        return grounded;
    }

    public Vector3 LookingDirection(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(-(float)Math.Cos(radian), 0, (float)Math.Sin(radian));
        return direction;
    }


}
