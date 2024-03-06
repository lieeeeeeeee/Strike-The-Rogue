using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DragIndicator {

    private const string imagePath = "Assets/resource/images/";
    private const string imageName = "dragIndicator";
    private const string imageType = ".png";
    private GameObject gameObject;
    private RectTransform rectTransform;

    // 座標(0, 0), サイズ0x0のオブジェクトを生成
    public DragIndicator(GameObject parent) {
        // 画像の読み込み
        byte[] imageData = File.ReadAllBytes(Path.Combine(imagePath, imageName + imageType));
        Texture2D texture = new Texture2D(2, 2);
        // SpriteRendererの作成
        SpriteRenderer spriteRenderer;
        // Spriteの作成
        Sprite sprite;

        // ゲームオブジェクトを作成
        gameObject = new GameObject("DragIndicator");
        // RectTransformの追加
        rectTransform = gameObject.AddComponent<RectTransform>();
        // SpriteRendererの追加
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        // 親オブジェクトを設定
        gameObject.transform.SetParent(parent.transform);

        // 画像をテクスチャに変換
        texture.LoadImage(imageData);

        // 画像を貼り付ける
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0.5f), 128.0f);
        spriteRenderer.sprite = sprite;

        // サイズを設定
        rectTransform.sizeDelta = sprite.bounds.size;

        // アンカーを設定
        rectTransform.anchorMin = new Vector2(0, 0.5f);
        rectTransform.anchorMax = new Vector2(0, 0.5f);

        // ピボットを設定
        rectTransform.pivot = new Vector2(0, 0.5f);

        // 非アクティブ化
        gameObject.SetActive(false);
    }

    public void Display(Vector2 ballPosition, Vector2 ballSize, Vector2 mousePosition, float distance) {
        float radius = ballSize.x * 0.5f;
        float angle = Mathf.Atan2(ballPosition.y - mousePosition.y, ballPosition.x - mousePosition.x);
        float x = Mathf.Cos(angle) * 0.5f;
        float y = Mathf.Sin(angle) * 0.5f;
        float width = distance / ballSize.x - 0.5f;

        rectTransform.localPosition = new Vector3(x, y, -1);
        rectTransform.localScale = new Vector3(width, 1, 1);
        rectTransform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
