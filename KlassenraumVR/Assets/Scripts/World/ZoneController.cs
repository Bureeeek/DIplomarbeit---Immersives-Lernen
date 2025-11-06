using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [Header("References")]
    public GameObject infoPanelPrefab;   // Prefab/InfoPanel.prefab
    public Transform playerOverride;     // leer lassen → nimmt Camera.main
    public AudioSource enterSfx;         // optional

    [Header("Settings")]
    public float panelHeight = 1.7f;
    public bool faceCamera = true;
    public float fallbackRadius = 1.2f;  // falls Trigger nicht feuert
    public bool useDistanceFallback = true;

    private GameObject _panel;
    private Transform _player;
    private bool _inside;

    void Start()
    {
        _player = playerOverride != null ? playerOverride :
                  (Camera.main != null ? Camera.main.transform : null);

        if (infoPanelPrefab != null)
        {
            _panel = Instantiate(infoPanelPrefab, transform);
            _panel.transform.localPosition = new Vector3(0, panelHeight, 0);
            _panel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other.transform)) Show(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other.transform)) Show(false);
    }

    bool IsPlayer(Transform t)
    {
        if (_player == null) return false;
        return t == _player || t.IsChildOf(_player) || t.CompareTag("Player");
    }

    void Update()
    {
        if (useDistanceFallback && _player != null)
        {
            var a = new Vector3(_player.position.x, 0, _player.position.z);
            var b = new Vector3(transform.position.x, 0, transform.position.z);
            bool shouldBeInside = Vector3.Distance(a, b) <= fallbackRadius;
            if (shouldBeInside != _inside) Show(shouldBeInside);
        }

        if (faceCamera && _panel != null && _panel.activeSelf && _player != null)
        {
            var look = _player.position - _panel.transform.position;
            look.y = 0f;
            if (look.sqrMagnitude > 0.0001f)
                _panel.transform.rotation = Quaternion.LookRotation(look);
        }
    }

    void Show(bool on)
    {
        _inside = on;
        if (_panel != null) _panel.SetActive(on);
        if (on && enterSfx != null) enterSfx.Play();
    }
}
