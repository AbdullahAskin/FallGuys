using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private float cameraPassingOffset = 1.35f;
    public GameObject _paintMaterialsGameObj;
    public ParticleSystem _confettiParticle;

    private void Start()
    {
        _confettiParticle = transform.Find("Particles").Find("Confetti").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mainCharacter"))
            StartCoroutine(PaintStart(other.gameObject));
        else if (other.CompareTag("bot"))
            RunOver(other.gameObject);
    }

    IEnumerator PaintStart(GameObject _charGameObj)
    {
        RunOver(_charGameObj);
        _confettiParticle.Play();
        yield return new WaitForSeconds(4f);
        GameObject.Find("CameraPosition").GetComponent<CameraMovement>().PaintingOn();
        _confettiParticle.Stop();
        _paintMaterialsGameObj.SetActive(true);
        yield return new WaitForSeconds(cameraPassingOffset);
        DisableRaceObjects();
        GameObject.FindGameObjectWithTag("canvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("canvas").transform.GetChild(1).gameObject.SetActive(false);
        _charGameObj.GetComponent<Painting>().Initialize();
    }

    private void RunOver(GameObject _charGameObj)
    {
        _charGameObj.GetComponentInChildren<CharacterAnimation>().Dance(true);
        _charGameObj.GetComponent<Character>().runOver = true;
    }

    private void DisableRaceObjects()
    {
        GameObject.Find("Bots").SetActive(false);
        GameObject.Find("RainParticle").SetActive(false);
    }
}
