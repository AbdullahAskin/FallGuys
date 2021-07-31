using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    private IMovement _movementScr;
    private IDecisionMove _decisionMoveScript;
    private GameObject _damageParticle;

    public string userName = "defaultName";

    [HideInInspector] public bool runOver = false;

    protected void Initialize()
    {
        _movementScr = GetComponent<IMovement>();
        _decisionMoveScript = GetComponent<IDecisionMove>();
        _damageParticle = Resources.Load<GameObject>("Prefabs/Particles/Damage");
        SetName(userName);
        LeaderBoard.AddPlayer(transform);
    }


    public void Movement()
    {
        var target = _decisionMoveScript.MovementDecision();
        _movementScr.Movement(target);
    }

    public IEnumerator DamageTaking(Vector3 hitPosition)
    {
        var moveScr = GetComponent<Move>();
        moveScr.damageTook = true;
        var damagePartGameObj = Instantiate(_damageParticle);
        damagePartGameObj.transform.position = hitPosition;
        Destroy(damagePartGameObj, damagePartGameObj.GetComponent<ParticleSystem>().main.duration);
        yield return new WaitForSeconds(.5f);
        moveScr.damageTook = false;
    }

    private void SetName(string userName)
    {
        var userNameText = transform.Find("VerticalCanvas").GetChild(0).GetComponentInChildren<Text>();
        userNameText.text = userName;
    }
}
