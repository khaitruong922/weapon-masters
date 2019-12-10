using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    private Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    public float speed=5f;
    public float maxHP=200f;
    public float currentHP;
    public Image healthBar;
    public Vector2 lookDir;
    void Start()
    { 
        currentHP = maxHP;
        cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
    }
       void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //get mouse position
    }
    void FixedUpdate() { //make character facees toward mouse position
        rb.MovePosition(rb.position+ movement * speed * Time.fixedDeltaTime);
        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
    }
    public void TakeDamage(float damage){
        currentHP -= damage;
        if (currentHP <= 0){
            Die();
        }
        healthBar.fillAmount = PercentHP();
    }
    public void Heal(float healAmount){
        currentHP += healAmount;
        if (currentHP >= maxHP){
            currentHP = maxHP;
        }
        healthBar.fillAmount = PercentHP();
    }
    void Die(){
        Destroy(gameObject);
        GameHandler.Instance.Defeat();
        Time.timeScale = 0;
    }
    public float PercentHP(){
        return(float) currentHP/maxHP;
    }
}
