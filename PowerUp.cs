using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Health,     // Can yenileme
        Speed,      // Hız artırma
        Damage      // Mermi hasarı artırma
    }

    public PowerUpType type;
    public float duration = 10f;    // Power-up süresi (saniye)
    public float value = 2f;        // Etki miktarı

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        HealthManager healthManager = FindObjectOfType<HealthManager>();

        switch (type)
        {
            case PowerUpType.Health:
                if (healthManager)
                {
                    healthManager.HealPlayer(50); // 50 can yenile
                }
                break;

            case PowerUpType.Speed:
                if (controller)
                {
                    controller.ApplySpeedBoost(value, duration);
                }
                break;

            case PowerUpType.Damage:
                // Mermi hasarını artır
                TowerShoot tower = player.GetComponent<TowerShoot>();
                if (tower)
                {
                    tower.ApplyDamageBoost(value, duration);
                }
                break;
        }
    }
}