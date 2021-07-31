using System;
using System.Collections;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private const float MaxJumpForce = 800f;
    [HideInInspector] public float minJumpAmount = 0.4f;

    private const float JumpPrepOffset = 0.3f;
    private const float JumpLandingOffset = 0.233f;
    private ParticleSystem _jumpBeginParticleSys, _jumpLandingParticleSys;

    private void Start()
    {
        _jumpBeginParticleSys = transform.Find("Particles").Find("JumpStarting").GetComponent<ParticleSystem>();
        _jumpLandingParticleSys = transform.Find("Particles").Find("JumpLanding").GetComponent<ParticleSystem>();
    }

    public void JumpControl(Rigidbody charRb, CharacterAnimation charAnimScr, GameObject charGfx, Vector3 target,
        float jumpAmount)
    {
        JumpMaker(charRb, charAnimScr, charGfx, target, jumpAmount);
    }

    private void JumpMaker(Rigidbody charRb, CharacterAnimation animScr, GameObject charGfx, Vector3 target,
        float jumpAmount)
    {
        if (jumpAmount > 0 && jumpAmount > minJumpAmount)
            StartCoroutine(Jump(charRb, animScr, charGfx, target, jumpAmount));
    }

    private IEnumerator Jump(Rigidbody charRb, CharacterAnimation animScr, GameObject charGfx, Vector3 target,
        float jumpAmount)
    {
        animScr.Jump(true);
        yield return new WaitForSeconds(JumpPrepOffset);
        var targetDirection = LookingDirection(charGfx.transform.eulerAngles.y + 90);
        var forceDirection =
            new Vector3(targetDirection.x * MaxJumpForce, MaxJumpForce, targetDirection.z * MaxJumpForce);
        charRb.AddForce(forceDirection * jumpAmount);
        _jumpBeginParticleSys.Play();
        yield return new WaitForSeconds(0.3f);
        while (!OnGroundControl(charRb))
            yield return new WaitForFixedUpdate();

        animScr.Jump(false);
        _jumpLandingParticleSys.Play();
        yield return new WaitForSeconds(JumpLandingOffset);
    }

    public static bool OnGroundControl(Rigidbody charRb)
    {
        var grounded = Physics.Raycast(charRb.transform.position + Vector3.up * .2f, Vector3.down, .43f,
            1 << LayerMask.NameToLayer("Ground"));
        if (!grounded)
            grounded = Physics.Raycast(charRb.transform.position + Vector3.up * .2f, Vector3.down, .43f,
                1 << LayerMask.NameToLayer("Ignore Raycast"));
        if (!grounded && charRb.transform.parent.position.y < -3f)
            RespawnPoint.RespawnCharacter(charRb);
        return grounded;
    }

    private static Vector3 LookingDirection(float angle)
    {
        var radian = angle * Mathf.Deg2Rad;
        var direction = new Vector3(-(float) Math.Cos(radian), 0, (float) Math.Sin(radian));
        return direction;
    }
}