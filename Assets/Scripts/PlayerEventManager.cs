using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerEventManager : MonoBehaviour
{
    public static PlayerCollisionEvent OnPlayerCollision = new PlayerCollisionEvent( );
}

public class PlayerCollisionEvent : UnityEvent<Collision>
{

}