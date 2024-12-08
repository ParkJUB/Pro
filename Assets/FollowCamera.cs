using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ���
    public Vector3 offset = new Vector3(0, 5, -10); // ������ �Ÿ� (x, y, z)
    public float smoothSpeed = 0.125f; // �������� �ε巯�� ����

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("FollowCamera: Ÿ���� �������� �ʾҽ��ϴ�!");
            return;
        }

        // ��ǥ ��ġ ���
        Vector3 desiredPosition = target.position + offset;

        // �ε巴�� �̵� (���� ����: �ε巯�� ������ ����)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ī�޶� ��ġ ����
        transform.position = smoothedPosition;

        // ��� �ٶ󺸱� (���� ����)
        transform.LookAt(target);
    }
}
