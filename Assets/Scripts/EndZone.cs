using UnityEngine;
using UnityEditor;
using System.Collections;

public enum EndZoneShape
{
    Cube,
    Sphere
}

public class EndZone : MonoBehaviour
{
    string m_MeshPath = "Gizmos/Gizmo_FinishLine.obj";

    public EndZoneShape EndZoneType = EndZoneShape.Cube;
    public float ZoneSize = 1.0f;

    void OnDrawGizmos( )
    {
        if ( ZoneSize < 0 ) ZoneSize = 0;

        Gizmos.color = Color.red;
        Mesh gizmo = AssetDatabase.LoadAssetAtPath<Mesh>( "Assets/Editor/Gizmo_FinishLine.obj" );
        Gizmos.DrawMesh( gizmo, transform.position );

        Gizmos.color = Color.magenta;
        switch ( EndZoneType )
        {
            case EndZoneShape.Cube:
                Gizmos.DrawWireCube( transform.position, new Vector3( ZoneSize, 0.3f, ZoneSize ) );
                break;
            case EndZoneShape.Sphere:
                Gizmos.DrawWireSphere( transform.position, ZoneSize );
                break;
        }
    }
}