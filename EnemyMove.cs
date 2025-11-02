using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Hedef (Base)")]
    public Transform target;     // Düşmanın gideceği hedef
    public float speed = 2f;     // Düşman hızı

    void Awake()
    {
        // Enemy tag'ini ekle
        gameObject.tag = "Enemy";
        
        // Eğer collider yoksa ekle
        if (!GetComponent<Collider>())
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }

        // Rigidbody ekle ve ayarla
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.freezeRotation = true;
            // Y ekseninde pozisyonu ve tüm rotasyonları dondur
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }

    void Update()
    {
        // Hedef varsa oraya doğru hareket et
        if (target)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var healthManager = FindObjectOfType<HealthManager>();

        // Eğer Base'e ulaştıysa
        if (other.CompareTag("Base"))
        {
            if (healthManager)
            {
                healthManager.DamageBase(20); // Her düşman 20 hasar versin
            }
            Destroy(gameObject);
        }
        // Eğer Tower'a (Player) çarparsa
        else if (other.CompareTag("Player"))
        {
            if (healthManager)
            {
                healthManager.DamagePlayer(10); // Her düşman 10 hasar versin
            }
            Destroy(gameObject);
        }
        // Eğer mermi çarptıysa
        else if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
