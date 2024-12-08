using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public CarController carController; // ���� ��Ʈ�ѷ��� ����
    public ItemType itemType; // ����� ������ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car")) // "Car" �±װ� ���� ��ü�� �浹�� ��
        {
            Debug.Log("�����۰� �浹 �߻�: " + itemType.ToString()); // �浹 �α� Ȯ��

            if (carController != null)
            {
                carController.ApplyItemEffect(itemType); // ������ ȿ�� ����
                Debug.Log("������ ȿ�� ����: " + itemType.ToString()); // ȿ�� ���� Ȯ��
                Destroy(gameObject); // �������� ����Ǹ� ������ ť�� ����
            }
            else
            {
                Debug.LogError("CarController�� �������� �ʾҽ��ϴ�!"); // carController�� �������� �ʾ��� ��
            }
        }
    }
}
