using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float movementSpeed = 25f;
    public float rotationSpeed = 35f;
    private GameObject weapon;
    private RaycastHit hit;
    private LineRenderer lineRenderer;

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        lineRenderer = weapon.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(
            new Vector3(0, 0, vertical) * movementSpeed * Time.deltaTime,
            Space.Self);

        transform.Rotate(new Vector3(0, horizontal, 0) * rotationSpeed * Time.deltaTime, Space.Self);

        Physics.Raycast(weapon.transform.position, weapon.transform.forward, out hit, Mathf.Infinity);

        try
        {
            if (hit.collider.gameObject.tag.Equals("Enemy"))
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, weapon.transform.position);
                lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    lineRenderer.enabled = false;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        catch (System.Exception)
        {
            lineRenderer.enabled = false;
        }
    }
}
