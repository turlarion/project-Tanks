using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _player_tank : _class_tank
{

    void Move()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            MoveW = true;
            MoveA = false;
            MoveS = false;
            MoveD = false;
            rb.transform.rotation = Quaternion.Euler(0, 0, 0f);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveW = false;
            MoveA = true;
            MoveS = false;
            MoveD = false;
            rb.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveW = false;
            MoveA = false;
            MoveS = true;
            MoveD = false;
            rb.transform.rotation = Quaternion.Euler(0, 0, 180.0f);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveW = false;
            MoveA = false;
            MoveS = false;
            MoveD = true;
            rb.transform.rotation = Quaternion.Euler(0, 0, 270.0f);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && MoveW)
        {
            MoveForvard();
        }

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)))
        {
            Stop();
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && MoveA)
        {
            MoveLeft();
        }

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)))
        {
            Stop();
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && MoveS)
        {
            MoveBack();
        }

        if ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)))
        {
            Stop();
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && MoveD)
        {
            MoveRight();
        }

        if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
        {
            Stop();
        }
    }

    public int hp;

    void Update()
    {

        //Обработка движения
        Move();

        //Выстрел

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        reloadTimeCounter -= Time.fixedDeltaTime;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Enemy")
        {
            hp -= 3;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Enemy Bullet")
        {
            hp -= 1;
        }

        if (obj.gameObject.tag == "Sup Bullet")
        {
            hp -= 3;
        }
    }

}
