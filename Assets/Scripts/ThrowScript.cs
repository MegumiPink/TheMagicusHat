using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    private Camera cam;

    public GameObject objectToThrow;

    public float throwForce;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            Click();
        }
    }

    void Click()
    {
        Vector3 point = new Vector3();
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        //using the bullet script, after th
        GameObject newObj = Instantiate(objectToThrow, transform.position, Quaternion.identity);
        newObj.GetComponent<Rigidbody2D>().linearVelocity = (point - transform.position).normalized * throwForce;

    }
}
