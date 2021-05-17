using UnityEngine;

namespace WaveSystem.Waves
{
    public abstract class CustomWave : MonoBehaviour
    {
        [SerializeField, Tooltip("first slot is enemy type, second is seconds to wait before spawning")]
        protected Vector2[] _enemyAndSpawnTimer;
    }
}
