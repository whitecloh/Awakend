
using System.Collections.Generic;

public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        { Quality.Common, "#d6d6d6" },
        { Quality.Uncommon, "#00ff00ff" },
        { Quality.Rare, "#0000ffff" },
        { Quality.Epic, "#800080ff" }

    };

    public static Dictionary<Quality, string> Colors => colors;
}
public enum Quality
{
    Common,
    Uncommon,
    Rare,
    Epic
}