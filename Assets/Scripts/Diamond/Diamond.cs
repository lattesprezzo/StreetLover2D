using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Diamond : MonoBehaviour
{
    [Tooltip("Information about the diamond")]
    public string info;
 [Space(10)]
    [Header("Diamond Properties")]
    [TextArea(3, 10)]
    public string description;

}
