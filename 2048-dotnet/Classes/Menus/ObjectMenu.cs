using Game2048.Interfaces;

namespace Game2048.Classes;

class ObjectMenu : IMenu
{
    string displayName;
    public string DisplayName
    {
        get
        {
            return displayName;
        }
    }

    Action<object[]> action;

    object[] args;

    public ObjectMenu(string displayName, Action<object[]> action, params object[] args)
    {
        this.displayName = displayName;
        this.action = action;
        this.args = args;
    }

    public MenuResult MenuAction()
    {
        action?.Invoke(args);
        return MenuResult.Obj;
    }
}
