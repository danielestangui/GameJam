using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float cameraDistance = -10f;

    public float _followSpeed;

    [Header("Paralax")]
    public Transform[] _background;

    private Camera cam;
    private Transform target;
    private float offset;
    private float stageSize;



    public static CameraController singleton;

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag(Tag.Player).transform;

        SetStage();
    }

    // Tiene que tener el mismo delta que el player, sino se ve lageado, en un futuro cambiar por un update global
    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        MoveCamera(delta);
        Parallax();
    }

    /// <summary>
    /// Prepara el stage.
    /// </summary>
    private void SetStage() {

        foreach (Transform t in _background) {
            t.position = new Vector3(0,t.position.y,t.position.z); ;
        }

        if (_background.Length < 0)
        {
            Debug.LogError("Error: La cámara no tiene asignado ningún background.");
            return;
        }

        stageSize = GetBackgroundSize(_background[_background.Length - 1]);
        offset = GetOffset(stageSize);
        transform.position = new Vector3(transform.position.x, cam.orthographicSize, cameraDistance);
    }

    private void MoveCamera(float delta) {
        float speed = delta * _followSpeed;
        float limitedPosition = Mathf.Clamp(target.transform.position.x, -offset, offset);
        Vector3 targetPosition = new Vector3(limitedPosition, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }

    private void MoveBackground(Transform backgroundTransform) {
        float bgOffsett = GetOffset(GetBackgroundSize(backgroundTransform));
        float limitedPosition = Mathf.Clamp(target.transform.position.x, -bgOffsett, bgOffsett);
        backgroundTransform.position = new Vector3(limitedPosition, backgroundTransform.position.y, backgroundTransform.position.z);
    }

    /// <summary>
    /// GetOffset necesita que primero se ejecute la función GetStageSize.
    /// </summary>
    /// <returns></returns>
    private float GetOffset(float width) {
        return (width/2) - (cam.aspect * cam.orthographicSize);
    }

    private void Parallax() {

        Transform skyTransform = _background[0];
        skyTransform.position = new Vector3(transform.position.x, skyTransform.position.y, skyTransform.position.z);

        if (_background.Length > 2) {

            for (int i = 1; i < (_background.Length -1); i++)
            {
                MoveBackground(_background[i]);
            }
        }
    }

    /// <summary>
    /// Calcula el ancho total en unidades de Unity que tiene la capa más cerana a la cámara del Background.
    /// </summary>
    /// <returns></returns>
    private float GetBackgroundSize(Transform background)
    {
        Transform nearestBackground = _background[_background.Length - 1];
        SpriteRenderer backgroundSprite = background.GetComponent<SpriteRenderer>();
        float pixelWidth = backgroundSprite.sprite.rect.width;
        float pixelPerUnit = backgroundSprite.sprite.pixelsPerUnit;

        return (pixelWidth / pixelPerUnit) * background.localScale.x;
    }
}
