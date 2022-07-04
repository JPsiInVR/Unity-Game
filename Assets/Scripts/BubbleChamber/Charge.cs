public readonly struct Charge
{
    public int Positive { get; }
    public int Neutral { get; }
    public int Negative { get; }
    
    public int TotalCharge => Positive - Negative;
    public int Count => Positive + Neutral + Negative;
    
    public Charge(int positive, int neutral, int negative)
    {
        Positive = positive;
        Neutral = neutral;
        Negative = negative;
    }
    
    public static Charge operator -(Charge a, Charge b) 
        => new Charge(a.Positive - b.Positive,
                      a.Neutral - b.Neutral,
                      a.Negative - b.Negative);
}