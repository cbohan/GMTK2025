public static class InterSceneData
{
    public static CatData Honey = new CatData()
    {
        Name = "Honey Loops",
        Description = "Shakey hands? Try using bigger lines!",
        Image = CatImage.Honey,
        Level = 1,
        
        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,

        Ult = UltType.Sticky_Honey,
        Ability = AbilityType.BiggerLines,
    };
    
    public static CatData Oreo = new CatData()
    {
        Name = "Oreos?",
        Description = "Smaller lines but smaller boosts!",
        Image = CatImage.Oreo,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,

        Ult = UltType.Sugar_Rush,
        Ability = AbilityType.ShorterLines,
    };

    public static CatData Apple = new CatData()
    {
        Name = "Jacked Cat",
        Description = "Big lines mean bigger boosts!",
        Image = CatImage.Apple,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,

        Ult = UltType.Apple_Jacked,
        Ability = AbilityType.LongerLines,
    };

    public static CatData Loop = new CatData()
    {
        Name = "Brother Loops",
        Description = "Crush your opponents with the CHONK!",
        Image = CatImage.Loop,
        Level = 0,

        Speed = StatValue.Medium,
        Stamina = StatValue.Medium,
        Acceleration = StatValue.Medium,
        
        Ult = UltType.Chungus_Mode,
        Ability = AbilityType.SizeAndSpeedBoost,
    };

    public static int PlayerRacePlacement;
    
    public static bool GameStarted = false;

    public static CatData PlayerCat = Loop;
    public static CatData[] AiCats = new CatData[]
    {
        Oreo,
        Apple,
        Loop,
    };    
}
