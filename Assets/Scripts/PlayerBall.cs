using UnityEngine;
using System.Collections;

public class PlayerBall : MonoBehaviour
{
    public AudioSource sfx_defaultCollision;

    void OnCollisionEnter( Collision _col )
    {
        //Play sound

        //Send hit event
        PlayerEventManager.OnPlayerCollision.Invoke( _col );
    }
}