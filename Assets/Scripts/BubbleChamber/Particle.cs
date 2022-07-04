using UnityEngine;
public class Particle {
    public Charge Charge { get; set; }
    public int Mass { get; }
    public Vector2Int PreviousLocation { get; set; }
    public Vector2Int Location { get; set; }
    public Vector3 Velocity { get; set; }
    public Color Color { get; }
    public float LifeTime { get; set; }
    public float TimeToLive { get; }

    public Particle(Charge charge, Vector2Int location, Vector3 velocity, float timeToLive)
    {
        Mass = charge.Count;

        if (Mass == 0)
        {
            Debug.LogError("Creating particle with mass 0");
        }

        Charge = charge;
        Color = charge.TotalCharge == 0 ? Color.black : Color.white;
        LifeTime = 0;
        PreviousLocation = location;
        Location = location;
        Velocity = velocity;
        TimeToLive = timeToLive;
    }
}