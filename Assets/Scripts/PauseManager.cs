using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : IPausable
{
    public bool IsPaused;

    private List<IPausable> _pausableList = new List<IPausable>();

    public void Register(IPausable pausable)
    {
        _pausableList.Add(pausable);
    }

    public void UnRegister(IPausable pausable)
    {
        _pausableList.Remove(pausable);
    }

    public void SetPause(bool isPause)
    {
        IsPaused = isPause;
        foreach (var item in _pausableList)
        {
            item.SetPause(isPause);
        }
    }
}
