#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileReplacer : EditorWindow
{
    private Tilemap targetTilemap;
    private TileBase oldTile;
    private TileBase newTile;
    private int replacedCount = 0;

    [MenuItem("Tools/Tile Replacer")]
    public static void ShowWindow()
    {
        GetWindow<TileReplacer>("Tile Replacer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tile Replacer", EditorStyles.boldLabel);
        GUILayout.Space(10);

        targetTilemap = (Tilemap)EditorGUILayout.ObjectField("Tilemap", targetTilemap, typeof(Tilemap), true);
        oldTile = (TileBase)EditorGUILayout.ObjectField("Replace This Tile", oldTile, typeof(TileBase), false);
        newTile = (TileBase)EditorGUILayout.ObjectField("With This Tile", newTile, typeof(TileBase), false);

        GUILayout.Space(10);

        if (GUILayout.Button("Replace Tiles"))
        {
            if (targetTilemap == null) { Debug.LogError("Assign a Tilemap!"); return; }
            if (oldTile == null)       { Debug.LogError("Assign the tile to replace!"); return; }

            ReplaceTiles();
        }

        if (replacedCount > 0)
        {
            GUILayout.Space(5);
            EditorGUILayout.HelpBox($"Replaced {replacedCount} tile(s)!", MessageType.Info);
        }
    }

    private void ReplaceTiles()
    {
        replacedCount = 0;
        BoundsInt bounds = targetTilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (targetTilemap.GetTile(pos) == oldTile)
            {
                targetTilemap.SetTile(pos, newTile);
                replacedCount++;
            }
        }

        Debug.Log($"Tile Replacer: replaced {replacedCount} tile(s).");
    }
}
#endif