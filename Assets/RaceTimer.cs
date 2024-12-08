using UnityEngine;
using TMPro;  // TextMeshPro�� ����Ϸ��� �� ���ӽ����̽��� �ʿ��մϴ�.

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // UI �ؽ�Ʈ�� �Ҵ��� ����
    private float raceTime = 0f;       // ���̽� �ð��� ������ ����
    private bool timerRunning = false; // Ÿ�̸Ӱ� ���� ������ Ȯ���ϴ� ����
    private int collisionCount = 0;    // �浹 Ƚ��

    private void OnCollisionEnter(Collision collision)
    {
        // ù ��° �浹�� �߻��ϸ� Ÿ�̸� ����
        if (!timerRunning && collisionCount == 0)
        {
            collisionCount++;
            timerRunning = true;
            raceTime = 0f; // Ÿ�̸� �ʱ�ȭ
            Debug.Log("Ÿ�̸� ����!");
        }
        // �� ��° �浹�� �߻��ϸ� �ð� ����
        else if (collisionCount == 1)
        {
            collisionCount++;
            timerRunning = false; // Ÿ�̸� ���߱�
            Debug.Log("�� ��° �浹! �� �ð�: " + raceTime + "��");
        }
    }

    private void Update()
    {
        // Ÿ�̸Ӱ� ���� ���� ���� �ð� ����
        if (timerRunning)
        {
            raceTime += Time.deltaTime;
        }

        // UI �ؽ�Ʈ�� �ǽð� �ð� ǥ��
        if (timerText != null)
        {
            timerText.text = "Time: " + raceTime.ToString("F2") + "s";  // �Ҽ��� 2�ڸ����� ǥ��
        }
    }
}
