using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Toko : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    [SerializeField] float jumpForce = 300f;
    readonly int limitJumpcount = 2;
    int jumpcount = 0;
    // Update is called once per frame
    void Update()
    {
        if (!rigid) return;

        if(Input.GetKeyDown(KeyCode.Space) && limitJumpcount> jumpcount)
        {
            jumpcount++;

            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpForce);
        }
        else if(Input.GetKeyUp(KeyCode.Space)&& 0<rigid.velocity.y)
        {
            rigid.velocity *= 0.5f;
        }

        if (GameMgr.Instance.isDead || !rigid) return;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //jumpcount = 0;

        if(collision.contacts[0].normal.y >0.8f)
        {
            if (anim) anim.SetBool("isGround", true);
            jumpcount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (anim) anim.SetBool("isGround", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("DeadZone"))
        {
            if (rigid) rigid.simulated = false;
            if (anim) anim.SetTrigger("isDie");
            GameMgr.Instance.OnDie();
        }
    }

    private void GameOver()
    {
        GameMgr.Instance.GameOver();
    }

}
