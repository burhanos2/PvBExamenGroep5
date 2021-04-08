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
    private int minimalDivergent = 0;
    [SerializeField]
    private int maximumDivergent = 0;
    [SerializeField]
    private GameObject Player;

    private bool ismoving = false;
    private float waitingTime = 0;

    
    // Start is called before the first frame update
    void Start()
    {
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
            waitingTime += Time.deltaTime;
            ismoving = false;
        }

        if (waitingTime == 1f)
        {
            waitingTime = 0;
            CalculateNewPosition();
        }
        
        
    }

    public void CalculateNewPosition()
    {   
        position = transform.position;
        newPosition = new Vector3(Random.Range(minimalDivergent, maximumDivergent),0,Random.Range(minimalDivergent, maximumDivergent));
        
        
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
