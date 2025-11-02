using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    private float speedMultiplier = 1f;
    private float speedBoostTime = 0f;
    private Animator anim;
    private Rigidbody rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        // Rigidbody yoksa ekle
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true; // Rotasyonu dondur
            rb.useGravity = true; // Yerçekimini aç
            // Y ekseninde pozisyonu ve tüm rotasyonları dondur
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }

    void Update()
    {
        // Hız boost süresini kontrol et
        if (Time.time > speedBoostTime && speedMultiplier > 1f)
        {
            speedMultiplier = 1f;
        }
        // W,A,S,D ile hareket
        float h = 0f;
        float v = 0f;

        if (Input.GetKey(KeyCode.A)) h = -1f;
        if (Input.GetKey(KeyCode.D)) h = 1f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;

        Vector3 dir = new Vector3(h, 0f, v).normalized;

        bool walking = dir.sqrMagnitude > 0.01f;
        if (anim) anim.SetBool("isWalking", walking);

        if (walking)
        {
            // Rigidbody kullanarak hareket et
            Vector3 moveVelocity = dir * moveSpeed * speedMultiplier;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
        else
        {
            // Hareket etmiyorsa x ve z eksenlerinde hızı sıfırla
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    // Hız artırma power-up'ını uygula
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        speedBoostTime = Time.time + duration;
    }
}