using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    public void Spawn(GameObject player)
    {
        // var newPlayer = Instantiate(player);
        player.transform.position = transform.position;
        // return newPlayer;
    }
}
