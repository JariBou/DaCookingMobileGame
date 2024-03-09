namespace _project.Scripts.Core
{
    public enum IngredientFamily
    {
        Meat, Vegetable, Seafood
    }

    public enum CookingMethod
    {
        Method1, Method2, Method3, 
        Null = 63
    }

    public enum PhaseCode
    {
        Phase1 = 0,
        Phase2 = 1,
        Phase3 = 2,
        Phase4 = 3,
    }
    
    public enum BossState
    {
        Calm = 0,
        Neutral = 1,
        Angry = 2,
    }
}