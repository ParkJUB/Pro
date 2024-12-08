using UnityEngine;
using TMPro; // TextMeshPro�� ����ϱ� ���� ���ӽ����̽� �߰�
using System.Collections;

public class CarController : MonoBehaviour
{
    public Rigidbody carRigidbody;

    public float acceleration = 500f; // ���ӷ�
    public float steering = 20f; // ȸ����
    public float brakeForce = 1000f; // �극��ũ ��

    public TextMeshProUGUI gearText; // ��� ǥ�ÿ� TextMeshProUGUI
    public TextMeshProUGUI speedText; // �ӵ� ǥ�ÿ� TextMeshProUGUI

    public float[] gearRatios = { 1f, 2f, 3f, 4f, 5f }; // ���� �迭, Inspector���� ���� ����
    private int currentGear = 1; // ���� ���
    private bool isBraking = false;

    public SteeringWheelController steeringWheel; // �ڵ� ��Ʈ�ѷ� ����

    private float turnInput = 0f; // ȸ�� �Է� (�¿�)

    // ������ ȿ�� ���� ����
    private bool isSpeedReduced = false;
    private bool isControlInverted = false;
    private bool isStopped = false;
    private float originalSpeed;
    private float speedReductionFactor = 0.5f; // �ӵ� ���� ȿ�� ����
    private float controlInvertDuration = 3f; // �¿� ��Ʈ�� ���� ���� �ð�

    void Start()
    {
        // �ʱ� ��� ���� ������Ʈ
        if (gearText != null)
        {
            gearText.text = $"Gear: {currentGear}";
        }

        originalSpeed = acceleration; // �ʱ� �ӵ� ����
    }

    void Update()
    {
        // �ӵ� ���
        float speed = carRigidbody.velocity.magnitude * 3.6f; // m/s�� km/h�� ��ȯ
        if (speedText != null)
        {
            speedText.text = $"Speed: {speed:F1} km/h";
        }

        // ��� ����
        if (Input.GetKeyDown(KeyCode.E) && currentGear < gearRatios.Length) // ��� ��
        {
            currentGear++;
            if (gearText != null)
            {
                gearText.text = $"Gear: {currentGear}";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentGear > 1) // ��� �ٿ�
        {
            currentGear--;
            if (gearText != null)
            {
                gearText.text = $"Gear: {currentGear}";
            }
        }

        // ȸ�� �Է� ó�� (�¿�)
        turnInput = Input.GetAxis("Horizontal"); // A, D �Է�

        // �������� ����Ǿ��� ��, ������ ȿ���� Ʈ���� (���� �浹 �� ó��)
    }

    void FixedUpdate()
    {
        if (isStopped) return; // ���� ������ ���� �������� �ʵ��� ó��

        float forwardInput = Input.GetAxis("Vertical"); // W, S �Է�

        // ���� �� ���� (�ӵ� ���� ȿ�� ����)
        float motorForce = forwardInput * acceleration * gearRatios[currentGear - 1];
        if (isSpeedReduced) motorForce *= speedReductionFactor; // �ӵ� ���� ����
        carRigidbody.AddForce(transform.forward * motorForce);

        // ȸ�� (�¿� ���� ȿ�� ����)
        float turnForce = turnInput * steering;
        if (isControlInverted) turnForce = -turnForce; // �¿� ���� ����
        carRigidbody.AddTorque(Vector3.up * turnForce);

        // �ڵ� ȸ�� ������Ʈ
        if (steeringWheel != null)
        {
            steeringWheel.UpdateSteeringWheel(turnInput); // �ڵ� Z�� ȸ�� ������Ʈ
        }

        // �극��ũ
        isBraking = Input.GetKey(KeyCode.Space);
        if (isBraking)
        {
            carRigidbody.AddForce(-carRigidbody.velocity.normalized * brakeForce);
        }
    }

    // ������ ȿ�� ����
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

    // �ӵ� ���� ������ ȿ��
    private IEnumerator ReduceSpeedEffect()
    {
        isSpeedReduced = true;
        yield return new WaitForSeconds(5f); // 5�� ���� �ӵ� ����
        isSpeedReduced = false;
    }

    // �¿� ���� ������ ȿ��
    private IEnumerator InvertControlsEffect()
    {
        isControlInverted = true;
        yield return new WaitForSeconds(controlInvertDuration); // ���� �ð� ���� �¿� ����
        isControlInverted = false;
    }

    // �ڵ��� ���� ������ ȿ��
    private IEnumerator StopCarEffect()
    {
        isStopped = true;
        yield return new WaitForSeconds(3f); // 3�� ���� ����
        isStopped = false;
    }
}

public enum ItemType
{
    SpeedReduction,
    ControlInvert,
    StopCar
}
