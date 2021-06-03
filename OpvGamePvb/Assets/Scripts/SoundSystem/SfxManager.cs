using UnityEngine;
using WaveSystem;

namespace SoundSystem
{
    public class SfxManager : MonoBehaviour
    {
        [SerializeField] private WavesManager _wavesManager; //.Instance is null? temporary
        private void Start()
        {
            Control.OnAttackKeys += PlayShootSound;
            Control.OnSwitchKey += PlaySwitchSound;
            _wavesManager.OnEnemyDeath += PlayShipDestructSound;
        } 
        private void PlayShootSound()
        {
            AudioManager.Instance.PlayRandomSfxVariant(SfxTypes.CannonShot);
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
