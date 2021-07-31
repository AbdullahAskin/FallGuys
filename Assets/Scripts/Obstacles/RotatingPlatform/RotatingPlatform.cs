using UnityEngine;

public class RotatingPlatform : Rotate
{
    private float _force;
    private void Start()
    {
        Initialize(40, Vector3.forward); //Dondugu yonun tersine kuvvet uyguladigi icin bu sekilde
        _force = -1000 * rotateSign;
    }

    void FixedUpdate()
    {
        RotateObject();
    }
    private void OnCollisionStay(Collision collision)
    {
        var objectRigidbody = collision.transform.GetComponent<Rigidbody>();
        objectRigidbody.AddForce(Vector3.back * _force * Time.deltaTime);
    }

    public Vector3 TargetToDirection(Vector3 forceLocation, Vector3 target)
    {
        var targetDirection = (target - forceLocation).normalized;
        return targetDirection;
    }
}