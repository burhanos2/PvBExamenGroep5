using System.Collections;
using UnityEngine;
using WaveSystem;
using UnityEngine.UI;

public class PointInput : MonoBehaviour
{
    
    private int _pointMultiplier = 1;
    [SerializeField]
    private ScoreKeeping _scoreKeeper;
    [SerializeField]
    private WavesManager _wavesManager;

    [SerializeField] 
    private Text _multiplier;

    private readonly Color32 _darkRed = new Color32(200, 10, 31, 255);
    private readonly Color32 _darkGreen = new Color32(7, 178, 0, 255);

    public static PointInput Instance;

    void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        _wavesManager.OnEnemyDeath += ObtainPoints;
        UpdateMultiplier();
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
            UpdateMultiplier();
        }
            
        
        
    }

    public void ResetMultiplier()
    {
        if (_pointMultiplier != 1)
            _pointMultiplier--;
        StartCoroutine(FlashRed(1));
        UpdateMultiplier();
    }

    private void UpdateMultiplier()
    {
        _multiplier.text = "x" + _pointMultiplier;
    }

    private IEnumerator FlashRed(float waitTime)
    {
        _multiplier.color = _darkRed;
        yield return new WaitForSeconds(waitTime);
        _multiplier.color = _darkGreen;
    }
}
