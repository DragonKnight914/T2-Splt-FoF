using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObjects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    [Header("CameraStats")]
    [SerializeField] private float flipYRotationTime = 0.5f;

    private Coroutine turnCoroutine;
    private Player P;
    private bool isFacingRight;

    // Start is called before the first frame update
    void Awake()
    {
        P = playerTransform.gameObject.GetComponent<Player>();

        isFacingRight = P.faceRight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        yield return new  WaitForSeconds(0.1f);

        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        
        while(elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            //lerp the y rotation
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
        

    }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;

        if (!isFacingRight)
            return 180f;
        else
            return 0f;
    }
}
