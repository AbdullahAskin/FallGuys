using UnityEngine;

public class RotatingPlatform : Rotate
{
    private float applyingForce;
    private void Start()
    {
        Initialize(40, Vector3.forward); //Dondugu yonun tersine kuvvet uyguladigi icin bu sekilde
        applyingForce = -1000 * rotateSign;
    }

    void FixedUpdate()
    {
        RotateObject();
    }
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody _objectRigidbody = collision.transform.GetComponent<Rigidbody>();
        _objectRigidbody.AddForce(Vector3.back * applyingForce * Time.deltaTime);
    }

    public Vector3 TargetToDirection(Vector3 forceLocation, Vector3 target)
    {
        Vector3 targetDirection = (target - forceLocation).normalized;
        return targetDirection;
    }
}