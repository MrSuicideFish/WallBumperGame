using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    void OnDrawGizmos( )
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere( transform.position, 0.5f );
    }
}
