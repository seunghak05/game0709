using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 코인 개수
    public int coin = 0;

    // 코인 개수를 표시할 텍스트 UI
    public TextMeshProUGUI textMeshProCoin;

    // 싱글톤 인스턴스 (다른 스크립트에서 GameManager.Instance로 접근 가능)
    public static GameManager Instance { get; private set; }

    // 게임 시작 시 싱글톤 초기화
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    // 코인 개수를 증가시키고 UI에 표시
    public void ShowCoinCount()
    {
        coin++;

        // 코인 텍스트 갱신
        if (textMeshProCoin != null)
            textMeshProCoin.SetText(coin.ToString());

        // 2개마다 미사일 업그레이드
        if (coin % 2 == 0)
        {
            // FindFirstObjectByType은 Unity 2023 이상에서 사용 가능
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.MissileUp(); // 플레이어에게 업그레이드 명령
            }
        }
    }
}
