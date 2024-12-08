using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float maxRotationAngle = 90f; // �ڵ��� �ִ� ȸ�� ����

    /// <summary>
    /// �ڵ� ȸ���� ������Ʈ�ϴ� �Լ�
    /// </summary>
    /// <param name="steeringInput">���� �Է� �� (-1.0 ~ 1.0)</param>
    public void UpdateSteeringWheel(float steeringInput)
    {
        // �Է°��� ���� Z�� ���� ȸ�� ���
        float rotationZ = steeringInput * maxRotationAngle;
        transform.localRotation = Quaternion.Euler(0f, 0f, -rotationZ); // Z������ ȸ��
    }
}
