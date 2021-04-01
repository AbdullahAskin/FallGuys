using UnityEngine;

public interface IMove
{
    Vector3 TargetToDirection(Vector3 target);
    void VelocityChange(Rigidbody _rb, Vector3 targetDirection);
    void Rotate(GameObject _gfx, Vector3 target);
    void StartMovement(Rigidbody _rb, CharacterAnimation _animScript, GameObject _gfx, Vector3 target);
}
