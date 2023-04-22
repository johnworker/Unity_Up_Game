using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Enemy : MonoBehaviour
    {
        [Header("�t��")]
        public float speed;
        [Header("��q")]
        public int health;
        [Header("���A��")]
        public Sprite[] sprites;

        [Header("�̤j�g������")]
        public float maxShotDelay;
        [Header("��e�g������")]
        public float curShotDelay;

        [Header("�l�uA")]
        public GameObject bulletObjA;
        [Header("�l�uB")]
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