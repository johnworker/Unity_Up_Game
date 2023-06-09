using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Enemy : MonoBehaviour
    {
        [Header("怪物名字")]
        public string enemyName;
        [Header("敵人分數")]
        public int enemyScore;
        [Header("速度")]
        public float speed;
        [Header("血量")]
        public int health;
        [Header("狀態圖")]
        public Sprite[] sprites;

        [Header("最大射擊延遲")]
        public float maxShotDelay;
        [Header("當前射擊延遲")]
        public float curShotDelay;

        [Header("子彈A")]
        public GameObject bulletObjA;
        [Header("子彈B")]
        public GameObject bulletObjB;
        public GameObject player;

        SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Fire();
            Reload();
        }

        void Fire()
        {
            if (curShotDelay < maxShotDelay)
                return;

            if(enemyName == "S")
            {
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector3 dirVec = player.transform.position - transform.position;
                rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
            }
            else if(enemyName == "L")
            {
                GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
                Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
                rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
                rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
            }

            curShotDelay = 0;
        }

        void Reload()
        {
            curShotDelay += Time.deltaTime;
        }

        void OnHit(int dmg)
        {
            health -= dmg;
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSpirt", 0.1f);

            if(health <= 0)
            {
                Player playerLogic = player.GetComponent<Player>();
                playerLogic.score += enemyScore;
                Destroy(gameObject);
            }
        }

        void ReturnSpirt()
        {
            spriteRenderer.sprite = sprites[0];
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "BorderBullet")
                Destroy(gameObject);
            else if (collision.gameObject.tag == "PlayerBullet")
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnHit(bullet.dmg);

                Destroy(collision.gameObject);
            }
        }
    }
}