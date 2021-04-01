using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Move(bool move)
    {
        _animator.SetBool("move", move);
    }

    public void Jump(bool jump)
    {
        _animator.SetBool("jump", jump);
    }

    public bool GetJump()
    {
        return _animator.GetBool("jump");
    }

    public void Dance(bool dance)
    {
        _animator.SetBool("jump", false);
        _animator.SetBool("move", false);
        _animator.SetBool("dance", dance);
    }
}
