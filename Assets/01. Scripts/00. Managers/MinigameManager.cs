using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public MinigameStoneController stoneControllerScript;
    private ObjectPool objectPool;

    public float spawnInterval = 2.0f;
    public Transform[] spawnPoints;

    public RawImage stoneLifeImage;
    private RawImage[] stoneLifeImages;
    public GameObject lifeTransform;
    private int oldLife;

    public GameObject startPanel;
    public GameObject endPanel;
    public GameObject clearPanel;
    public Button startBtn;
    public Button restartBtn;
    public Button backGameBtn;

    public bool isGameOver;
    public float gameTime = 10f;
    private float spawntimer;
    private float gametimer;

    public GameObject checkLocation;
    public Image barUI;
    public Image locationUI;

    void Start()
    {
        objectPool = this.GetComponent<ObjectPool>();
        GameManager.Instance._minigame = this;
        
        Time.timeScale = 0f;

        startBtn.onClick.AddListener(StartGame);
        restartBtn.onClick.AddListener(RestartGame);
        backGameBtn.onClick.AddListener(GotoSauna);
    }

    void Update()
    {
        if(oldLife > stoneControllerScript.life) {
            oldLife = stoneControllerScript.life;
            if (oldLife < 0) oldLife = 0;
            for (int i = oldLife; i < stoneLifeImages.Length; i++) {
                stoneLifeImages[i].enabled = false;
            }
        }

        if(!isGameOver)
        {
            spawntimer += Time.deltaTime;
            gametimer += Time.deltaTime;

            MoveMiniStone();

            if (spawntimer >= spawnInterval)
            {
                SpawnObstacle();
                spawntimer = 0;
            }

            if (gametimer > gameTime)
            {
                isGameOver = true;
                clearPanel.SetActive(true);
                ITEM_TYPE randomItem = (ITEM_TYPE)Random.Range(0, System.Enum.GetValues(typeof(ITEM_TYPE)).Length);
                Debug.Log(randomItem);
                GameManager.Item.AcquireItem(randomItem);
                Time.timeScale = 0f;
            }
        }else{
            endPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = objectPool.GetPooledObject();
        if (obstacle != null)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            obstacle.transform.position = spawnPoint.position;
            obstacle.SetActive(true);
        }
    }

    private void MoveMiniStone()
    {
        float progress = gametimer / gameTime;
        float xPosition = Mathf.Lerp(0, 890, progress);
        locationUI.rectTransform.localPosition = new Vector2(xPosition, locationUI.rectTransform.localPosition.y);
    }

    private void SetData()
    {
        Time.timeScale = 1;

        checkLocation.SetActive(true);

        Vector2 startPos = new Vector2(0, locationUI.rectTransform.anchoredPosition.y);
        locationUI.rectTransform.anchoredPosition = startPos;

        /*GameManager.Stone.WhenPlayerDecideGrowingNewStoneInBulgama(GameManager.Stone.stoneInfo["LimeStone"]);*/
        if(GameManager.Stone.growingStone.stoneStat.Equals(STONE_TYPE.LimeStone))
            stoneControllerScript.life = 3;
        else if(GameManager.Stone.growingStone.stoneStat.Equals(STONE_TYPE.Granite))
            stoneControllerScript.life = 5;

        stoneLifeImages = new RawImage[stoneControllerScript.life];
        for(int i = 0; i < stoneControllerScript.life; i++) {
            stoneLifeImages[i] = Instantiate(stoneLifeImage);
            stoneLifeImages[i].transform.SetParent(lifeTransform.transform);
            stoneLifeImages[i].rectTransform.localScale = new Vector3(2.1049f,2.1049f,2.1049f);
            stoneLifeImages[i].rectTransform.localPosition = new Vector3(-238 + (210 * i), 1193, 0);
        }

        oldLife = stoneControllerScript.life;
        gametimer = 0;
    }

    private void StartGame()
    {
        SetData();
        startPanel.SetActive(false);
    }

    private void RestartGame()
    {
        SetData();
        isGameOver = false;
        endPanel.SetActive(false);
    }

    private void GotoSauna()
    {
        GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
    }
}