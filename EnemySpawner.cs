using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    [Range(0f,1f)]
    public float baseTargetChance = 0.5f; // % kaç düşman Base'i hedeflesin

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: enemyPrefab is not assigned or was deleted. Spawning disabled.");
            return;
        }

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: enemyPrefab became null at runtime. Cancelling spawning.");
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        GameObject e = null;
        try
        {
            e = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("EnemySpawner: Failed to instantiate enemyPrefab. " + ex.Message);
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }
        var enemyMove = e.GetComponent<EnemyMove>();
        if (enemyMove == null) return;

        // Bulunduğunda Player ve Base objelerini tag ile bul
        var playerObj = GameObject.FindWithTag("Player");
        var baseObj = GameObject.FindWithTag("Base");

        // Eğer her ikisi de varsa rastgele bir hedef ata
        if (playerObj != null && baseObj != null)
        {
            if (Random.value < baseTargetChance)
                enemyMove.target = baseObj.transform;
            else
                enemyMove.target = playerObj.transform;
        }
        else if (playerObj != null)
        {
            enemyMove.target = playerObj.transform;
        }
        else if (baseObj != null)
        {
            enemyMove.target = baseObj.transform;
        }
    }
}
