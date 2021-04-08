using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PointInput : MonoBehaviour
{
    private int _PointMultiplier;
    [SerializeField]
    private ScoreKeeping scoreKeeper;
    // Start is called before the first frame update


    // Update is called once per frame


    public void ObtainPoints(int pointsToAdd)
    {
      scoreKeeper.UpdateScore(pointsToAdd *=_PointMultiplier);  
    }

    public void AddMultuplier(int newMultiplier)
    {
        if (_PointMultiplier > 20)
        {
            _PointMultiplier += newMultiplier;
        }
       
    }

    public void ResetMultiplier()
    {
        _PointMultiplier = 0;
    }
}
