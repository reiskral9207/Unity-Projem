using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    private float damageMultiplier = 1f;

    void Awake()
    {
        // Bullet tag'ini ekle
        gameObject.tag = "Bullet";
        
        // Eğer collider yoksa ekle
        if (!GetComponent<Collider>())
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            ScoreManager.AddScore((int)(10 * damageMultiplier)); // Hasar artışıyla birlikte daha fazla puan
            Destroy(gameObject);
        }
    }

    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }
}
