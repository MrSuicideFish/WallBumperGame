using UnityEngine;
using System.Collections;

public class BallCamera : MonoBehaviour
{
    private Transform Target;
    private Vector3 m_LastPlayerDir;
    private InputController m_PlayerInput;

    public Vector3 m_TargetRotation;
    public Vector3 m_PositionOffset;

    private float m_TiltPower = 0.0f;
    public float m_MaxTiltPower = 1.0f;

    void Awake( )
    {
        PlayerEventManager.OnPlayerSpawn.AddListener( OnPlayerSpawn );
        m_PlayerInput = GetComponent<InputController>( );
    }

    void OnPlayerSpawn( GameObject _newPlayer )
    {
        Target = _newPlayer.transform;
    }

    void Update( )
    {
        if(Target != null )
        {
            //Move camera to desired location
            transform.position = Vector3.Lerp( transform.position, Target.position + m_PositionOffset, 0.05f );

            //calc rotation based on tilt direction
            if(m_PlayerInput.m_LastTiltVector.z != 0 || m_PlayerInput.m_LastTiltVector.x != 0 )
            {
                m_TiltPower += Time.fixedDeltaTime * ( m_MaxTiltPower / Mathf.PI );

                //limit
                if ( m_TiltPower > m_MaxTiltPower )
                    m_TiltPower = m_MaxTiltPower;
            }

            //Look at ball
            var lookRot = Quaternion.LookRotation( ( new Vector3(Target.position.x * 1.1f, Target.position.y, Target.position.z * 0.9f) - transform.position), transform.up );
            transform.rotation = lookRot;

        }
    }
}