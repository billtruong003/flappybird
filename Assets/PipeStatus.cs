using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeStatus : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed = 3;
    [SerializeField] private bool isScrolling;
    public bool IsScrolling => isScrolling;
    public void SetSpeed(float speed) => scrollingSpeed = speed;
    public void Scroll() => isScrolling = true;
    private void Update()
    {
        if (isScrolling && transform.position.x >= -3)
        {
            transform.Translate(Vector3.left * scrollingSpeed * Time.deltaTime);
        }
        else
        {
            isScrolling = false;
            transform.position = new Vector3(6.5f, 0, 0);
        }
    }

    public void SetRandomHeight()
    {
        float randomY = Random.Range(2f, -2f);
        transform.position = new Vector3(6.5f, randomY, 0);
    }
}
