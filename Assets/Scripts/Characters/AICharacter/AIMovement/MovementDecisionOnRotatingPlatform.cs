using UnityEngine;

public class MovementDecisionOnRotatingPlatform : MonoBehaviour
{
    private Vector3 defMovAddition;

    private void Start()
    {
        defMovAddition = new Vector3(1f, 0, 1f);
    }

    public Vector3 MovementDecision(Vector3 currentTarget)
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z), Vector3.down, out RaycastHit hit, .43f))
        {
            if (hit.collider.CompareTag("rotatingPlatform"))
            {
                return OnRotatingPlatform(hit, currentTarget);
            }
        }
        return currentTarget;
    }

    private Vector3 OnRotatingPlatform(RaycastHit hit, Vector3 currentTarget)
    {
        float rotateSign = hit.transform.GetComponent<RotatingPlatform>().rotateSign;
        Vector3 tempTarget = transform.position + defMovAddition;
        tempTarget.y = currentTarget.y;
        tempTarget.z = -(rotateSign);
        return tempTarget;
    }
}
