using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static GameObject INST_PLAYER;
    public GameObject PlayerMeshPrefab;

    void Start( )
    {
        //Look for player start position
        GameObject[] playerStartLocations = GameObject.FindGameObjectsWithTag( "PlayerSpawn" );

        if(playerStartLocations.Length == 0 )
        {
            Debug.LogException( new System.Exception( "MISSING PLAYER START" ) );
        }
        else if(playerStartLocations.Length > 1 )
        {
            Debug.LogException( new System.Exception( "Multiple spawn points found. Only '1' spawn point allowed." ) );
        }
        else
        {
            //spawn player
            INST_PLAYER = ( GameObject )GameObject.Instantiate( PlayerMeshPrefab, playerStartLocations[0].transform.position, Quaternion.Euler( 0, 0, 0 ) );
            PlayerEventManager.OnPlayerSpawn.Invoke( INST_PLAYER );
        }
    }
}