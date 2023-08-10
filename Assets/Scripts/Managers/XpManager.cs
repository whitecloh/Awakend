
static class XpManager
{
    public static int CalculateExp(Enemy enemy)
    {
        int baseXp = (Player.MyInstance.MyLevel *5)+45;
        int grayLvl = CalculateGrayLevel();
        int totalXp = 0;

        if(enemy.MyLevel >= Player.MyInstance.MyLevel)
        {
            totalXp = (int)(baseXp * (1 + 0.05f * (enemy.MyLevel - Player.MyInstance.MyLevel)));
        }
        else if(enemy.MyLevel >grayLvl)
        {
            totalXp = (baseXp) * (1 - (Player.MyInstance.MyLevel - enemy.MyLevel) / (ZeroDifference())); 
        }

        return totalXp;
    }

    public static int CalculcateExp(Quest quest)
    {
        if(Player.MyInstance.MyLevel<=quest.Level+5)
        {
            return quest.Xp;
        }
        if (Player.MyInstance.MyLevel <= quest.Level + 6)
        {
            return (int)(quest.Xp*0.8/5)*5;
        }
        if (Player.MyInstance.MyLevel <= quest.Level + 7)
        {
            return (int)(quest.Xp * 0.6 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel <= quest.Level + 8)
        {
            return (int)(quest.Xp * 0.4 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel <= quest.Level + 9)
        {
            return (int)(quest.Xp * 0.2 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel <= quest.Level + 10)
        {
            return (int)(quest.Xp * 0.1 / 5) * 5;
        }

        return 0;
    }

    private static int ZeroDifference()
    {
        if(Player.MyInstance.MyLevel<=7)
        {
            return 5;
        }
        if(Player.MyInstance.MyLevel >= 8 && Player.MyInstance.MyLevel <= 9)
        {
            return 6;
        }
        if (Player.MyInstance.MyLevel >= 10 && Player.MyInstance.MyLevel <= 11)
        {
            return 7;
        }
        if (Player.MyInstance.MyLevel >= 12 && Player.MyInstance.MyLevel <= 15)
        {
            return 8;
        }
        if (Player.MyInstance.MyLevel >= 16 && Player.MyInstance.MyLevel <= 19)
        {
            return 9;
        }
        if (Player.MyInstance.MyLevel >= 20 && Player.MyInstance.MyLevel <= 29)
        {
            return 11;
        }
        if (Player.MyInstance.MyLevel >= 30 && Player.MyInstance.MyLevel <= 39)
        {
            return 12;
        }
        if (Player.MyInstance.MyLevel >= 40 && Player.MyInstance.MyLevel <= 44)
        {
            return 13;
        }
        if (Player.MyInstance.MyLevel >= 45 && Player.MyInstance.MyLevel <= 49)
        {
            return 14;
        }
        if (Player.MyInstance.MyLevel >= 50 && Player.MyInstance.MyLevel <= 54)
        {
            return 15;
        }
        if (Player.MyInstance.MyLevel >= 55 && Player.MyInstance.MyLevel <= 59)
        {
            return 16;
        }
        return 17;
    }

    public static int CalculateGrayLevel()
    {
        if(Player.MyInstance.MyLevel<=5)
        {
            return 0;
        }
        else if(Player.MyInstance.MyLevel>=6 && Player.MyInstance.MyLevel<=49)
        {
            return Player.MyInstance.MyLevel - (Player.MyInstance.MyLevel / 10) - 5;
        }
        else if (Player.MyInstance.MyLevel == 50)
        {
            return Player.MyInstance.MyLevel - 10;    
        }
        else if (Player.MyInstance.MyLevel >= 51 && Player.MyInstance.MyLevel <= 60)
        {
            return Player.MyInstance.MyLevel - (Player.MyInstance.MyLevel / 5) - 1;
        }
        return Player.MyInstance.MyLevel - 9;
    }
}
