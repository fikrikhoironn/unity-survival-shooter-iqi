using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerHealth playerHealth;
    private Wallet wallet;

    void Awake()
    {
        if (DataManager.instance.currentSaveData != null)
        {
            playerHealth = GetComponent<PlayerHealth>();
            playerHealth.currentHealth = DataManager.instance.currentSaveData.playerData.health;
            wallet = GetComponent<Wallet>();
            wallet.gold = DataManager.instance.currentSaveData.playerData.money;
        }
    }
}
