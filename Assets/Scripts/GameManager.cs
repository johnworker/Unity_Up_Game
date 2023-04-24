using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace kan
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] enemyObjs;
        public Transform[] spawnPoints;

        public float maxSpawnDelay;
        public float curSpawnDelay;

        public GameObject player;
        public TextMeshProUGUI scoreText;
        public Image[] lifeImage;
        public GameObject gameOverSet;

        void Update()
        {
            curSpawnDelay += Time.deltaTime;
            
            if(curSpawnDelay > maxSpawnDelay)
            {
                SpawnEnemy();
                maxSpawnDelay = Random.Range(0.5f, 3f);
                curSpawnDelay = 0;
            }

            // UI 分數更新
            Player playerLogic = player.GetComponent<Player>();
            scoreText.text = string.Format("{0:n0}", playerLogic.score);
        }

        void SpawnEnemy()
        {
            int ranEnemy = Random.Range(0, 3);
            int ranPoint = Random.Range(0, 9);

            GameObject enemy = Instantiate(enemyObjs[ranEnemy],
                               spawnPoints[ranPoint].position,
                               spawnPoints[ranPoint].rotation);
            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();

            enemyLogic.player = player;

            // 右側生成
            if (ranPoint == 5 || ranPoint == 6)
            {
                enemy.transform.Rotate(Vector3.back * 90);
                rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
            }

            // 左側生成
            else if (ranPoint == 7 || ranPoint == 8)
            {
                enemy.transform.Rotate(Vector3.forward * 90);
                rigid.velocity = new Vector2(enemyLogic.speed, -1);
            }

            // 前方生成
            else
            {
                rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
            }
        }

        public void UpdateLifeIcon(int life)
        {
            // UI 生命裡面禁用
            for (int index = 0; index < 3; index++)
            {
                lifeImage[index].color = new Color(1, 1, 1, 0);
            }
            // UI 生命啟用
            for (int index = 0; index < life; index++)
            {
                lifeImage[index].color = new Color(1, 1, 1, 1);
            }
        }

        public void RespawnPlayer()
        {
            Invoke("RespawnPlayerExe", 2f);
        }

        void RespawnPlayerExe()
        {
            player.transform.position = Vector3.down * 3.5f;
            player.SetActive(true);

            Player playerLogic = player.GetComponent<Player>();
            playerLogic.isHit = false;
        }

        public void GameOver()
        {
            gameOverSet.SetActive(true);
        }

        public void RetryGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
