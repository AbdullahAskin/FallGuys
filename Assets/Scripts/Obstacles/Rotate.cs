using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody _obstacleRb;
    public float rotateSign;//Ters tarafa donebilme olanagi tanir.
    [HideInInspector] private Vector3 _rotateVector;


    protected void Initialize(float rotateSpeed, Vector3 rotateDirection)
    {
        _rotateVector = rotateDirection * rotateSpeed * rotateSign;
        _obstacleRb = GetComponent<Rigidbody>();
    }

    protected void RotateObject()
    {
        Quaternion deltaRotation = Quaternion.Euler(_rotateVector * Time.fixedDeltaTime);
        _obstacleRb.MoveRotation(_obstacleRb.rotation * deltaRotation);
    }

}
