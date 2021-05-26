using UnityEngine;

namespace SoundSystem
{
    public class SfxManager : MonoBehaviour
    {
        private void Start()
        {
            Control.OnAttackKeys += PlayShootSound;
        }

        private void PlayShootSound()
        {
            AudioManager.Instance.PlayRandomSfxVariant(SfxTypes.CannonShot);
        }
    }
}
