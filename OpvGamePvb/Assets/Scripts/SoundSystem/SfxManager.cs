using UnityEngine;
using WaveSystem;

namespace SoundSystem
{
    public class SfxManager : MonoBehaviour
    { 
        
        private void Start()
        {
            Control.Instance.OnSwitchKey += PlaySwitchSound;
            WavesManager.Instance.OnEnemyDeath += PlayShipDestructSound;
        }
        
        private void PlaySwitchSound()
        {
            AudioManager.Instance.PlayRandomSfxVariant(SfxTypes.SwitchSound);
        }

        private void PlayShipDestructSound(int points, GameObject enemy)
        {
            AudioManager.Instance.PlayRandomSfxVariant(SfxTypes.ShipDestruct);
        }
    }
}
