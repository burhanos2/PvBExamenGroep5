using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private bool isActive = false;
    private Vector3 objectToMoveTo;
    [SerializeField]private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, objectToMoveTo, step);
        }

        if (transform.position == objectToMoveTo)
        {
            isActive = false;
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }

    public bool GetActive()
    {
        return isActive;
    }

    public void SetObjectToMoveTo(Vector3 position)
    {
        objectToMoveTo = position;
    }
}
