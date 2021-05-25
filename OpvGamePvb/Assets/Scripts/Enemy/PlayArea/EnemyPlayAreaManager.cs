using UnityEngine;

public class EnemyPlayAreaManager : MonoBehaviour
{
    public GameObject[] GetPlayAreaObjects { get; private set; }

    private void Start()
    {
        GetPlayAreaObjects = GameObject.FindGameObjectsWithTag("EnemyPlayArea");
    }

    /// <summary>
    /// returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)
    /// </summary>
    /// <param name="indexToGrab">index in array</param>
    /// <returns>returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)</returns>
    public Vector4 GetBoundsOfArea(int indexToGrab)
    {
        if (indexToGrab > GetPlayAreaObjects.Length - 1 || indexToGrab < 0)
        {
            Debug.LogError("index in GetBoundsOfArea(int indexToGrab) does not exist");
            return Vector4.zero;
        }
        var objRenderer = GetPlayAreaObjects[indexToGrab].GetComponent<Renderer>().bounds;
        
        var funk = new Vector4(
            objRenderer.min.x,
            objRenderer.max.x,
            objRenderer.min.z,
            objRenderer.max.z
            );
        
        return funk;
    }
    
    /// <summary>
    /// returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)
    /// </summary>
    /// <param name="closestToPoint">get the area closest to point</param>
    /// <returns>returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)</returns>
    public Vector4 GetBoundsOfNearestArea(Vector3 closestToPoint)
    {
        GameObject closestObj = null;
        var minDist = Mathf.Infinity;
        var searchFor = closestToPoint;
        foreach (var obj in GetPlayAreaObjects)
        {
            var dist = Vector3.Distance(obj.transform.position, searchFor);
            if (!(dist < minDist)) continue;
            closestObj = obj;
            minDist = dist;
        }

        if (closestObj == null)
        {
            Debug.LogError("closestObject == null");
            return Vector4.zero;
        }
        var closestBounds = closestObj.GetComponent<Renderer>().bounds;
        var fin = new Vector4(closestBounds.min.x, closestBounds.max.x, closestBounds.min.z, closestBounds.max.z);
        return fin;
    }

    /// <summary>
    /// returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX) IF the object is tagged as a play area
    /// </summary>
    /// <param name="playAreaObj">get the area closest to point</param>
    /// <returns>returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)</returns>
    public Vector4 GetBoundsIfPlayArea(GameObject playAreaObj)
    {
        if (playAreaObj.CompareTag("EnemyPlayArea"))
        {
            var bounds = playAreaObj.GetComponent<Renderer>().bounds;
            var fin = new Vector4(bounds.min.x, bounds.max.x, bounds.min.z, bounds.max.z);
            return fin;
        }

        Debug.LogError("Object is not tagged as play area");
        return Vector4.zero;
    }
}
