using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting2;
using UnityEditor;
using UnityEngine;

namespace Unity.VisualScripting2
{
    [InitializeOnLoad]
    public class VSHacker : AssetPostprocessor
    {
        static VSHacker()
        {
            VSIProvider.Hack();
        }

    }
}
