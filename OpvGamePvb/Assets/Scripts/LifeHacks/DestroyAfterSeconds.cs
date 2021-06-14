using System.Collections;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _secondsToDestruction;
    [SerializeField] private GameObject _target;
    
    void Start()
    {
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(_secondsToDestruction);
        Destroy(_target);
    }
}
