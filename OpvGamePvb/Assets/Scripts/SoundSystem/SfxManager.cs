using UnityEngine;
using WaveSystem;

namespace SoundSystem
{
    public class SfxManager : MonoBehaviour
    {
        [SerializeField] private WavesManager _wavesManager; //.Instance is null? temporary
        private void Start()
        {
            Control.OnSwitchKey += PlaySwitchSound;
            _wavesManager.OnEnemyDeath += PlayShipDestructSound;
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
