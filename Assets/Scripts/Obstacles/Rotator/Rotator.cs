using UnityEngine;

public class Rotator : Rotate
{
    private GameObject _backPredict;
    private void Start()
    {
        Initialize(75f, Vector3.up);
        _backPredict = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        RotateObject();
        PredictObstacleControl();
    }


    private void PredictObstacleControl()
    {
        var currentYAngle = transform.eulerAngles.y;
        if (currentYAngle > 270)
            _backPredict.SetActive(false);
        else if (currentYAngle > 90)
            _backPredict.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageableScript = collision.transform.GetComponent<IDamageable>();
        StartCoroutine(damageableScript.DamageTaking(collision.contacts[0].point));
    }
}
