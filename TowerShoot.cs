using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float rotationSpeed = 100f; // Dönüş hızı (derece/saniye)
    private float damageMultiplier = 1f;
    private float damageBoostTime = 0f;

    void Update()
    {
        // Hasar boost süresini kontrol et
        if (Time.time > damageBoostTime && damageMultiplier > 1f)
        {
            damageMultiplier = 1f;
        }
        // Sadece ok tuşlarıyla dönüş kontrolü
        float rotation = 0f;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = 1f; // Sola dön
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation = -1f; // Sağa dön
        }

        // Dönüş hareketini uygula
        transform.Rotate(Vector3.up * rotation * rotationSpeed * Time.deltaTime);

        // Space tuşu ile ateş etme
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript)
        {
            bulletScript.SetDamageMultiplier(damageMultiplier);
        }
    }

    // Hasar artırma power-up'ını uygula
    public void ApplyDamageBoost(float multiplier, float duration)
    {
        damageMultiplier = multiplier;
        damageBoostTime = Time.time + duration;
    }
}
