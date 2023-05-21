using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private const float CameraPassingOffset = 1.35f;

    public GameObject paintMaterialsGo;
    public ParticleSystem confettiParticleSys;
    private Painting _paintingScr;
    private CameraMovement _cameraMovementScr;
    private CharacterAnimation _charAnimScr;
    private Character _charScr;

    private GameObject _rainParticleGo;
    private GameObject _botsParentGo;
    private Transform _canvasTrans;

    private void Start()
    {
        _paintingScr = FindObjectOfType<Painting>();
        _charAnimScr = _paintingScr.GetComponentInChildren<CharacterAnimation>();
        _charScr = _paintingScr.GetComponent<Character>();
        _cameraMovementScr = FindObjectOfType<CameraMovement>();

        _rainParticleGo = GameObject.Find("RainParticle");
        _botsParentGo = GameObject.Find("Bots");
        _canvasTrans = GameObject.FindGameObjectWithTag("canvas").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mainCharacter"))
            StartCoroutine(PaintStart(other.gameObject));
        else if (other.CompareTag("bot"))
            SetCharPropToRunOver(other.gameObject);
    }

    private IEnumerator PaintStart(GameObject charGo)
    {
        SetCharPropToRunOver(charGo);
        confettiParticleSys.Play();
        yield return new WaitForSeconds(4f);
        _cameraMovementScr.PaintingOn();
        confettiParticleSys.Stop();
        paintMaterialsGo.SetActive(true);
        yield return new WaitForSeconds(CameraPassingOffset);
        DisableRaceObjects();
        _canvasTrans.GetChild(0).gameObject.SetActive(true);
        _canvasTrans.GetChild(1).gameObject.SetActive(false);
        _paintingScr.Initialize();
    }


    private void SetCharPropToRunOver(GameObject charGo)
    {
        _charAnimScr.Dance(true);
        _charScr.runOver = true;
    }

    private void DisableRaceObjects()
    {
        _botsParentGo.SetActive(false);
        _rainParticleGo.SetActive(false);
    }
}