using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Core.AutomationEntities;

public class ExecutableItem
{
    public string Id;
    private const string _defaultKey = "default";
    private string _currentKey;
    private Dictionary<string, ExecutableItem> _subItems;

    public ExecutableItem(string id)
    {
        Id = id;
        _subItems = new();
    }

    public void StartExecution()
    {

    }

    public void StopExecution()
    {
    }

    public ExecutableItem AddItem(string id)
    {
        var item = new ExecutableItem(id);
        _subItems.TryAdd(id, item);
        _currentKey = id;
        return item;
    }

    public ExecutableItem GetCurrentItem()
    {
        ExecutableItem executableItem;
        if (_currentKey == null)
        {
            executableItem = AddItem(_defaultKey);
        }
        else
        {
            _ = _subItems.TryGetValue(_currentKey, out executableItem);
        }
        return executableItem;
    }
}
