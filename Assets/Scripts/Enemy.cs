using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Enemy : MonoBehaviour
    {
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

        SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnHit(int dmg)
        {
            health -= dmg;
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSpirt", 0.1f);

            if(health <= 0)
            {
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