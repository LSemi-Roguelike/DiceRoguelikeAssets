
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeItem))]
public class ItemEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath,
        UnityEngine.Object[] subAssets, int width, int height)
    {
        var obj = target as UpgradeItem;
        var icon = obj.GetSprite();
        var preview = AssetPreview.GetAssetPreview(icon);

        if (preview == null)
        {
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }

        var final = new Texture2D(width, height);

        EditorUtility.CopySerialized(preview, final);

        return final;
    }
}

[CustomEditor(typeof(Parts))]
public class PartsEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath,
        UnityEngine.Object[] subAssets, int width, int height)
    {
        var obj = target as Parts;
        var icon = obj.GetSprite();
        var preview = AssetPreview.GetAssetPreview(icon);

        if (preview == null)
        {
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }

        var final = new Texture2D(width, height);

        EditorUtility.CopySerialized(preview, final);

        return final;
    }
}

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath,
        UnityEngine.Object[] subAssets, int width, int height)
    {
        var obj = target as Weapon;
        var icon = obj.GetSprite();
        var preview = AssetPreview.GetAssetPreview(icon);

        if (preview == null)
        {
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }

        var final = new Texture2D(width, height);

        EditorUtility.CopySerialized(preview, final);

        return final;
    }
}

[CustomEditor(typeof(DiceParts))]
public class DicePartsEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath,
        UnityEngine.Object[] subAssets, int width, int height)
    {
        var obj = target as DiceParts;
        var icon = obj.GetSprite();
        var preview = AssetPreview.GetAssetPreview(icon);

        if (preview == null)
        {
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }

        var final = new Texture2D(width, height);
        Debug.Log(final + "," + preview);
        EditorUtility.CopySerialized(preview, final);

        return final;
    }
}

#endif