using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class SceneIAM : InputActionManager
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
