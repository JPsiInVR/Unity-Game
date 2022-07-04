using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleChamber
{
    public List<Particle> Particles { get; }
    private readonly Vector3 _magneticField;
    private readonly float _decayRate;
    private Vector2Int _chamberSize;

    private Vector2Int _positiveChargesRange;
    private Vector2Int _neutralChargesRange;
    private Vector2Int _negativeChargesRange;
    private Vector2Int _velocityRange;
    
    public BubbleChamber(Vector2Int chamberSize, Vector3 magneticField, float decayRate)
    {
        Particles = new List<Particle>(1000);
        _magneticField = magneticField;
        _chamberSize = chamberSize;
        _decayRate = decayRate;

        _positiveChargesRange = new Vector2Int(2, 5);
        _neutralChargesRange = new Vector2Int(2, 5);
        _negativeChargesRange = new Vector2Int(2, 5);

        _velocityRange = new Vector2Int(300, 1000);
    }
    
    public IEnumerator SpawnParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            Particles.Add(new Particle(
                new Charge(Random.Range(_positiveChargesRange.x, _positiveChargesRange.y),
                    Random.Range(_neutralChargesRange.x, _neutralChargesRange.y),
                    Random.Range(_negativeChargesRange.x, _negativeChargesRange.y)),
                new Vector2Int(Random.Range(0, _chamberSize.y), Random.Range(0, _chamberSize.y)),
                new Vector3(Random.Range(_velocityRange.x, _velocityRange.y), 0), DecayDistribution(_decayRate)));
        }
    }

    
    public void UpdateParticles()
    {
        for (int i = Particles.Count - 1; i >= 0; i--)
        {
            Particles[i].LifeTime += Time.deltaTime;
            Vector3 force = Particles[i].Charge.TotalCharge * Vector3.Cross(Particles[i].Velocity, _magneticField);
            Vector3 acceleration = force / Particles[i].Mass;

            Particles[i].Velocity += Vector3Int.RoundToInt(acceleration * Time.deltaTime);
            Particles[i].Velocity *= 1.0f - 0.3f * Time.deltaTime;
            
            Particles[i].PreviousLocation = Particles[i].Location;
            Particles[i].Location += Vector2Int.RoundToInt(Particles[i].Velocity * Time.deltaTime);

            if (Particles[i].Mass > 1)
            {
                continue;
            }

            if (Particles[i].Charge.TotalCharge == 0 || Particles[i].LifeTime > Particles[i].TimeToLive)
            {
                Particles.Remove(Particles[i]);
            }
        }
    }
    
    public void Split()
    {
        for (int i = Particles.Count - 1; i >= 0; i--)
        {
            if (Particles[i].LifeTime < Particles[i].TimeToLive || Particles[i].Mass == 1)
            {
                continue;
            }
            
            SplitParticle(Particles[i]);
            Particles.RemoveAt(i);
        }
    }
    
    private void SplitParticle(Particle particle)
    {
        while (particle.Charge.Count > 0)
        {
            Charge newParticleCharge = new Charge(
                Random.Range(0, particle.Charge.Positive + 1),
                Random.Range(0, particle.Charge.Neutral + 1),
                Random.Range(0, particle.Charge.Negative + 1));
            
            if (newParticleCharge.Count == 0)
            {
                continue;
            }

            particle.Charge -= newParticleCharge;
           
            Particle newParticle = new Particle(newParticleCharge, particle.Location, particle.Velocity, DecayDistribution(_decayRate));
            Particles.Add(newParticle);
        }
    }
    
    private float DecayDistribution (float lambda) => (float) (lambda * Math.Exp(-lambda * Random.Range(0.0f, 1.0f)));
}
