using UnityEngine.UI;

public interface IClickable

{
    int MyCount
    {
        get;
    }

    Image MyIcon
    {
        get;
        set;
    }
    Text MyStackText
    {
        get;
    }
}
