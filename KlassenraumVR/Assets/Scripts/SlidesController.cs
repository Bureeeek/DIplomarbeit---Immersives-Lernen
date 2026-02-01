using UnityEngine;

public class SlidesController : MonoBehaviour
{
    public Renderer slideRenderer; // Das Mesh Renderer-Objekt deiner Leinwand
    public string slideFolder = "Slides";
    public int slideCount = 3; // Anzahl der Slides
    private int idx = 0;
    private Texture2D[] slides;

    void Start()
    {
        slides = new Texture2D[slideCount];
        for (int i = 0; i < slideCount; i++)
        {
            string path = slideFolder + "/slide_" + (i + 1).ToString("00");
            slides[i] = Resources.Load<Texture2D>(path);
        }
        ShowCurrentSlide();
    }
    public void Next()
    {
        idx = (idx + 1) % slideCount;
        ShowCurrentSlide();
    }
    public void Prev()
    {
        idx = (idx - 1 + slideCount) % slideCount;
        ShowCurrentSlide();
    }
    void ShowCurrentSlide()
    {
        slideRenderer.material.mainTexture = slides[idx];
    }
}
