using UnityEngine;

public class KeepScale : MonoBehaviour
{
    [SerializeField] private Vector3 _scale;
    private void Update()
    {
        transform.localScale = _scale;
    }
}
