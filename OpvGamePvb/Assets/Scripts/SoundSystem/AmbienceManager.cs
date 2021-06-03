using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoundSystem
{
   public class AmbienceManager : MonoBehaviour
   {
      [SerializeField] private float[] _waitTimings = { 5f };
      [SerializeField] private float _timeBetweenCallingClips = 2f;
      private float _waitTimer;
      private readonly bool[] _ambienceChannels = {false, false}; // allows two ambience channels
      

      private void Update()
      {
         if (_waitTimer > 0)
         {
            _waitTimer -= Time.deltaTime;
            return;
         }
         if (!_ambienceChannels[0] || !_ambienceChannels[1])
         {
            WaitRandomTime(!_ambienceChannels[0] ? 0 : 1);
         }
      }

      private void WaitRandomTime(int indToPlayOn)
      {
         _ambienceChannels[indToPlayOn] = true;

         AudioManager.Instance.PlayRandomAmbienceVariant(AmbienceTypes.Wind);
         var coroutine = WaitForChannel(_waitTimings[Random.Range(0, _waitTimings.Length - 1)], indToPlayOn);
         StartCoroutine(coroutine);
         
         _waitTimer = _timeBetweenCallingClips;
      }

      private IEnumerator WaitForChannel(float waitTime, int channelIndex)
      {
         yield return new WaitForSeconds(waitTime);
         _ambienceChannels[channelIndex] = false;
      }
   }
}
