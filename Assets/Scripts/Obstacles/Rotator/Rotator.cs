using UnityEngine;

public class Rotator : Rotate
{
    private GameObject _backPredict;
    private void Start()
    {
        Initialize(75f, Vector3.up);
        _backPredict = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        RotateObject();
        PredictObstacleControl();
    }


    void PredictObstacleControl()
    {
        float currentYAngle = transform.eulerAngles.y;
        if (currentYAngle > 270)
            _backPredict.SetActive(false);
        else if (currentYAngle > 90)
            _backPredict.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable _damageableScript = collision.transform.GetComponent<IDamageable>();
        StartCoroutine(_damageableScript.DamageTaking(collision.contacts[0].point));
    }
}
