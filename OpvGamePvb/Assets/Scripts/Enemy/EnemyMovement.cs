using UnityEngine;

using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 position;
    [SerializeField]
    private float speed = 0;
    private Vector3 newPosition;
    [SerializeField]
    private int minimalDivergentX = 0;
    [SerializeField]
    private int minimalDivergentZ;
    [SerializeField]
    private int maximumDivergentX = 0;
    [SerializeField]
    private int maximumDivergentZ;
    [SerializeField]
    private GameObject Player;

    private MeshRenderer playerMesh;
    [SerializeField]
    private float _DistanceHeldToPlayer = 0;

    private bool ismoving = false;
    private float waitingTimer = 0;
    [SerializeField]
    private float WaitingTime = 0;

    [SerializeField] private float _angle = 20;
    private Transform _posHelper;

    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("BoatModol");
       playerMesh =  Player.GetComponent<MeshRenderer>();
        
       _posHelper = new GameObject().transform;
       
        position = transform.position;
        CalculateNewPosition();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != newPosition &&  ismoving == true)
        {   

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            
        }
        else
        {   
            
            waitingTimer += Time.deltaTime;
            
            ismoving = false;
        }

        if (waitingTimer >= WaitingTime)
        {
            
            waitingTimer = 0;
            CalculateNewPosition();
        }
        
        
    }

    public void CalculateNewPosition()
    {   
        newPosition = new Vector3(Random.Range(minimalDivergentX, maximumDivergentX),0,Random.Range(minimalDivergentZ, maximumDivergentZ));
        float distance = Vector3.Distance(newPosition,playerMesh.bounds.center);
        position = transform.position;
        
        if(distance <= _DistanceHeldToPlayer)
        {
            _posHelper.position = transform.position;
            _posHelper.RotateAround(Player.transform.position, Vector3.up, _angle);
            newPosition = _posHelper.position;
        }
        else
        {
            
            ismoving = true;
        }



    }

    public bool getMovementStatus()
    {
        return ismoving;
    }

    
}
