using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private static Vector3 _respawnPointPos;

    private void OnEnable()
    {
        _respawnPointPos = transform.position;
    }

    public static void RespawnCharacter(Rigidbody charRb)
    {
        charRb.MovePosition(_respawnPointPos);
        charRb.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}