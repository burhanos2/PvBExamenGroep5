using System.Collections.Generic;
using UnityEngine;

public class ConeActiveHandler : MonoBehaviour
{   
    [SerializeField]
    private List<GameObject> _cones;


    [Range(0, 2)] private int _activePosition = 0;
    
    private void Start()
    {
        ChangeActivity(0);
    }


    private void ChangeActivity(int newActive)
    {
        switch (newActive)
        {
            case 0: 
                this.gameObject.SetActive(false);
                
                break;
            case 1:
                this.gameObject.SetActive(true);
                _cones[0].SetActive(true);
                _cones[1].SetActive(false);
                break;
            case 2:
                this.gameObject.SetActive(true);
                _cones[0].SetActive(false);
                _cones[1].SetActive(true);
                break;
        }
        
    }
}
