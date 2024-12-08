using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public CarController carController; // 차의 컨트롤러를 참조
    public ItemType itemType; // 적용될 아이템 종류

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car")) // "Car" 태그가 붙은 객체와 충돌할 때
        {
            Debug.Log("아이템과 충돌 발생: " + itemType.ToString()); // 충돌 로그 확인

            if (carController != null)
            {
                carController.ApplyItemEffect(itemType); // 아이템 효과 적용
                Debug.Log("아이템 효과 적용: " + itemType.ToString()); // 효과 적용 확인
                Destroy(gameObject); // 아이템이 적용되면 아이템 큐브 삭제
            }
            else
            {
                Debug.LogError("CarController가 설정되지 않았습니다!"); // carController가 설정되지 않았을 때
            }
        }
    }
}
