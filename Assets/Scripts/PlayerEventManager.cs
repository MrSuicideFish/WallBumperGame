using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerEventManager : MonoBehaviour
{
    public static PlayerCollisionEvent OnPlayerCollision = new PlayerCollisionEvent( );
    public static PlayerSpawnEvent OnPlayerSpawn = new PlayerSpawnEvent( );
}

public class PlayerCollisionEvent : UnityEvent<Collision>
{}

public class PlayerSpawnEvent : UnityEvent<GameObject>
{}