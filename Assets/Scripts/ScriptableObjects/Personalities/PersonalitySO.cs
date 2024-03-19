using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Personality", menuName = "CharacterExpressions/Personality", order = 4)]
public class PersonalitySO : ScriptableObject
{
    public string personalityName;
    public string PersonalityPrompt;
}
