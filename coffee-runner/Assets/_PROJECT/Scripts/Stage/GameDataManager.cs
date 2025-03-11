using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance { get; private set; }
    
    public int coins { get; set; }
    public int premiumCoins { get; set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(instance);
    }
}
