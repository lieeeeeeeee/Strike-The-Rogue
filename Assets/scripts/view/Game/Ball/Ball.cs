using UnityEngine;

public class Ball : MonoBehaviour {
    private Vector2 position { 
        get { return Camera.main.WorldToScreenPoint(transform.position); }
    }
    private Vector2 size { 
        get { return transform.localScale; } 
        set { transform.localScale = value; } 
    }
    private bool isDragged = false;
    private new Rigidbody2D rigidbody2D;
    private DragIndicator dragIndicator;
    private int dragDistanceLimit = 4;
    private float dragDistance = 0;
    private float speedRatio = 1.0f;
    private float sizeRatio = 0.05f;
    private float floorFriction = 0.99f;

    private const int fps = 30;
    private float time = 0;

    float test = 0;

    private void Start() {
        Vector2 parentSize = transform.parent.GetComponent<RectTransform>().rect.size;
        dragIndicator = new DragIndicator(gameObject);
        rigidbody2D = GetComponent<Rigidbody2D>();

        size = new Vector2(parentSize.x * sizeRatio, parentSize.x * sizeRatio);
    }
    private void Update() {
        // test += Time.deltaTime;
        // if (test > 1) {
        //     Debug.Log(  
        //         "localPos: " + transform.localPosition + ", C(localPos): " + position + 
        //         "\npos" + transform.position + ", C(pos): " + Camera.main.WorldToScreenPoint(transform.position) +
        //         "\nmousePos: " + Input.mousePosition
        //     );
        //     test = 0;
        //     return;
        // }
        if (isDragged) {
            if (isInsideCircle()) return;
            Vector2 mousePosition = Input.mousePosition;

            dragDistance = Vector2.Distance(position, mousePosition);

            if (dragDistance >= size.x * dragDistanceLimit) {
                dragDistance = size.x * dragDistanceLimit;
            }

            dragIndicator.Display(position, size, mousePosition, dragDistance);
        }
        // ボールが動いているとき
        // if (rigidbody2D.velocity.magnitude > 0.1f) {
        //     // 一定時間おきに速度を減衰させる
        //     time += Time.deltaTime;
        //     if (time < 1.0f / fps) return;
        //     time = 0;
        //     rigidbody2D.velocity *= floorFriction;

        //     // 速度が一定以下になったら停止
        //     if (rigidbody2D.velocity.magnitude > 0.5f) return;
        //     rigidbody2D.velocity = Vector2.zero;
            
        // }
    }
    private void OnMouseDown() {
        if (!isInsideCircle()) return; 
        if (rigidbody2D.velocity.magnitude > 0.1f) {
            Debug.Log("Ball is moving");
            return;
        }
        isDragged = true;
    }

    private void OnMouseUp() {
        if (!isDragged) return;
        if (!isInsideCircle()) {
            Vector2 mouseUpPosition = Input.mousePosition;
            Vector2 forceDirection = (position - mouseUpPosition).normalized;
            // 距離に比例して力を加える
            float forceMagnitude = dragDistance * speedRatio * 0.05f;

            GetComponent<Rigidbody2D>().AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        }
        dragIndicator.Hide();
        isDragged = false;
    }

    private bool isInsideCircle() {
        Vector2 mousePosition = Input.mousePosition;
        float radius = size.x / 2;

        // Debug.Log("position: " + position + ", mousePosition: " + mousePosition);
        return Vector2.Distance(position, mousePosition) <= radius;
    }
}
