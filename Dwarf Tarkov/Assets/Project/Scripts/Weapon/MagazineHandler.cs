using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineHandler : MonoBehaviour
{
    private WeaponData weaponData;
    private float timeInGravity;
    private const float lifetime = 60f;
    private float timer;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        timeInGravity = Random.Range(0f, 2f);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeInGravity)
        {
            rb.gravityScale = 0f;
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetData(WeaponData data)
    {
        weaponData = data;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        spriteRenderer.sprite = weaponData.MagazineSprite;
    }
}
