using UnityEngine;

public class PaintableObjectRotate : MonoBehaviour
{
    private Vector3 _rotateVector;
    public float rotateSpeed;

    private void Start()
    {
        _rotateVector = new Vector3(0, rotateSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        transform.Rotate(_rotateVector, Space.Self);
    }
}
