using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스 추가
using System.Collections;

public class CarController : MonoBehaviour
{
    public Rigidbody carRigidbody;

    public float acceleration = 500f; // 가속력
    public float steering = 20f; // 회전력
    public float brakeForce = 1000f; // 브레이크 힘

    public TextMeshProUGUI gearText; // 기어 표시용 TextMeshProUGUI
    public TextMeshProUGUI speedText; // 속도 표시용 TextMeshProUGUI

    public float[] gearRatios = { 1f, 2f, 3f, 4f, 5f }; // 기어비 배열, Inspector에서 설정 가능
    private int currentGear = 1; // 현재 기어
    private bool isBraking = false;

    public SteeringWheelController steeringWheel; // 핸들 컨트롤러 연결

    private float turnInput = 0f; // 회전 입력 (좌우)

    // 아이템 효과 관련 변수
    private bool isSpeedReduced = false;
    private bool isControlInverted = false;
    private bool isStopped = false;
    private float originalSpeed;
    private float speedReductionFactor = 0.5f; // 속도 감소 효과 비율
    private float controlInvertDuration = 3f; // 좌우 컨트롤 반전 지속 시간

    void Start()
    {
        // 초기 기어 정보 업데이트
        if (gearText != null)
        {
            gearText.text = $"Gear: {currentGear}";
        }

        originalSpeed = acceleration; // 초기 속도 설정
    }

    void Update()
    {
        // 속도 계산
        float speed = carRigidbody.velocity.magnitude * 3.6f; // m/s를 km/h로 변환
        if (speedText != null)
        {
            speedText.text = $"Speed: {speed:F1} km/h";
        }

        // 기어 변경
        if (Input.GetKeyDown(KeyCode.E) && currentGear < gearRatios.Length) // 기어 업
        {
            currentGear++;
            if (gearText != null)
            {
                gearText.text = $"Gear: {currentGear}";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentGear > 1) // 기어 다운
        {
            currentGear--;
            if (gearText != null)
            {
                gearText.text = $"Gear: {currentGear}";
            }
        }

        // 회전 입력 처리 (좌우)
        turnInput = Input.GetAxis("Horizontal"); // A, D 입력

        // 아이템이 적용되었을 때, 아이템 효과를 트리거 (추후 충돌 시 처리)
    }

    void FixedUpdate()
    {
        if (isStopped) return; // 정지 상태일 때는 움직이지 않도록 처리

        float forwardInput = Input.GetAxis("Vertical"); // W, S 입력

        // 가속 및 감속 (속도 감소 효과 적용)
        float motorForce = forwardInput * acceleration * gearRatios[currentGear - 1];
        if (isSpeedReduced) motorForce *= speedReductionFactor; // 속도 감소 적용
        carRigidbody.AddForce(transform.forward * motorForce);

        // 회전 (좌우 반전 효과 적용)
        float turnForce = turnInput * steering;
        if (isControlInverted) turnForce = -turnForce; // 좌우 반전 적용
        carRigidbody.AddTorque(Vector3.up * turnForce);

        // 핸들 회전 업데이트
        if (steeringWheel != null)
        {
            steeringWheel.UpdateSteeringWheel(turnInput); // 핸들 Z축 회전 업데이트
        }

        // 브레이크
        isBraking = Input.GetKey(KeyCode.Space);
        if (isBraking)
        {
            carRigidbody.AddForce(-carRigidbody.velocity.normalized * brakeForce);
        }
    }

    // 아이템 효과 적용
    public void ApplyItemEffect(ItemType item)
    {
        switch (item)
        {
            case ItemType.SpeedReduction:
                StartCoroutine(ReduceSpeedEffect());
                break;
            case ItemType.ControlInvert:
                StartCoroutine(InvertControlsEffect());
                break;
            case ItemType.StopCar:
                StartCoroutine(StopCarEffect());
                break;
        }
    }

    // 속도 감소 아이템 효과
    private IEnumerator ReduceSpeedEffect()
    {
        isSpeedReduced = true;
        yield return new WaitForSeconds(5f); // 5초 동안 속도 감소
        isSpeedReduced = false;
    }

    // 좌우 반전 아이템 효과
    private IEnumerator InvertControlsEffect()
    {
        isControlInverted = true;
        yield return new WaitForSeconds(controlInvertDuration); // 일정 시간 동안 좌우 반전
        isControlInverted = false;
    }

    // 자동차 정지 아이템 효과
    private IEnumerator StopCarEffect()
    {
        isStopped = true;
        yield return new WaitForSeconds(3f); // 3초 동안 정지
        isStopped = false;
    }
}

public enum ItemType
{
    SpeedReduction,
    ControlInvert,
    StopCar
}
