using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingStone : MonoBehaviour
{
    [Serialize]
    StoneController controller;
    [SerializeField] GameObject moss;
    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러
    public float interval = 10.0f; // 간격 시간 (초)

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Stone.growingStone == null)
        {
            Debug.Log("No Stone Choosed");
            this.gameObject.SetActive(false);
        }
        else if(GameManager.Stone.growingStone != null)
        {
            switch (GameManager.Stone.growingStone.stoneStat.StoneType)
            {
                case STONE_TYPE.LimeStone:
                    this.AddComponent<LimeStoneController>();
                    controller = this.GetComponent<LimeStoneController>();
                    controller.stone = GameManager.Stone.growingStone;
                    break;
                case STONE_TYPE.Granite:
                    this.AddComponent<GraniteController>();
                    controller = this.GetComponent<GraniteController>();
                    controller.stone = GameManager.Stone.growingStone;
                    break;
                default:
                    Debug.Log(GameManager.Stone.growingStone);
                    break;
            }
        }
        if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(moss != null) StartCoroutine(Moss());
        }
    }

    IEnumerator Moss()
    {
        while (true)
        {
            // 스프라이트의 크기 가져오기
            Bounds bounds = spriteRenderer.bounds;

            // 스프라이트의 랜덤 위치 계산
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                bounds.center.z
            );

            // 새로운 오브젝트 생성
            GameObject mossObj = Instantiate(moss, randomPosition, Quaternion.identity);
            mossObj.transform.SetParent(transform, false);

            // 간격 시간 대기
            yield return new WaitForSeconds(interval);
        }
    }
}
