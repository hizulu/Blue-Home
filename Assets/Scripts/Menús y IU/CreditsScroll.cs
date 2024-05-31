using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para manejar el componente Image
using TMPro;

public class CreditsScrollTMP : MonoBehaviour
{
    [Header("Configuración de desplazamiento")]
    [SerializeField] public float scrollSpeed = 20f;
    [SerializeField] public Image logoImage;
    [SerializeField] public float logoDisplayTime = 5f; //tiempo del logo en pantalla
    [SerializeField] public float fadeDuration = 4f;
    [SerializeField] public float logoMaxScale = 5f; //tamaño adicional del logo

    //variables privadas para el desplazamiento
    private RectTransform rectTransform;
    private float elapsedTime;
    private bool startFading;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        elapsedTime = 0f;
        startFading = false;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        //si ha pasado el tiempo de visualización del logo, se desplaza
        if (elapsedTime >= logoDisplayTime)
        {
            startFading = true;
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }

        //si ha terminado el tiempo de visualización del logo, se desvanece
        if (startFading && elapsedTime <= logoDisplayTime + fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0.1f, (elapsedTime - logoDisplayTime) / fadeDuration);
            logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, alpha);
        }

        // Aumenta el tamaño del logo mientras está en pantalla
        if (elapsedTime <= logoDisplayTime)
        {
            float scale = Mathf.Lerp(logoMaxScale-0.5f, logoMaxScale, elapsedTime / logoDisplayTime);
            logoImage.transform.localScale = new Vector3(scale, scale, scale);
        }

        // El texto fuera de pantalla
        if (rectTransform.anchoredPosition.y > 4800) // Hardcodeado porque no con el sizeDelta no funciona bien
        {
            Invoke("ChangeScene", 5f); 
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}