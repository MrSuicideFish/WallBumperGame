using UnityEngine;
using System.Collections;
using System;

public interface IPICKUP
{
    AudioSource PickupSound { get; }
    void Pickup( );
}

public class wb_Pickup : MonoBehaviour, IPICKUP
{
    public AudioSource PickupSound { get; private set; }

    float m_initialY, m_newY;
    float m_oscilRange, m_oscilOffset;
    public virtual void Awake( )
    {
        m_initialY = transform.position.y;
        m_newY = m_initialY + 0.5f;
        m_oscilRange = ( m_newY - m_initialY ) / 2;
        m_oscilOffset = m_oscilRange + m_initialY;
    }

    public virtual void Update( )
    {
        //floating
        transform.position = new Vector3( transform.position.x,
            m_oscilOffset + Mathf.Sin( Time.time ) * m_oscilRange, transform.position.z );
    }

    public void Pickup( )
    {
        //play pickup sound

        //Destroy Obj
        GameObject.Destroy( gameObject );
    }

    void OnTriggerEnter(Collider _col )
    {
        Pickup( );
    }
}