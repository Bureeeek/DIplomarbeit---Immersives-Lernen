using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug; // <— hinzufügen


[RequireComponent(typeof(Collider))]   // Für OnMouseEnter/Exit nötig
public class SeatHighlighter : MonoBehaviour
{
    public string seatName = "A1";
    public GameObject labelPrefab;

    private Renderer rend;
    private GameObject labelInstance;

    void Awake()
    {
        // Renderer irgendwo am Objekt oder Kindern suchen
        rend = GetComponent<Renderer>();
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        if (rend == null)
            Debug.LogWarning($"SeatHighlighter: Kein Renderer gefunden auf '{name}'. Highlight-Farbe wird deaktiviert.");
    }

    void Start()
    {
        if (labelPrefab != null)
        {
            labelInstance = Instantiate(labelPrefab, transform);
            var tmp = labelInstance.GetComponentInChildren<TextMeshPro>();
            if (tmp) tmp.text = seatName;
            labelInstance.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        if (rend) rend.material.color = Color.cyan;
        if (labelInstance) labelInstance.SetActive(true);
    }

    void OnMouseExit()
    {
        if (rend) rend.material.color = Color.white;
        if (labelInstance) labelInstance.SetActive(false);
    }
}
