using UnityEngine;
using System.Collections;

public class BallCamera : MonoBehaviour
{
    Transform Target;
    public Vector3 m_TargetRotation;
    public Vector3 m_PositionOffset;

    void Awake( )
    {
        PlayerEventManager.OnPlayerSpawn.AddListener( OnPlayerSpawn );
    }

    void OnPlayerSpawn( GameObject _newPlayer )
    {
        Target = _newPlayer.transform;
    }

    void FixedUpdate( )
    {
        if(Target != null )
        {
            //Move camera to desired location
            transform.position = Vector3.Lerp( transform.position, Target.position + m_PositionOffset, 0.2f);

            //Set cam rotation
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( m_TargetRotation ), 0.2f );
        }
    }
}
