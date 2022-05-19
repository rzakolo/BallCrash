using System;
using UnityEngine;

public class GameManager
{
    private int point, lastRecord, health = 100;
    private bool _loseGame = false;
    private SaveManager _save;
    public event Action<int> OnPointChanged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnRecordChanged;
    public event Action OnLoseGame;
    public GameManager(SaveManager saveManager)
    {
        _save = saveManager;
        OnRecordChanged += _save.SetRecord;
        LoadLastRecord();
    }

    public void AddPoint(int point)
    {
        this.point += point;
        OnPointChanged?.Invoke(this.point);
    }
    public void DoublePoints()
    {
        point *= 2;
        OnPointChanged?.Invoke(point);
    }
    public void ApplyDamage(int value)
    {
        health -= value;
        if (health <= 0 && !_loseGame)
        {
            _loseGame = true;
            OnLoseGame?.Invoke();
        }
        OnHealthChanged?.Invoke(health);
    }
    public void ChangeLastRecord()
    {
        lastRecord = point;
        OnRecordChanged?.Invoke(lastRecord);
    }
    public void LoadLastRecord()
    {
        lastRecord = _save.Record;
        OnRecordChanged?.Invoke(lastRecord);
    }
    private void OnApplicationQuit()
    {
        ChangeLastRecord();
        OnRecordChanged -= _save.SetRecord;
    }
}
