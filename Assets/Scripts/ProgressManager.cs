using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    private Transform _chaptersContainer;

    [SerializeField]
    private Material _enabledMaterial;

    [SerializeField]
    private Material _disabledMaterial;

    private List<List<CardboardInteractable>> _parts = new List<List<CardboardInteractable>>();
    
    private void Start()
    {
        //Chapters
        for (int i = 0; i < _chaptersContainer.childCount; i++)
        {
            Transform chapterTransform = _chaptersContainer.GetChild(i);
            //Parts
            for (int j = 0; j < chapterTransform.childCount; j++)
            {
                Transform partTransform = chapterTransform.GetChild(j);
                
                var currentPart = new List<CardboardInteractable>();
                _parts.Add(currentPart);
                
                //Sections
                for (int k = 0; k < partTransform.childCount; k++)
                {
                    CardboardInteractable sectionButton = partTransform.GetChild(k).GetComponent<CardboardInteractable>();
                    sectionButton.Interactable = false;
                    currentPart.Add(sectionButton);
                } 
            }
        }

        int progress = GameData.Instance.Progress;

        foreach (var part in _parts)
        {
            if (progress > 0)
            {
                part[0].Interactable = true;
                part[0].GetComponent<Image>().material = _enabledMaterial;
            }

            for (int i = 1; i < part.Count && progress > 0; i++)
            {
                part[i].GetComponent<Image>().material = _enabledMaterial;
                part[i].Interactable = true;
                progress--;
            }
        }
    }
}
