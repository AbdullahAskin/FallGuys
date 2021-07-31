using UnityEngine;

public interface IMove
{
    Vector3 TargetToDirection(Vector3 target);
    void VelocityChange(Rigidbody playerRb, Vector3 targetDirection);
    void Rotate(GameObject playerGfx, Vector3 target);
    void StartMovement(Rigidbody charRb, CharacterAnimation animScr, GameObject charGfx, Vector3 target);
}
