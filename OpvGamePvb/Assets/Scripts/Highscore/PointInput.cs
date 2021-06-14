using UnityEngine;
using WaveSystem;

public class PointInput : MonoBehaviour
{
    
    private int _pointMultiplier = 1;
    [SerializeField]
    private ScoreKeeping _scoreKeeper;
    [SerializeField]
    private WavesManager _wavesManager;

    public static PointInput Instance;

    void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        _wavesManager.OnEnemyDeath += ObtainPoints;
        // playerBullet.OnShotHit += AddMultiplier;
        // playerBullet.OnShotMiss += ResetMultiplier;
    }
    
    public void ObtainPoints(int pointsToAdd, GameObject gameObject)
    {
        _scoreKeeper.UpdateScore(pointsToAdd *=_pointMultiplier);  
    }

    public void AddMultiplier(int newMultiplier)
    {
        if (_pointMultiplier < 20)
        {
            _pointMultiplier += newMultiplier;
        
        }
            
        
        
    }

    public void ResetMultiplier()
    {
        if (_pointMultiplier != 1)
            _pointMultiplier--;
        
    }
}
