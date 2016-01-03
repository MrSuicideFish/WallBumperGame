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

    float deltaTime = 0.0f;
    void Update( )
    {
        deltaTime += ( Time.deltaTime - deltaTime ) * 0.1f;
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

    void OnGUI( )
    {
        GUI.Label( new Rect( 0, 20, 256, 32 ), "Gyro Enabled: " + Input.gyro.enabled );
        GUI.Label( new Rect( 0, 40, 256, 32 ), "Gyro Attitude: " + Input.gyro.attitude.eulerAngles.ToString( ) );
        GUI.Label( new Rect( 0, 60, 256, 32 ), "Rotation Rate: " + Input.gyro.rotationRate.ToString( ) );
        GUI.Label( new Rect( 0, 80, 256, 32 ), "Rotation Rate (Unbiased): " + Input.gyro.rotationRateUnbiased.ToString( ) );

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle( );

        Rect rect = new Rect( 0, 0, w, h * 2 / 100 );
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format( "{0:0.0} ms ({1:0.} fps)", msec, fps );
        GUI.Label( rect, text, style );
    }
}