using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public int health;

        SpriteRenderer spriteRenderer;
        Rigidbody2D rigid;
        public Sprite[] sprites;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.down * speed;
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
            }
        }
    }
}