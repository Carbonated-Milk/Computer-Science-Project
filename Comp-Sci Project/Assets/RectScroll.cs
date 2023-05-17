using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectScroll : MonoBehaviour
{
    public Vector2 scrollSpeed;
    private RawImage image;
    private void Awake()
    {
        image = GetComponent<RawImage>();
    }
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + scrollSpeed * Time.unscaledDeltaTime, image.uvRect.size);
    }
}
