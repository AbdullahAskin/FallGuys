using UnityEngine;

public class PaintableObjectRotate : MonoBehaviour
{
    private Vector3 rotateVector;
    public float rotateSpeed;

    void Start()
    {
        rotateVector = new Vector3(0, rotateSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        RotateObject();
    }

    public void RotateObject()
    {
        transform.Rotate(rotateVector, Space.Self);
    }
}
