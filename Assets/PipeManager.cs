using System.Collections;
using System.Collections.Generic;
using Flappy.Data;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private PipeStatus[] pipes;
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private float speedIncreaseInterval = 3.0f;
    [SerializeField] private float speedIncrement = 0.05f;

    void Start()
    {
        pipes = gameObject.GetComponentsInChildren<PipeStatus>();
        if (pipes == null || pipes.Length == 0)
        {
            Debug.LogError("No pipes found! Ensure that PipeStatus components are correctly attached.");
            return;
        }
        StartCoroutine(ScrollingLogic());
        StartCoroutine(IncreaseScrollSpeed());
    }

    private IEnumerator ScrollingLogic()
    {
        while (true)
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is null. Ensure that GameManager is initialized.");
                yield break;
            }

            if (GameManager.Instance.GameState == GameState.Playing)
            {
                bool anyPipeScrolled = false;
                foreach (var pipe in pipes)
                {
                    if (!pipe.IsScrolling)
                    {
                        pipe.SetRandomHeight();
                        pipe.SetSpeed(scrollSpeed);
                        pipe.Scroll();
                        anyPipeScrolled = true;
                        break;
                    }
                }

                if (!anyPipeScrolled)
                {
                    Debug.LogWarning("No pipe available to scroll.");
                }

                yield return new WaitForSeconds(3);
            }
            else
            {
                yield return null; 
            }
        }
    }

    private IEnumerator IncreaseScrollSpeed()
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.GameState == GameState.Playing)
            {
                scrollSpeed += speedIncrement;
                Debug.Log($"Scroll speed increased to {scrollSpeed}");
            }
            yield return new WaitForSeconds(speedIncreaseInterval);
        }
    }
}
