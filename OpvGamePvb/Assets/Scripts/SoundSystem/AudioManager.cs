using UnityEngine;
using Random = UnityEngine.Random;

namespace SoundSystem
{
   public enum SfxTypes 
   {
      CannonShot = 0,
   }

   public enum AmbienceTypes
   {
      Wind = 0,
   }

   public class AudioManager : MonoBehaviour
   {
      public static AudioManager Instance;
      private void Awake()
      {
         Instance = this;
      }
      
      [SerializeField] private AudioSource _ambience;
      [SerializeField] private AudioSource _sfx;
      
      // it is very important that these arrays align with the enumerators
      [SerializeField] private SoundClipVariantGroup[] _sfxVariantGroups; 
      [SerializeField] private SoundClipVariantGroup[] _ambienceVariantGroups;

      private int _musicIndex;
      private bool _musicIsPlaying;
      private bool _ambienceIsPlaying;

      [System.Serializable]
      private class SoundClipVariantGroup
      {
         [SerializeField] private string _name; //I WISH I KNEW THIS WAS POSSIBLE WHEN I DID THE CUSTOM WAVE SYSTEM... set name of cluster here
         public AudioClip[] _sounds; //set clips here
         [Range(0, 1)]
         public float[] _volumes; // set different levels of volume for variation here
      }

      public void PlayRandomSfxVariant(SfxTypes index)
      {
         var shortcutSounds = _sfxVariantGroups[(int) index]._sounds;
         var shortcutVolume = _sfxVariantGroups[(int)index]._volumes;
         _sfx.PlayOneShot( 
            shortcutSounds[Random.Range(0, shortcutSounds.Length - 1)],
            Random.Range(shortcutVolume[0], shortcutVolume[shortcutVolume.Length - 1])
            );
      }
      
      public void PlayRandomAmbienceVariant(AmbienceTypes index)
      {
         var shortcutSounds = _ambienceVariantGroups[(int) index]._sounds;
         var shortcutVolume = _ambienceVariantGroups[(int)index]._volumes;
         _ambience.PlayOneShot( 
            shortcutSounds[Random.Range(0, shortcutSounds.Length - 1)],
            Random.Range(shortcutVolume[0], shortcutVolume[shortcutVolume.Length - 1])
         );
      }
   }
}