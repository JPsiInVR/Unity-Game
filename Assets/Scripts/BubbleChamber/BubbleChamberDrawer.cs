using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BubbleChamberDrawer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Vector3 magneticField; 
    [SerializeField]
    private float decayRate;
    [SerializeField] 
    private Color fadeColor;
    private HashSet<int> _pixelsSet;
    private Texture2D _texture;
    private Color32[] _pixels;
    private readonly Vector2Int _imageDimensions = new Vector2Int{ x = 2880, y = 1800 };
    private BubbleChamber _bubbleChamber;
    private void Start()
    {
        _bubbleChamber = new BubbleChamber(_imageDimensions, magneticField , decayRate);
        
        var preallocationListForHashset = Enumerable.Range(0, _imageDimensions.x * _imageDimensions.y);
        _pixelsSet = new HashSet<int>(preallocationListForHashset);
        _pixelsSet.Clear();
        
        _texture = new Texture2D(_imageDimensions.x, _imageDimensions.y, TextureFormat.ARGB32, true);

        for (int i = 0; i < _imageDimensions.x; i++)
        {
            for (int j = 0; j < _imageDimensions.y; j++)
            {
                _texture.SetPixel(i, j, Color.black);
            }
        }
        
        _pixels = _texture.GetPixels32();

        StartCoroutine(_bubbleChamber.SpawnParticles());
    }

    private void Update()
    {
        FadeOut();
        _bubbleChamber.UpdateParticles();
        Draw();
        _bubbleChamber.Split();
    }
    
    private void FadeOut()
    {
        foreach (int pixelPosition in _pixelsSet)
        {
            _pixels[pixelPosition] -= fadeColor;
        }
        _pixelsSet.RemoveWhere(i => _pixels[i].r <= 0);
    }

    private void DrawLine(Vector2Int startPoint, Vector2Int endPoint, Color color, int thickness)
    {
        
        //When both particles are off texture don't even bother to draw
        if (!IsPointOnTexture(startPoint, 0) && !IsPointOnTexture(endPoint, 0))
        {
            return;
        }

        Vector2Int currentPoint = startPoint;
        float progressFraction = 1 / (startPoint - endPoint).magnitude;
        float progress = 0;

        while (currentPoint.x != endPoint.x || currentPoint.y != endPoint.y)
        {
            currentPoint = Vector2Int.RoundToInt(Vector2.Lerp(startPoint, endPoint, progress));
            progress += progressFraction;

            //Draw only when point is on the texture
            if (IsPointOnTexture(currentPoint, thickness))
            {
                DrawPoint(currentPoint, color, 2);
            }
        }
    }
    
    private void DrawPoint(Vector2Int point, Color color, int thickness)
    {
        for (int x = -thickness; x < thickness; x++)
        {
            for (int y = -thickness; y < thickness; y++)
            { 
                _pixels[point.x + x + _imageDimensions.x * (y + point.y)] = color;
                _pixelsSet.Add(point.x + x + _imageDimensions.x * (y + point.y));
            }
        }
    }

    private bool IsPointOnTexture(Vector2Int point, int thickness)
    {
        return point.x + thickness < _imageDimensions.x && point.x - thickness > 0 && point.y + thickness < _imageDimensions.y && point.y - thickness > 0;
    }

    private void Draw()
    {
        foreach (var particle in _bubbleChamber.Particles)
        {
            DrawLine(particle.PreviousLocation, particle.Location, particle.Color, 2);
        }
        
        _texture.SetPixels32(_pixels);
        _texture.Apply(true);
        image.sprite = Sprite.Create(_texture, new Rect(0,0, _imageDimensions.x, _imageDimensions.y), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect);
    }
}
