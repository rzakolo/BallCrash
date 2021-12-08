using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int point, lastRecord, health = 100;
    private SaveManager save;
    public event Action<int> OnPointChanged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnRecordChanged;
    public event Action OnLoseGame;
    public event Action OnPause;
    public event Action OnResume;
    private void Awake()
    {
        save = new SaveManager();
        OnRecordChanged += save.SetRecord;
    }
    private void Start()
    {
        LoadLastRecord();
    }

    public void AddPoint(int point)
    {
        this.point += point;
        OnPointChanged?.Invoke(this.point);
    }
    public void ApplyDamage(int value)
    {
        health -= value;
        if (health <= 0)
            OnLoseGame?.Invoke();
        OnHealthChanged?.Invoke(health);
    }
    public void ChangeLastRecord()
    {
        lastRecord = point;
        OnRecordChanged?.Invoke(lastRecord);
    }
    private void LoadLastRecord()
    {
        lastRecord = save.Record;
        OnRecordChanged?.Invoke(lastRecord);
    }
    private void OnApplicationQuit()
    {
        ChangeLastRecord();
        OnRecordChanged -= save.SetRecord;
    }
    public void Pause()
    {
        OnPause?.Invoke();
    }
    public void Resume()
    {
        OnResume?.Invoke();
    }
}
