using UnityEngine;
using UnityEngine.UI;
using WaveSystem;

public class DisplayLiveEnemyAmount : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    private void Update()
    {
        _text.text = "Radar: " + WavesManager.Instance._currentLiveEnemies.Count;
    }
}
