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
    /// (WIP) returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)
    /// </summary>
    /// <param name="closestToPoint">get the area closest to point</param>
    /// <returns>returns a vector 4 of bounds (x MIN, x MAX, z MIN, z MAX)</returns>
    public Vector4 GetBoundsOfNearestArea(Vector3 closestToPoint)
    {

        return Vector4.zero;
    }
}
