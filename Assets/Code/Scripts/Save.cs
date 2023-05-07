using System;

[Serializable]
public class Save
{
    public int Coins;
    public int BowLevel;
    public int SpearLevel;

    public Save() 
    {
        Coins = 0;
        BowLevel = 1;
        SpearLevel = 1;
    }
}
