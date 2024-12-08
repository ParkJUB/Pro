using UnityEngine;
using UnityEngine.UI;

public class CarUIController : MonoBehaviour
{
    public Text gearText; // ���� ��� ǥ��
    public Text speedText; // �ӵ� ǥ��
    public Rigidbody carRigidbody; // �ڵ����� Rigidbody
    public int currentGear = 1; // �⺻ ���
    private float[] gearRatios = { 0, 3.5f, 2.8f, 1.9f, 1.4f, 1.0f }; // ����
    private float maxSpeedPerGear = 40f; // ��� �� �ִ� �ӵ�(km/h)

    void Update()
    {
        // �ӵ� ���
        float speed = carRigidbody.velocity.magnitude * 3.6f; // m/s�� km/h�� ��ȯ
        speedText.text = $"{speed:0.0} km/h";

        // ��� ���� ������Ʈ
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
