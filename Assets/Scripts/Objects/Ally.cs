using TMPro;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] private GameObject _clue;
    [SerializeField] private bool _isJustEvent; // true - не бомба, false - бомба.
    [SerializeField] [TextArea] private string _textToSay;
    [SerializeField] private LayerMask _layerOfClue;

    private EventsManager _eventsManager;
    private GameObject _dialogueWindow;
    private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _dialogueWindow = GameObject.FindWithTag("DialogueWindow");
        _textMeshPro = GameObject.FindWithTag("TMP").GetComponent<TextMeshProUGUI>();
        _eventsManager = GameObject.FindWithTag("EventsManager").GetComponent<EventsManager>();

        _dialogueWindow.SetActive(false);

        SpawnEvent();
        
        ChangeClueLayer(10);
        ChangeText();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            ChangeClueLayer(_layerOfClue);
            ChangeText();
        }
    }

    private void SpawnEvent()
    {
        if (_isJustEvent)
        {
            Debug.Log($"Position: {_eventsManager.eventsPosition[0]}");
            Vector3 spawnPos = _eventsManager.eventsPosition[0];
            _eventsManager.eventsPosition.RemoveAt(0);
            Instantiate(_clue, spawnPos, Quaternion.identity);
        }
    }
    
    private void ChangeClueLayer(LayerMask layer)
    {
        _clue.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("OnGameAndOnMap");
        Debug.Log($"Layer - {layer}");
        Debug.Log($"Name - {_clue.transform.GetChild(0).gameObject.name}");
    }

    private void ChangeText()
    {
        _dialogueWindow.SetActive(true);
        _textMeshPro.text = _textToSay;
    }
}
