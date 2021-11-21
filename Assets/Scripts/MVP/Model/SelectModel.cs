using System;
using UnityEngine;

[CreateAssetMenu(fileName ="SelectModel",menuName = "My/SelectModel")]
public class SelectModel : ScriptableObject
{
    public event Action<ISelectable> evtSelect;
    public event Action evtDeSelectAll;
    public int CountSelect { get; private set; }
    public int CountUI { get; private set; }

    public void Select(ISelectable selectable)
    {
        evtSelect?.Invoke(selectable);
        if (selectable.IsGroupSelectable)
        {
            if (selectable.IsSelect) CountSelect++; else CountSelect--;
        }
    }
    public void DeselectAll()
    {
        evtDeSelectAll?.Invoke();
        CountSelect = 0;
    }

    public void MouseOnUI()
    {
        CountUI++;
    }
    public void MouseOffUI()
    {
        CountUI--;
    }


}
