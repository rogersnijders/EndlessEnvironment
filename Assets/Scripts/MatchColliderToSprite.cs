using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MatchColliderToSprite : MonoBehaviour
{
    private void Start()
    {
        MatchCollider();
    }

    private void MatchCollider()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        Color32[] pixels = texture.GetPixels32();
        int width = texture.width;
        int height = texture.height;
        int startY = 0;
        int endY = height - 1;
        //int endY = height;

        // Find the first and last non-transparent row of pixels
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[y * width + x];
                if (pixel.a != 0)
                {
                    startY = y;
                    break;
                }
            }
            if (startY != 0)
                break;
        }

        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[y * width + x];
                if (pixel.a != 0)
                {
                    endY = y;
                    break;
                }
            }
            if (endY != height - 1)
                break;
        }

        // Set the height of the capsule collider
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        float colliderHeight = ((endY - startY) / (float)height) * sprite.bounds.size.y;
        Vector2 colliderSize = new Vector2(capsuleCollider.size.x, colliderHeight);
        capsuleCollider.size = colliderSize;

        //// Set the offset of the capsule collider
        //float offsetY = sprite.bounds.extents.y - colliderHeight / 2f;
        ////Vector2 colliderOffset = new(0f, offsetY);
        //Vector2 colliderOffset = new(0f, 0f);
        //capsuleCollider.offset = colliderOffset;
    }
}
