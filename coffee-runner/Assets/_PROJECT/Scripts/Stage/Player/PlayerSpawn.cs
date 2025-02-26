using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] PlayerController _playerPrefab;
    [SerializeField] Vector3 _positionOffset;
    
    public PlayerController Spawn()
    {
        var newPlayer = Instantiate(_playerPrefab);
        newPlayer.transform.position = transform.position + _positionOffset;
        return newPlayer;
    }
}
