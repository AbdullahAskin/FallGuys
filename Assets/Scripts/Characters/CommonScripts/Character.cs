using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    protected IMovement _movementScript;
    private IDecisionMove _decisionMoveScript;
    private GameObject _damageParticle;

    public string userName = "defaultName";

    [HideInInspector] public bool runOver = false;

    public void Initialize()
    {
        _movementScript = GetComponent<IMovement>();
        _decisionMoveScript = GetComponent<IDecisionMove>();
        _damageParticle = Resources.Load<GameObject>("Prefabs/Particles/Damage");
        SetName(userName);
        LeaderBoard.AddPlayer(transform);
    }


    public void Movement()
    {
        Vector3 target = _decisionMoveScript.MovementDecision();
        _movementScript.Movement(target);
    }

    public IEnumerator DamageTaking(Vector3 hitPosition)
    {
        Move _moveScript = GetComponent<Move>();
        _moveScript.damageTook = true;
        GameObject _damagePartGameObj = Instantiate(_damageParticle);
        _damagePartGameObj.transform.position = hitPosition;
        Destroy(_damagePartGameObj, _damagePartGameObj.GetComponent<ParticleSystem>().main.duration);
        yield return new WaitForSeconds(.5f);
        _moveScript.damageTook = false;
    }

    private void SetName(string userName)
    {
        Text _userNameText = transform.Find("VerticalCanvas").GetChild(0).GetComponentInChildren<Text>();
        _userNameText.text = userName;
    }
}
