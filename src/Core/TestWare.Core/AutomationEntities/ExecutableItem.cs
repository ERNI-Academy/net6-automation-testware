namespace TestWare.Core.AutomationEntities;

public class ExecutableItem
{
    private readonly string _id;
    private const string _defaultKey = "default";
    private string _currentKey;
    private readonly Dictionary<string, ExecutableItem> _subItems;
    public string Id { get { return _id; } }

    public ExecutableItem(string id)
    {
        _id = id;
        _subItems = new();
    }

    public void StartExecution()
    {
        // Do nothing, not applicable.
    }

    public void StopExecution()
    {
        // Do nothing, not applicable.
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
