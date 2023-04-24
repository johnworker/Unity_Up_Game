using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Player : MonoBehaviour
    {
        [Header("--- 觸碰界線 ---")]
        public bool isTouchTop;
        public bool isTouchBottom;
        public bool isTouchRight;
        public bool isTouchLeft;

        [Header("生命")]
        public int life;
        [Header("分數")]
        public int score;
        [Header("速度")]
        public float speed;
        [Header("力量")]
        public float power;
        [Header("最大射擊延遲")]
        public float maxShotDelay;
        [Header("當前射擊延遲")]
        public float curShotDelay;

        [Header("子彈A")]
        public GameObject bulletObjA;
        [Header("子彈B")]
        public GameObject bulletObjB;

        public GameManager manager;
        public bool isHit;
        Animator anim;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Move();
            Fire();
            Reload();
        }

        void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
                h = 0;

            float v = Input.GetAxisRaw("Vertical");
            if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
                v = 0;

            Vector3 curPos = transform.position;
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

            transform.position = curPos + nextPos;

            if (Input.GetButtonDown("Horizontal") ||
               Input.GetButtonUp("Horizontal"))
            {
                anim.SetInteger("Input", (int)h);
            }
        }

        void Fire()
        {
            if (!Input.GetButton("Fire1"))
                return;

            if (curShotDelay < maxShotDelay)
                return;

            switch (power)
            {
                // 力量
                case 1:
                    GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.up, ForceMode2D.Impulse);
                    break;
                case 2:
                    GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.2f, transform.rotation);
                    GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.2f, transform.rotation);
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up, ForceMode2D.Impulse);
                    rigidL.AddForce(Vector2.up, ForceMode2D.Impulse);
                    break;
                case 3:
                    GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                    GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                    GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
                    Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                    rigidRR.AddForce(Vector2.up, ForceMode2D.Impulse);
                    rigidCC.AddForce(Vector2.up, ForceMode2D.Impulse);
                    rigidLL.AddForce(Vector2.up, ForceMode2D.Impulse);
                    break;
            }


            curShotDelay = 0;
        }

        void Reload()
        {
            curShotDelay += Time.deltaTime;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Border")
            {
                switch (collision.gameObject.name)
                {
                    case "Top":
                        isTouchTop = true;
                        break;

                    case "Bottom":
                        isTouchBottom = true;
                        break;

                    case "Right":
                        isTouchRight = true;
                        break;

                    case "Left":
                        isTouchLeft = true;
                        break;
                }
            }
            else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
            {
                if (isHit)
                    return;

                isHit = true;

                life--;
                manager.UpdateLifeIcon(life);

                if(life == 0)
                {
                    manager.GameOver();
                }
                else
                {
                    manager.RespawnPlayer();
                }

                gameObject.SetActive(false);
                Destroy(collision.gameObject);
            }
            else if(collision.gameObject.tag == "Item")
            {
                Item item = collision.gameObject.GetComponent<Item>();
                switch (item.type)
                {
                    case "Coin":
                        break;
                    case "Power":
                        break;
                    case "Boom":
                        break;
                }
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Border")
            {
                switch (collision.gameObject.name)
                {
                    case "Top":
                        isTouchTop = false;
                        break;

                    case "Bottom":
                        isTouchBottom = false;
                        break;

                    case "Right":
                        isTouchRight = false;
                        break;

                    case "Left":
                        isTouchLeft = false;
                        break;
                }
            }
        }
    }
}