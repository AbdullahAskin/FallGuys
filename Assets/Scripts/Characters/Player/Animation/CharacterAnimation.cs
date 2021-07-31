using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int MoveAnim = Animator.StringToHash("move");
    private static readonly int JumpAnim = Animator.StringToHash("jump");
    private static readonly int DanceAnim = Animator.StringToHash("dance");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Move(bool move)
    {
        _animator.SetBool(MoveAnim, move);
    }

    public void Jump(bool jump)
    {
        _animator.SetBool(JumpAnim, jump);
    }

    public bool GetJump()
    {
        return _animator.GetBool(JumpAnim);
    }

    public void Dance(bool dance)
    {
        _animator.SetBool(JumpAnim, false);
        _animator.SetBool(MoveAnim, false);
        _animator.SetBool(DanceAnim, dance);
    }
}
