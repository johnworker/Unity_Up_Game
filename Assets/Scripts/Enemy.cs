using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Enemy : MonoBehaviour
    {
        [Header("�Ǫ��W�r")]
        public string enemyName;
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
                rigid.AddForce(Vector2.up, ForceMode2D.Impulse);
            }
            else if(enemyName == "L")
            {

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