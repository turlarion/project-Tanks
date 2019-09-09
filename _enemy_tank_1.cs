using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
namespace System.IO
{

    public class _enemy_tank_1 : _class_tank
    {
        protected float X, Y;

        protected bool isShooting = false;
        protected bool BasePoint = false;
        protected int direction;
        public int scorecost = 100;

        protected Random rnd = new Random();

        protected List<Vector2> ListRnd = new List<Vector2>();
        protected List<Vector3> ListLaw = new List<Vector3>();

        protected GameObject player;
        protected GameObject ctr;
        protected GameObject[] walls;
        protected Rigidbody2D pl;

        protected void Dir()
        {
            if (direction == 1)
            {
                MoveForvard();
            }
            else if (direction == 2)
            {
                MoveRight();
            }
            else if (direction == 3)
            {
                MoveBack();
            }
            else
            {
                MoveLeft();
            }
        }

        protected void ChangeDir()
        {
            int old = direction;
            do
            {
                direction = rnd.Next(1, 5);
            } while (old == direction);

            Dir();
        }

        protected bool TriggerPlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pl = player.GetComponent<Rigidbody2D>();

            if ((TriggerPlayerX() || TriggerPlayerY()))
            {
                return true;
            }
            else return false;
        }

        protected bool TriggerPlayerX()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pl = player.GetComponent<Rigidbody2D>();
            if (Math.Abs(pl.position.x - rb.position.x) <= 20f)
            {
                return true;
            }
            return false;
        }

        protected bool TriggerPlayerY()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            pl = player.GetComponent<Rigidbody2D>();
            if (Math.Abs(pl.position.y - rb.position.y) <= 20f)
            {
                return true;
            }
            else return false;
        }

        protected void Move()
        {

            if (!TriggerPlayer())
            {
                isShooting = false;

                if (rb.velocity == new Vector2(0f, 0f) && !BasePoint)
                {
                    ChangeDir();
                }

                foreach (Vector2 v in ListRnd)
                {
                    Vector2 pos = transform.position;
                    if (rb.position.x == (v.x + X) && rb.position.y == (v.y + Y))
                    {
                        ChangeDir();
                    }
                }

                foreach (Vector3 v in ListLaw)
                {
                    Vector2 pos = transform.position;
                    if (rb.position.x == (v.x + X) && rb.position.y == (v.y + Y))
                    {

                        if (v.z <= 5)
                        {
                            direction = Convert.ToInt32(v.z);
                            Dir();
                        }
                        else if (v.z == 5)
                        {
                            int t = rnd.Next(0, 2);
                            if (t == 1) direction = 2; else direction = 4;
                            Dir();
                        }
                        else if (v.z == 6)
                        {
                            direction = rnd.Next(2, 4);
                            Dir();
                        }
                        else
                        {
                            direction = rnd.Next(3, 5);
                            Dir();
                        }

                    }
                }
            }
            else if (TriggerPlayer())
            {
                isShooting = true;
                if (TriggerPlayerX())
                {
                    if (pl.position.x > rb.position.x)
                    {
                        MoveBack();
                    }
                    else if (pl.position.x > rb.position.x)
                    {
                        MoveForvard();
                    }
                }

                if (TriggerPlayerY())
                {
                    if (pl.position.y > rb.position.y)
                    {
                        MoveRight();
                    }
                    else if (pl.position.y < rb.position.y)
                    {
                        MoveLeft();
                    }
                }

            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D obj)
        {
            if (obj.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                _score.score += scorecost;
            }

            if (obj.gameObject.tag == "Brick" || obj.gameObject.tag == "Edges" || obj.gameObject.tag == "Enemy")
            {
                ChangeDir();
            }

            if (obj.gameObject.tag == "Base Brick")
            {
                Stop();
                isShooting = true;
                BasePoint = true;
            }

        }

        protected virtual void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.gameObject.tag == "PlayerBullet")
            {

                _score.score += scorecost;
                Destroy(gameObject);
            }
        }

        protected virtual bool Check()
        {
            float dist = Math.Abs((rb.position.x * rb.position.x + rb.position.y * rb.position.y) - (pl.position.x * pl.position.x + pl.position.y * pl.position.y));

            if (dist <= 100)
            {
                return true;
            }
            else return false;
        }

        protected virtual void Update()
        {

            if (isShooting)
            {
                Shoot();
            }
            reloadTimeCounter -= Time.fixedDeltaTime;
            Move();
        }

        protected override void Awake()
        {
            base.Awake();

            ctr = GameObject.FindGameObjectWithTag("Controller");
            Rigidbody2D rrr = ctr.GetComponent<Rigidbody2D>();
            X = rrr.position.x;
            Y = rrr.position.y;

            string s;

            StreamReader f1 = new StreamReader("RndLvl" + SceneManager.GetActiveScene().buildIndex + ".txt");
            while ((s = f1.ReadLine()) != null)
            {
                float x = Convert.ToSingle(s);
                float y = Convert.ToSingle(f1.ReadLine());
                Vector2 v = new Vector2(x, y);
                ListRnd.Add(v);
            }
            f1.Close();


            StreamReader f2 = new StreamReader("LawLvl" + SceneManager.GetActiveScene().buildIndex + ".txt");
            while ((s = f2.ReadLine()) != null)
            {
                float x = Convert.ToSingle(s);
                float y = Convert.ToSingle(f2.ReadLine());
                float d = Convert.ToSingle(f2.ReadLine());
                Vector3 v = new Vector3(x, y, d);
                ListLaw.Add(v);
            }
            f2.Close();

            direction = rnd.Next(2, 5);
            Dir();

        }

    }
}
