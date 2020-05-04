using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMove : MonoBehaviour
{
    public float speed;
    Vector3 pos;
    Vector3 prePos;

    public float StealthLength;

    bool Cloak = false;

    SpriteRenderer sr;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
        sr = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        if (transform.position == pos)
        {
            if (Input.GetButtonDown("up")) pos.y += 4;

            if (Input.GetButtonDown("down")) pos.y -= 4;

            if (Input.GetButtonDown("left")) pos.x -= 4;

            if (Input.GetButtonDown("right")) pos.x += 4;

            prePos = pos;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("sneak"))
        {
            StartCoroutine(Stealth());
        }
    }

    IEnumerator Stealth()
    {
        Cloak = true;
        sr.color = Color.blue;
        yield return new WaitForSeconds(StealthLength);
        sr.color = Color.white;
        Cloak = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Beam") && Cloak == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            pos = prePos;
        }
    }

}
