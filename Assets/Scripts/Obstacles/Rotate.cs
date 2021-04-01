using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody _rb;
    public float rotateSign;//Ters tarafa donebilme olanagi tanir.
    [HideInInspector] protected Vector3 rotateVector;


    public void Initialize(float rotateSpeed, Vector3 rotateDirection)
    {
        rotateVector = rotateDirection * rotateSpeed * rotateSign;
        _rb = GetComponent<Rigidbody>();
    }

    public void RotateObject()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotateVector * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    }

}
