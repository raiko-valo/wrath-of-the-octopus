using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AttackCollision SectorAttackPrefab;
    public AttackCollision CircleAttackPrefab;
    public AttackCollision BulletAttackPrefab;

    public float NextAttack;
    public float CooldownAttack;

    public float NextSectorAttack;
    public float CooldownSectorAttack;
    public float DurationSecotrAttack;

    public float NextCircleAttack;
    public float CooldownCircleAttack;
    public float DurationCircleAttack;

    public float NextBulletAttack;
    public float CooldownBulletAttack;
    public float DurationBulletAttack;

    public float BaseRadius;

    public Animator animator;
    public AudioClipGroup audioClipSword;
    public AudioClipGroup audioClipShot;

    private AttackType attackType;
    private AttackCollision attackMove;

    
    private enum AttackType
    {
        circle,
        sector,
        shoot
    }

    void Start()
    {
        NextSectorAttack = Time.time;
    }

    void Update()
    {
        if (Time.time >= NextAttack && attackMove == null)
        {
            if (Input.GetKeyDown(KeyCode.Q) && Time.time >= NextCircleAttack)
            {
                animator.SetTrigger("Attack");
                audioClipSword.Play();
                attackType = AttackType.circle;
                ToggleCircleCollider();
            }

            if (Input.GetKeyDown(KeyCode.W) && Time.time >= NextSectorAttack)
            {
                animator.SetTrigger("Attack");
                audioClipSword.Play();
                attackType = AttackType.sector;
                ToggleSectorCollider();
            }

            if (Input.GetKeyDown(KeyCode.E) && Time.time >= NextBulletAttack)
            {
                animator.SetTrigger("Attack");
                audioClipShot.Play();
                attackType = AttackType.shoot;
                ToggleBulletCollider();
            }
        }
        

        if (attackMove != null)
        {
            if (attackType != AttackType.shoot)
            {
                attackMove.transform.position = transform.position;
            }

            if (Time.time >= NextAttack)
            {
                switch (attackType)
                {
                    case AttackType.circle:
                        NextCircleAttack = Time.time + CooldownCircleAttack;
                        attackMove.EndAttackMove();
                        break;
                    case AttackType.sector:
                        NextSectorAttack = Time.time + CooldownSectorAttack;
                        attackMove.EndAttackMove();
                        break;
                    case AttackType.shoot:
                        NextBulletAttack = Time.time + CooldownBulletAttack;
                        break;
                }

                NextAttack = Time.time + CooldownAttack;
            }
        }
    }

    void ToggleAttack(AttackCollision attackPrefab, float attackDuration)
    {
        if (attackPrefab != null)
        {
            animator.SetTrigger("Attack");
            attackMove = Instantiate<AttackCollision>(attackPrefab);
            attackMove.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = (mousePosition - attackMove.transform.position).normalized;

            if (attackType == AttackType.shoot)
            {
                Bullet bullet = attackMove.GetComponent<Bullet>();
                bullet.Angle = new Vector3(direction.x, direction.y, 0);
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            attackMove.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            NextAttack = Time.time + attackDuration;
        }
    }

    void ToggleCircleCollider()
    {
        ToggleAttack(CircleAttackPrefab, DurationCircleAttack);
        NextSectorAttack = Time.time + DurationSecotrAttack;
    }

    void ToggleSectorCollider()
    {
        ToggleAttack(SectorAttackPrefab, DurationSecotrAttack);
        NextSectorAttack = Time.time + DurationSecotrAttack;
    }

    void ToggleBulletCollider()
    {
        ToggleAttack(BulletAttackPrefab, DurationBulletAttack);
        NextBulletAttack = Time.time + DurationBulletAttack;
        attackMove = null;
    }


    //void OnDrawGizmos()
    //{
    //    // Draw a circle in the Scene view when the object is selected
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, baseRadius);
    //}
}
