using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    
    public int score = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Collectible.OnCollected += HandleCollectibleCollected;
    }
    void OnDestroy()
    {
        Collectible.OnCollected -= HandleCollectibleCollected;
    }

    private void HandleCollectibleCollected(){
        score++;
        Debug.Log("pontuação: "+ score);
    }
}