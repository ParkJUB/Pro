using UnityEngine;
using TMPro;  // TextMeshPro를 사용하려면 이 네임스페이스가 필요합니다.

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // UI 텍스트를 할당할 변수
    private float raceTime = 0f;       // 레이스 시간을 저장할 변수
    private bool timerRunning = false; // 타이머가 실행 중인지 확인하는 변수
    private int collisionCount = 0;    // 충돌 횟수

    private void OnCollisionEnter(Collision collision)
    {
        // 첫 번째 충돌이 발생하면 타이머 시작
        if (!timerRunning && collisionCount == 0)
        {
            collisionCount++;
            timerRunning = true;
            raceTime = 0f; // 타이머 초기화
            Debug.Log("타이머 시작!");
        }
        // 두 번째 충돌이 발생하면 시간 측정
        else if (collisionCount == 1)
        {
            collisionCount++;
            timerRunning = false; // 타이머 멈추기
            Debug.Log("두 번째 충돌! 총 시간: " + raceTime + "초");
        }
    }

    private void Update()
    {
        // 타이머가 실행 중일 때만 시간 증가
        if (timerRunning)
        {
            raceTime += Time.deltaTime;
        }

        // UI 텍스트에 실시간 시간 표시
        if (timerText != null)
        {
            timerText.text = "Time: " + raceTime.ToString("F2") + "s";  // 소수점 2자리까지 표시
        }
    }
}
