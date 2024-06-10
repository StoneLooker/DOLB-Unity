using UnityEngine;
using UnityEngine.UI;

public class ShowStoneInfo : MonoBehaviour
{
    public Text objectNameText;  // 오브젝트 이름을 표시할 UI 텍스트
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        objectNameText.gameObject.SetActive(false);  // 시작 시 텍스트 비활성화
    }

    void Update()
    {
        Vector2 mousePosition = GameManager.Input.mousePosUnity;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if ((StoneController)hitObject.GetComponent<StoneController>() != null)
            {
               StoneController hitStone = (StoneController)hitObject.GetComponent<StoneController>();
               objectNameText.text = 
                    "TYPE: " + hitStone.stone.nickName + "\r\n" /*+
                    "HP: " + hitStone.stone.HP + "\r\n" +
                    "Love:" + hitStone.stone.loveGage + "\r\n" +
                    "Evloution: " + hitStone.stone.nextEvolutionPercentage + "\r\n" +
                    "INFO: " + hitStone.stone.stoneInfo + "\r\n"*/;      
                objectNameText.gameObject.SetActive(true);
                objectNameText.transform.position = Input.mousePosition;  // 마우스 위치 기준으로 텍스트 위치 조정
            } else {
                objectNameText.text = hitObject.name;
                objectNameText.gameObject.SetActive(true);
                objectNameText.transform.position = Input.mousePosition;  
            }

        }
        else
        {
            objectNameText.gameObject.SetActive(false);  // 아무 오브젝트도 감지되지 않으면 텍스트 비활성화
        }
    }
}
