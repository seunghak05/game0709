using UnityEngine;

public class DamagePopUpManager : MonoBehaviour
{
    // 싱글톤 패턴: 다른 스크립트에서 DamagePopUpManager.Instance로 접근 가능
    public static DamagePopUpManager Instance { get; private set; }

    public RectTransform canvasRect;         // UI 캔버스의 RectTransform (월드 → 화면 위치 변환 시 필요)
    public GameObject damageTextPrefab;      // 데미지 텍스트 프리팹

    // 게임이 시작될 때 가장 먼저 실행되는 함수
    void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복 매니저 제거
        }
    }

    // 데미지 텍스트를 생성하는 함수
    public void CreateDamageText(int damage, Vector3 worldPos)
    {
        // 월드 좌표 → 화면 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 캔버스 하위에 텍스트 프리팹 생성
        GameObject textObj = Instantiate(damageTextPrefab, canvasRect);

        // 텍스트 UI 위치를 화면 위치로 설정
        textObj.GetComponent<RectTransform>().position = screenPos;

        // 텍스트 내용과 애니메이션 처리
        textObj.GetComponent<DamageText>().show(damage);
    }
}
