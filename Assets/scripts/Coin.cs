using UnityEngine;

public class Coin : MonoBehaviour
{
    // Rigidbody2D 컴포넌트 참조
    private Rigidbody2D rb;

    // 시작 시 호출됨
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        Jump(); // 코인을 위로 튀기기
    }

    // 코인을 위로 튀기는 함수
    void Jump()
    {
        // x축은 -1~1, y축은 3~5의 임의의 힘을 가함 (Impulse 모드)
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 6f)), ForceMode2D.Impulse);
    }

    // 플레이어와 충돌 시 호출됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.ShowCoinCount(); // 코인 개수 표시
            Destroy(gameObject); // 코인 오브젝트 삭제
        }
    }
}