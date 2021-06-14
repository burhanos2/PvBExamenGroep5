using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//throw this script on camera

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _randomMin = -0.4f;
    [SerializeField]
    private float _randomMax = 0.4f;

    private Transform _camTransform;

    private void Start()
    {
        _camTransform = gameObject.transform;
    }

    public IEnumerator CamShake (float duration, float magnitude)
    {
        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var random = magnitude * Random.Range(_randomMin, _randomMax) * 0.1f;
            
            _camTransform.localPosition += -_camTransform.right * random;
            _camTransform.localPosition += -_camTransform.up * random;
            
            elapsed += Time.deltaTime;

            yield return null;
        }
        _camTransform.position = transform.parent.position; //set pos back to CameraPos
    }

}
