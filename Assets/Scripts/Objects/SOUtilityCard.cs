using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityCard", menuName = "Cards/NewUtilityCard", order = 2)]
public class SOUtilityCard : SOCardBase
{
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    private Sprite cardImage;
    private Sprite cardBackground;
}
