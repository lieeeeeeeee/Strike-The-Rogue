using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Wall : MonoBehaviour {
    private enum sideType { top, bot, left, right };
    [SerializeField] private sideType side;
    private float thicknessRatio = 0.01f;
    private Transform parentTransform;
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;

    void Reset() {
    }
    void OnValidate() {
        adjustTransform();
    }
    void Start() {
        adjustTransform();
    }

    private void adjustTransform() {
        Vector3 parentSize = transform.parent.GetComponent<RectTransform>().rect.size;
        float thichness = ((parentSize.x < parentSize.y) ? parentSize.x : parentSize.y) * thicknessRatio;


        switch (side) {
            case sideType.top:
                transform.localPosition = new Vector3(0, parentSize.y / 2 - thichness / 2, 0);
                transform.localScale = new Vector3(parentSize.x, thichness, 1);
                break;
            case sideType.bot:
                transform.localPosition = new Vector3(0, -parentSize.y / 2 + thichness / 2, 0);
                transform.localScale = new Vector3(parentSize.x, thichness, 1);
                break;
            case sideType.right:
                transform.localPosition = new Vector3(parentSize.x / 2 - thichness / 2, 0, 0);
                transform.localScale = new Vector3(thichness, parentSize.y, 1);
                break;
            case sideType.left:
                transform.localPosition = new Vector3(-parentSize.x / 2 + thichness / 2, 0, 0);
                transform.localScale = new Vector3(thichness, parentSize.y, 1);
                break;
        }
    }
}
