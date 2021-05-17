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
}
