using UnityEngine;

namespace WaveSystem.Waves
{
    public abstract class CustomWave : MonoBehaviour
    {
        [Tooltip("counting from 0, which play area in the index to pick")]
        public int _playAreaToSpawnIndex;
        [Tooltip("first slot is enemy type, second is seconds to wait before spawning")]
        public Vector2[] _enemyAndSpawnTimer;
    }
}
