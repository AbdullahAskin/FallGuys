using System.Collections;
using UnityEngine;

public interface IDamageable
{
    IEnumerator DamageTaking(Vector3 hitPosition);
}
