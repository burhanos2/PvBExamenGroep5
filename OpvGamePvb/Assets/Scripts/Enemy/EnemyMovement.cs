using UnityEngine;
using WaveSystem;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 _position;
    [SerializeField]
    private float speed = 0;
    private Vector3 _newPosition;
    [SerializeField]
    private float _minimalDivergentX = 0;
    [SerializeField]
    private float _minimalDivergentZ;
    [SerializeField]
    private float _maximumDivergentX = 0;
    [SerializeField]
    private float _maximumDivergentZ;
    [SerializeField]
    private GameObject _player;

    private MeshRenderer _playerMesh;
    [SerializeField]
    private float _distanceHeldToPlayer = 0;

    private bool _ismoving = false;
    private float _waitingTimer = 0;
    [SerializeField]
    private float _waitingTime = 0;

    [SerializeField] private float _angle = 20;
    private Transform _posHelper;

    private Vector4 _myPlayBounds;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        _myPlayBounds = WavesManager.Instance.GetCurrentPlayArea;

        _minimalDivergentX = _myPlayBounds.x;
        _maximumDivergentX = _myPlayBounds.y;
        _minimalDivergentX = _myPlayBounds.z;
        _maximumDivergentZ = _myPlayBounds.w;
        //minimalDivergentX = 
        _player = GameObject.Find("BoatModol");
       _playerMesh =  _player.GetComponent<MeshRenderer>();
        
       _posHelper = new GameObject().transform;
       
        _position = transform.position;
        CalculateNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position != _newPosition &&  _ismoving == true)
        {   

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, step);
            
        }
        else
        {   
            
            _waitingTimer += Time.deltaTime;
            
            _ismoving = false;
        }

        if (_waitingTimer >= _waitingTime)
        {
            
            _waitingTimer = 0;
            CalculateNewPosition();
        }
        
        
    }

    public void CalculateNewPosition()
    {   
        _newPosition = new Vector3(Random.Range(_minimalDivergentX, _maximumDivergentX),0,Random.Range(_minimalDivergentZ, _maximumDivergentZ));
        float distance = Vector3.Distance(_newPosition,_playerMesh.bounds.center);
        _position = transform.position;
        
        if(distance <= _distanceHeldToPlayer)
        {
            _posHelper.position = transform.position;
            _posHelper.RotateAround(_player.transform.position, Vector3.up, _angle);
            _newPosition = _posHelper.position;
        }
        else
        {
            
            _ismoving = true;
        }



    }

    public bool getMovementStatus()
    {
        return _ismoving;
    }

    
}
