using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상
    public Vector3 offset = new Vector3(0, 5, -10); // 대상과의 거리 (x, y, z)
    public float smoothSpeed = 0.125f; // 움직임의 부드러움 정도

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("FollowCamera: 타겟이 설정되지 않았습니다!");
            return;
        }

        // 목표 위치 계산
        Vector3 desiredPosition = target.position + offset;

        // 부드럽게 이동 (선택 사항: 부드러운 움직임 적용)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 카메라 위치 설정
        transform.position = smoothedPosition;

        // 대상 바라보기 (선택 사항)
        transform.LookAt(target);
    }
}
