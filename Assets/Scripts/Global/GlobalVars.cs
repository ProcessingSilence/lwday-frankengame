using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    public static class vars
    {
        public static float deathPos;
        public static string[] importantTags;
    }

    public float deathPosition;
    public string[] importantTags;
    private void Awake()
    {
        vars.deathPos = deathPosition;
        if (importantTags.Length != 0)
        {
            vars.importantTags = new string[importantTags.Length];
            for (int i = 0; i < importantTags.Length - 1; i++)
            {
                vars.importantTags[i] = importantTags[i];
            }
        }
    }
}
