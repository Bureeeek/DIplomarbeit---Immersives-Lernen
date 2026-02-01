

using UnityEngine;

public class MeetingZone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject infoPanel;

    [Header("Filter (optional)")]
    public bool useTagFilter = false;
    public string requiredTag = "Player";

    private void Awake()
    {
        // Panel beim Start verstecken
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsValid(other)) return;

        if (infoPanel != null)
            infoPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsValid(other)) return;

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    private bool IsValid(Collider other)
    {
        if (!useTagFilter) 
            return true;

        return other.CompareTag(requiredTag);
    }
}
