using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    public ParticleSystem damageParticleSys;
    public Text characterText;
    public string userName = "defaultName";
    
    private IMovement _movementScr;
    private Move _commonMoveScr;
    private IDecisionMove _decisionMoveScript;



    [HideInInspector] public bool runOver = false;

    protected void Initialize()
    {
        _movementScr = GetComponent<IMovement>();
        _commonMoveScr = GetComponent<Move>();
        _decisionMoveScript = GetComponent<IDecisionMove>();
        SetName(userName);
        LeaderBoard.AddChar(this);
    }


    public IEnumerator DamageTaking(Vector3 hitPosition)
    {
        _commonMoveScr.damageTook = true;
        var clonedDamageParticleSys = Instantiate(damageParticleSys);
        clonedDamageParticleSys.transform.position = hitPosition;
        Destroy(clonedDamageParticleSys, clonedDamageParticleSys.main.duration);
        yield return new WaitForSeconds(.5f);
        _commonMoveScr.damageTook = false;
    }

    public void Movement()
    {
        var target = _decisionMoveScript.MovementDecision();
        _movementScr.Movement(target);
    }

    private void SetName(string userName)
    {
        var userNameText = transform.Find("VerticalCanvas").GetChild(0).GetComponentInChildren<Text>();
        userNameText.text = userName;
    }
}