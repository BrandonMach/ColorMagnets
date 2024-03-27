using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerColorChangeControlsArcade")]
public class ArcadeControlsSO : ScriptableObject
{
    [SerializeField] public KeyCode ChangeRed;
    [SerializeField] public KeyCode ChangeBlue;
    [SerializeField] public KeyCode ChangeYellow;
    [SerializeField] public KeyCode ChangeGreen;
    
}
