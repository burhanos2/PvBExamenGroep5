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
    private int maximumDivergentX = 0;
    [SerializeField]
    private int minimalDivergentZ;
    [SerializeField]
    private int maximumDivergentZ;
    [SerializeField]
    private GameObject Player;

    private bool ismoving = false;
    private float waitingTimer = 0;
    [SerializeField]
    private float WaitingTime = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("BoatModol");
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
        position = transform.position;
        newPosition = new Vector3(Random.Range(minimalDivergentX, maximumDivergentX),0,Random.Range(minimalDivergentZ, maximumDivergentZ));
        
        
        if((newPosition.x <= Player.GetComponent<MeshRenderer>().bounds.size.x /2f )&&(newPosition.x >= Player.GetComponent<MeshRenderer>().bounds.size.x *2f) && ((newPosition.z <= Player.GetComponent<MeshRenderer>().bounds.size.z / 2f)&&(newPosition.x >= Player.GetComponent<MeshRenderer>().bounds.size.x *2f)))
        {
            CalculateNewPosition();
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
