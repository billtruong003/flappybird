using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Flappy.Data;
using UnityEngine.Animations.Rigging;

public class FlappyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject smokeFX;
    [SerializeField] private GameObject explosionFX;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.GameState != GameState.Playing)
            return;
            
        Debug.Log("Triggered with: " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            explosionFX.SetActive(true);
            rb.bodyType = RigidbodyType2D.Static;
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            GameManager.Instance.IncreaseScore(1);
        }
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.Playing)
            return;

        Jump();
        if (rb.velocity.y < 0)
        {
            smokeFX.SetActive(false);
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.up * jumpForce;
            smokeFX.SetActive(true);
            animator.SetTrigger("Flap");
        }
    }

    public void Init(GameState state)
    {
        SetRB(state);
        switch (state)
        {
            case GameState.MainMenu:
                animator.SetTrigger("AnimStatic");
                break;
            case GameState.Playing:
                animator.SetTrigger("Flap");
                break;
            case GameState.GameOver:
                ResetToInitialState(); 
                break;
        }
    }   

    private void ResetToInitialState()
    {
        transform.position = new Vector3(-1.5f, 0, 0);
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = true;
        }
        explosionFX.SetActive(false);
        smokeFX.SetActive(false);
    }

    [SerializeField] public void SetRB(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                rb.bodyType = RigidbodyType2D.Static;
                break;
            case GameState.Playing:
                rb.bodyType = RigidbodyType2D.Dynamic;
                break;
            case GameState.GameOver:
                rb.bodyType = RigidbodyType2D.Static;
                break;
        }
    }
}
