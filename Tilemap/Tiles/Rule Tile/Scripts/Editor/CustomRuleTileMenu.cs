﻿namespace UnityEditor
{
    static class CustomRuleTileMenu
    {
        [MenuItem("Assets/Create/Custom Rule Tile Script", false, 89)]
        static void CreateCustomRuleTile()
        {
            ProjectWindowUtil.CreateAssetWithContent("Assets/Tilemap/Tiles/Rule Tile/ScriptTemplates/NewCustomRuleTile.cs.txt", "NewCustomRuleTile.cs");
        }
    }
}
