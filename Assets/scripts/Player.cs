using UnityEngine;

public class Player : MonoBehaviour
{
     private bool isDead = false;
    private Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 1f; // 플레이어 이동 속도

    int missIndex = 0; // 현재 미사일 종류 인덱스

    public GameObject[] missilePrefab; // 미사일 프리팹 배열
    public Transform spPostion; // 미사일 생성 위치 (Transform)

    [SerializeField]
    private float shootInverval = 0.05f; // 미사일 연사 간격

    private float lastshotTime = 0f; // 마지막으로 미사일을 쏜 시간 저장

    private Animator animator; // 애니메이터 컴포넌트

    // Start는 스크립트가 처음 실행될 때 1회 실행됨
    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update는 매 프레임마다 호출됨
    void Update()
    {
        // 좌우 방향 키 입력 받기 (-1, 0, 1 중 하나 반환)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Debug.Log("ㅎ히히" + horizontalInput);
        // 이동할 방향 계산
        Vector3 moveTo = new Vector3(horizontalInput, 0, 0);

        // 위치 이동 (속도와 프레임 시간 반영)
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        // 입력에 따라 애니메이션 재생
        if (horizontalInput < 0)
        {
            animator.Play("Left"); // 왼쪽 이동 애니메이션
        }
        else if (horizontalInput > 0)
        {
            animator.Play("Right"); // 오른쪽 이동 애니메이션
        }
        else
        {
            animator.Play("Idle"); // 대기 애니메이션
        }

        // 미사일 발사 시도
        shoot();
    }

    // 미사일 발사 처리
    void shoot()
    {
        // 마지막 발사 후 간격이 충분히 지났는지 확인
        if (Time.time - lastshotTime > shootInverval)
        {
            // 미사일 생성 (지정된 위치에 현재 인덱스의 프리팹으로)
            Instantiate(missilePrefab[missIndex], spPostion.position, Quaternion.identity);

            // 마지막 발사 시간 갱신
            lastshotTime = Time.time;
        }
    }

    // 미사일 업그레이드 함수
    public void MissileUp()
    {
        // 다음 미사일 종류로 변경
        missIndex++;

        // 발사 간격 줄이기 (더 빠르게 발사)
        shootInverval -= 0.1f;

        // 발사 간격이 너무 작아지지 않도록 최소값 설정
        if (shootInverval <= 0.1f)
        {
            shootInverval = 0.1f;
        }

        // 미사일 인덱스가 배열 범위를 넘지 않도록 제한
        if (missIndex >= missilePrefab.Length)
        {
            missIndex = missilePrefab.Length - 1;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isDead && collision.CompareTag("Enemy"))
        {
            // 적과 충돌 시 튕기기
            Jump();
            isDead = true; // 한번만 실행되도록
            GameManager.Instance.gameOverPanel.SetActive(true);
        }
        else if (collision.CompareTag("Bottom"))
        {
            Time.timeScale = 0f;
        }
    }

    void Jump()
    {
        // x축은 -1~1, y축은 3~5의 임의의 힘을 가함 (Impulse 모드)
        rb.gravityScale = 2;
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 6f)), ForceMode2D.Impulse);
        
    }
}
