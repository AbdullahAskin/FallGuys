using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private const float CameraPassingOffset = 1.35f;
    public GameObject paintMaterialsGo;
    public ParticleSystem confettiParticleSys;

    private void Start()
    {
        confettiParticleSys = transform.Find("Particles").Find("Confetti").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mainCharacter"))
            StartCoroutine(PaintStart(other.gameObject));
        else if (other.CompareTag("bot"))
            RunOver(other.gameObject);
    }

    private IEnumerator PaintStart(GameObject charGo)
    {
        RunOver(charGo);
        confettiParticleSys.Play();
        yield return new WaitForSeconds(4f);
        GameObject.Find("CameraPosition").GetComponent<CameraMovement>().PaintingOn();
        confettiParticleSys.Stop();
        paintMaterialsGo.SetActive(true);
        yield return new WaitForSeconds(CameraPassingOffset);
        DisableRaceObjects();
        GameObject.FindGameObjectWithTag("canvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("canvas").transform.GetChild(1).gameObject.SetActive(false);
        charGo.GetComponent<Painting>().Initialize();
    }
    

    private void RunOver(GameObject charGo)
    {
        charGo.GetComponentInChildren<CharacterAnimation>().Dance(true);
        charGo.GetComponent<Character>().runOver = true;
    }

    private void DisableRaceObjects()
    {
        GameObject.Find("Bots").SetActive(false);
        GameObject.Find("RainParticle").SetActive(false);
    }
}