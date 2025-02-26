using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerSpawn _start;
    [SerializeField] Transform _end;

    public PlayerSpawn start => _start;
    public Transform end => _end;
}
