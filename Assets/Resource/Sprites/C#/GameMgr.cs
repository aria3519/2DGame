using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameMgr instance = null;
    public static GameMgr Instance
    {
        get
        {
            if(null == instance)
            {
                instance = new GameObject("GameMgr").AddComponent<GameMgr>();
                DontDestroyOnLoad(instance.gameObject);

            }
            return instance;
        }
    }
    private Text score;
    private float scoreCount = 0;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
            return;
        }
        var canvas = FindObjectOfType<Canvas>();
        if(canvas)
        {
            var scoreTr = canvas.transform.Find("Score");
            if (scoreTr) score = scoreTr.GetComponent<Text>();
            
            var gameOverTr = canvas.transform.Find("GameOver");
            if (gameOverTr) gameOver = gameOverTr.gameObject;
        }
       

        Destroy(gameObject);
    }
    public bool isDead { get; private set; }
    public void OnDie()
    {
        isDead = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Instance.isDead) return;
        if (false == isDead && score)
        {
            scoreCount += Time.deltaTime * 100;
            score.text = string.Format("Score : {0:N0}", Mathf.RoundToInt(scoreCount));
        }
    }
    private GameObject gameOver;
    public void GameOver()
    {
        if (gameOver) gameOver.SetActive(true);
    }
}
