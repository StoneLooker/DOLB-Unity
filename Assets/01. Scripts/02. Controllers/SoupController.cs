using UnityEngine;

public class SoupController : MonoBehaviour
{
    public GameObject targetObject;
    public float duration = 5.0f; // 붉어지는 데 걸리는 시간 (초)
    private bool startColorChange = false;
    private float elapsedTime = 0f;
    private Color initialColor;
    private Color targetColor = Color.black;
    public SOUP_TYPE type;

    private void Start()
    {
        // 초기 색상을 저장합니다.
        if (targetObject != null)
        {
            initialColor = targetObject.GetComponent<Renderer>().material.color;
        }

        if(type == SOUP_TYPE.Hot)
        {
            targetColor = Color.red;
        }
        else if(type == SOUP_TYPE.Cold)
        {
            targetColor = Color.blue;
        }

        // 10초 후에 색 변경을 시작합니다.

        Invoke("UpdateStone", 2.5f);
        Invoke("StartColorChange", 2f);
    }

    private void Update()
    {
        if (startColorChange && targetObject != null)
        {
            // 색 변경이 시작되면 경과 시간을 증가시킵니다.
            elapsedTime += Time.deltaTime;

            // 색을 점진적으로 변경합니다.
            float t = Mathf.Clamp01(elapsedTime / duration);
            targetObject.GetComponent<Renderer>().material.color = Color.Lerp(initialColor, targetColor, t);
        }
    }

    private void StartColorChange()
    {
        startColorChange = true;
    }

    private void UpdateStone()
    {
        if(GameManager.Stone.growingStone != null)
        {
            Stone gStone = GameManager.Stone.growingStone;
            switch(gStone.stoneStat.StoneType)
            {
                case STONE_TYPE.LimeStone:
                    if (type == SOUP_TYPE.Cold) gStone.UpdateLoveGage(15);
                    else if (type  == SOUP_TYPE.Hot) gStone.UpdateLoveGage(10);
                    break;
                case STONE_TYPE.Granite:
                    if (type == SOUP_TYPE.Cold) gStone.UpdateLoveGage(10);
                    else if (type == SOUP_TYPE.Hot) gStone.UpdateLoveGage(15);
                    break;
                default:
                    break;
            }
        }
    }
}

public enum SOUP_TYPE
{
    Hot, Cold
}
