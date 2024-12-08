using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float maxRotationAngle = 90f; // 핸들의 최대 회전 각도

    /// <summary>
    /// 핸들 회전을 업데이트하는 함수
    /// </summary>
    /// <param name="steeringInput">조향 입력 값 (-1.0 ~ 1.0)</param>
    public void UpdateSteeringWheel(float steeringInput)
    {
        // 입력값에 따라 Z축 기준 회전 계산
        float rotationZ = steeringInput * maxRotationAngle;
        transform.localRotation = Quaternion.Euler(0f, 0f, -rotationZ); // Z축으로 회전
    }
}
