using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public Rigidbody m_Player;
    public float m_MovementSpeed = 1;
    public float m_JumpHeight = 20.0f;
    public bool m_MobileMode = false;
    Vector3 TiltVector, RestTiltVector;

    bool m_IsJumping = false;

    void OnGUI( )
    {
        GUIStyle style = new GUIStyle( );
        int w = Screen.width, h = Screen.height;
        Rect rect = new Rect( 0, 0, w, h * 2 / 100 );

        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 32;
        style.normal.textColor = new Color( 1.0f, 1.0f, 1.0f, 1.0f );

        GUI.Label( new Rect( 0, 80, 256, 64 ), "Gyro Gravity: " + Input.acceleration.ToString( ), style );
        GUI.Label( new Rect( 0, 120, 256, 64 ), "Gyro Gravity (Calibrated): " + ( Input.acceleration - RestTiltVector ).ToString( ), style );

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format( "{0:0.0} ms ({1:0.} fps)", msec, fps );
        GUI.Label( rect, text, style );
    }

    void Awake( )
    {
        PlayerEventManager.OnPlayerSpawn.AddListener( OnPlayerSpawn );
    }

    void Start( )
    {
        TiltVector = Vector3.zero;
        PlayerEventManager.OnPlayerCollision.AddListener( OnPlayerCollision );
    }

    float deltaTime = 0.0f;
    void Update( )
    {
        if ( m_Player == null ) return;

        deltaTime += ( Time.deltaTime - deltaTime ) * 0.1f;

    #if UNITY_EDITOR_WIN
        if ( !m_MobileMode )
                {
                    //Up: x/-x, Right: z/-z 
                    if ( Input.GetKey( KeyCode.UpArrow ) ) { TiltVector += Vector3.forward; }
                    if ( Input.GetKey( KeyCode.DownArrow ) ) { TiltVector += Vector3.back; }
                    if ( Input.GetKey( KeyCode.LeftArrow ) ) { TiltVector += Vector3.left; }
                    if ( Input.GetKey( KeyCode.RightArrow ) ) { TiltVector += Vector3.right; }
                }

                if ( !m_IsJumping )
                {
                    if ( Input.GetKeyDown( KeyCode.Space ) )
                    {
                        Jump( );
                    }
                }
#endif
        //(Pitch,Yaw,Roll)
        //Up: (0,0,-1)
        //Down (0,0,1)

        //roll: (1/-1,0,0)

#if !UNITY_EDITOR && UNITY_ANDROID
                if(m_MobileMode)
                {
                    TiltVector = Input.acceleration;

                    if ( !m_IsJumping )
                    {
                        if(Input.GetTouch(0).tapCount == 1 )
                        {
                            Jump( );
                        }
                    }
                }
#endif

        //Move player
        m_Player.AddTorque( TiltVector * m_MovementSpeed );
        TiltVector = Vector3.zero;
    }

    void Jump( )
    {
        m_Player.AddForce( Vector3.up * m_JumpHeight );
        m_IsJumping = true;
    }

    void OnPlayerSpawn( GameObject _newPlayer )
    {
        RestTiltVector = Input.acceleration;

        m_Player = _newPlayer.GetComponent<Rigidbody>( );
    }

    void OnPlayerCollision( Collision col )
    {
        if ( Mathf.RoundToInt( col.contacts[0].normal.y ) == 1 )
        {
            m_IsJumping = false;
        }
    }
}