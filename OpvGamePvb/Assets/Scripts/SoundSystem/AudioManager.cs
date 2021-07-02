using UnityEngine;
using Random = UnityEngine.Random;

namespace SoundSystem
{
   public enum SfxTypes 
   {
      CannonShot = 0,
      SwitchSound = 1,
      ShipDestruct = 2,
      CannonMoveLoop = 3,
      Splash = 4,
      PlayerDamaged = 5,
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
      [SerializeField] private AudioSource _sfxLoop;
      
      // it is very important that these arrays align with the enumerators
      [SerializeField] private SoundClipVariantGroup[] _sfxVariantGroups; 
      [SerializeField] private SoundClipVariantGroup[] _ambienceVariantGroups;

      private int _musicIndex;
      private bool _musicIsPlaying;
      private bool _ambienceIsPlaying;

      [System.Serializable]
      private class SoundClipVariantGroup
      {
         [SerializeField] private string _name; //set name of cluster here
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
      
      public void PlaySfxLoopStart(SfxTypes indexOfStart)
      {
         var shortcutSoundStart = _sfxVariantGroups[(int) indexOfStart]._sounds[0];
         var shortcutVolumeStart = _sfxVariantGroups[(int)indexOfStart]._volumes[0];
         _sfxLoop.PlayOneShot(shortcutSoundStart, shortcutVolumeStart); //play start clip on down
      }
      public void PlaySfxLooped(SfxTypes indexOfLoop) //for sounds that only have one sound and volume and a loop, made for gun rotation
      {
         var shortcutSoundLoop = _sfxVariantGroups[(int) indexOfLoop]._sounds[0];
         var shortcutVolumeLoop = _sfxVariantGroups[(int)indexOfLoop]._volumes[0];
         
         _sfxLoop.clip = shortcutSoundLoop;
         _sfxLoop.volume = shortcutVolumeLoop;
         _sfxLoop.Play(); // then play the Loop clip on loop after it is done
      }
      
      public void StopSfxLoop()
      {
         _sfxLoop.Stop();
      }
   }
}
