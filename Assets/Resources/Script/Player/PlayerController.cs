using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized]
    public Rigidbody2D rb2D;
    public GameObject container, skillCastAt;
    Animator anim;
    PlayerStats playerStats;
    public bool isJumping, isAttacking, isGrounded;
    // bool isSkillCD;
    float moveHorizontal, CDSkillContainer;
    public float jumpForce = 20f, moveSpeed = 5f;
    public int skillMana = 30;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
        /*Change the gravity scale*/
        rb2D.gravityScale = 3f;

        CDSkillContainer = playerStats.skillCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.isAlive)
        {
            Run();
            Jump();
            Attack();
        }
        if (transform.position.y <= -100f)
        {
            playerStats.health = 0;
        }
    }

    void Run()
    {
        moveHorizontal = isAttacking ? 0 : Input.GetAxis("Horizontal"); // prevent player to flip while attacking
        rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);

        anim.SetBool("isRun", moveHorizontal != 0 && !isJumping && isGrounded ? true : false);
        if (moveHorizontal > 0) // flip right
        {
            transform.localScale = new Vector2(transform.localScale.x > 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);
            playerStats.knockbackPower = playerStats.knockbackPower < 0 ? -playerStats.knockbackPower : playerStats.knockbackPower;
        }
        else if (moveHorizontal < 0) // flip left
        {
            transform.localScale = new Vector2(transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y);
            playerStats.knockbackPower = playerStats.knockbackPower > 0 ? -playerStats.knockbackPower : playerStats.knockbackPower;
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        isJumping = rb2D.velocity.y > 0 && !isGrounded; // return boolean untuk isJumping
        anim.SetBool("isJump", isJumping);
        anim.SetBool("isFall", !isGrounded);

    }

    void Attack()
    {
        isAttacking = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill");
        if (!isAttacking && isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack"); // normal attack
            }
            if (Input.GetKeyDown(KeyCode.E) /*&& !isSkillCD */&& playerStats.mana > skillMana)
            {
                anim.SetTrigger("skill"); // launch skill
                // isSkillCD = true;
                playerStats.mana -= skillMana;
            }
        }
        // if (isSkillCD)
        // {
        //     StartCoroutine(SkillCooldown());
        // }
        rb2D.constraints = isAttacking ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation; // prevent player can move while attacking
    }
    void Skill(GameObject magicSkill) // method for Animation Event
    {
        if (!magicSkill.GetComponent<Skill>())
        {
            magicSkill.AddComponent<Skill>();
        }
        if (transform.localScale.x > 0)
        {
            Instantiate(magicSkill, skillCastAt.transform.position, Quaternion.identity, container != null ? container.transform : null);
        }
        else if (transform.localScale.x < 0)
        {
            Instantiate(magicSkill, skillCastAt.transform.position, Quaternion.identity, container != null ? container.transform : null);
        }

    }
    // IEnumerator SkillCooldown()
    // {
    //     playerStats.skillCD = playerStats.skillCD - Time.deltaTime;
    //     yield return new WaitUntil(() => playerStats.skillCD <= 0);
    //     playerStats.skillCD = CDSkillContainer;
    //     yield return null;
    //     Debug.Log("Skill not CD");
    //     isSkillCD = false;
    // }

}
