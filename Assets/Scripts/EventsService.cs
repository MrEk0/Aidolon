using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventsService : MonoBehaviour
{
    [SerializeField] private ServerController _serverController;
    [SerializeField] private string _serverURL = string.Empty;
    [Min(0f)] [SerializeField] private float _cooldownBeforeSend = 0f;
    [SerializeField] private string _appropriateResponseString = "200 OK";

    private readonly Dictionary<string, string> _events = new Dictionary<string, string>();

    private float _sendTimer = 0f;
    private bool _isTimerActive = false;
    [CanBeNull]
    private SaveSystem _saveSystem;

    private void Start()
    {
        _saveSystem = new SaveSystem();
        
        foreach (var analyticsEvent in _saveSystem.AnalyticsEvents)
        {
            _events.Add(analyticsEvent.Key, analyticsEvent.Value);
        }

        if (_events.Count > 0)
            TryToSend();
    }

    private void OnDestroy()
    {
        _saveSystem?.Save();
    }

    private void Update()
    {
        if (!_isTimerActive)
            return;

        _sendTimer += Time.deltaTime;

        if (_sendTimer <= _cooldownBeforeSend)
            return;
        
        TryToSend();
            
        _sendTimer = 0f;
    }

    public void TrackEvent(string type, string data)
    {
        _events.Add(type, data);
        _saveSystem?.AddEvent(type, data);

        _isTimerActive = true;
    }

    private void TryToSend()
    {
        _serverController.SendData(_serverURL, _events, result =>
        {
            if (!result.Contains(_appropriateResponseString))
                return;
            
            _events.Clear();
            _saveSystem?.ClearEvents();

            _isTimerActive = false;
            
        }, error =>
        {
            Debug.LogWarning($"{error}");
        });
    }
    
}

