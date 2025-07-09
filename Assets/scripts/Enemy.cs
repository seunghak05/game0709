using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject damageTextPrefab;

    private SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;

    public float flashDuration = 0.1f;
    private Color originealColor;

    public float enemyHp = 1;

    [SerializeField]
    public float moveSpeed = 1f;

    public GameObject Coin;
    public GameObject Effect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originealColor = spriteRenderer.color;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originealColor;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            Missile missile = collision.GetComponent<Missile>();
            StopAllCoroutines();
            StartCoroutine("HitColor");

            enemyHp = enemyHp - missile.missileDamage;
            if (enemyHp < 0)
            {
                Destroy(gameObject);
                Instantiate(Coin, transform.position, Quaternion.identity);
                Instantiate(Effect, transform.position, Quaternion.identity);
            }
            TakeDamage(missile.missileDamage);
        }
    }
    IEnumerator HitColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    // 데미지 처리 함수
    void TakeDamage(int damage)
    {
        // 체력 감소 처리 등...
        
        // 피격 효과
        StartCoroutine(HitColor());

        // 데미지 팝업 표시
        DamagePopUpManager.Instance.CreateDamageText(damage, transform.position);
    }
}
