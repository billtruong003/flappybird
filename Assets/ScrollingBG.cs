using System.Collections;
using System.Collections.Generic;
using Flappy.Data;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    [SerializeField] private Material bgMaterial;
    [SerializeField] private float scrollSpeed = 0.5f; 

    private float offsetX = 0f;

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.Playing)
            return;
            
        offsetX += scrollSpeed * Time.deltaTime;
        if (offsetX >= 1)
        {
            offsetX = 0;
        }
        bgMaterial.mainTextureOffset = new Vector2(offsetX, 0);
    }
}
