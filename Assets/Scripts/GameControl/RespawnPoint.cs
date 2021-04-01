using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private static Vector3 respawnPointPos;

    private void OnEnable()
    {
        respawnPointPos = transform.position;
    }

    public static void RespawnCharacter(GameObject _gfx)
    {
        Rigidbody _rb = _gfx.GetComponentInParent<Rigidbody>();
        _rb.MovePosition(respawnPointPos);
        _gfx.transform.rotation = Quaternion.Euler(0, 90, 0);
        //Dogma particle effect 
    }

}
