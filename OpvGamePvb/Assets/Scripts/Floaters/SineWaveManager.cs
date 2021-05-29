using UnityEngine;

// this singleton sets up a sine wave for the floaters to all grab from.
namespace Floaters
{
   public class SineWaveManager : MonoBehaviour
   {
      public static SineWaveManager Instance;
      private void Awake()
      {
         Instance = this;
      }

      [SerializeField] private float _amplitude = 0.5f;
      [SerializeField] private float _length = 1f;
      [SerializeField] private float _speed = 3f;
      private float _offset;

      private void Update()
      {
         _offset += Time.deltaTime * _speed;
      }
      // y = a sin x
      public float GetWaveHeight(float x)
      {
         return _amplitude * Mathf.Sin(x / _length + _offset);
      }
   }
}
