using Game2048.Interfaces;

namespace Game2048.Classes.Menus;

class ActionMenu : IMenu
{
    string displayName;
    public string DisplayName
    {
        get
        {
            return displayName;
        }
    }

    Action action;

    public ActionMenu(string displayName)
    {
        this.displayName = displayName;
    }

    public MenuResult MenuAction()
    {
        action?.Invoke();
        return MenuResult.OK;
    }
}
