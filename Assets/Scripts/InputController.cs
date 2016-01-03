using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public Rigidbody m_Player;
    public float m_MovementSpeed = 1;

    Vector3 TiltVector;

    void Start( )
    {
        TiltVector = Vector3.zero;
    }

    void FixedUpdate( )
    {
#if UNITY_EDITOR && UNITY_ANDROID
        if ( Input.GetKey( KeyCode.UpArrow ) ) { TiltVector += Vector3.right; }
        if ( Input.GetKey( KeyCode.DownArrow ) ) { TiltVector += Vector3.left; }
        if ( Input.GetKey( KeyCode.LeftArrow) ) { TiltVector += Vector3.forward; }
        if ( Input.GetKey( KeyCode.RightArrow) ) { TiltVector += Vector3.back; }
#endif

#if !UNITY_EDITOR && UNITY_ANDROID
        TiltVector = Input.gyro.rotationRate;
#endif

        //Move player
        m_Player.AddTorque( TiltVector * m_MovementSpeed );

        TiltVector = Vector3.zero;
    }
}