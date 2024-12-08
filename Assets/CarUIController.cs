using UnityEngine;
using UnityEngine.UI;

public class CarUIController : MonoBehaviour
{
    public Text gearText; // 현재 기어 표시
    public Text speedText; // 속도 표시
    public Rigidbody carRigidbody; // 자동차의 Rigidbody
    public int currentGear = 1; // 기본 기어
    private float[] gearRatios = { 0, 3.5f, 2.8f, 1.9f, 1.4f, 1.0f }; // 기어비
    private float maxSpeedPerGear = 40f; // 기어 당 최대 속도(km/h)

    void Update()
    {
        // 속도 계산
        float speed = carRigidbody.velocity.magnitude * 3.6f; // m/s를 km/h로 변환
        speedText.text = $"{speed:0.0} km/h";

        // 기어 상태 업데이트
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentGear < gearRatios.Length - 1)
        {
            currentGear++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentGear > 1)
        {
            currentGear--;
        }

        gearText.text = $"Gear: {currentGear}";
    }
}
