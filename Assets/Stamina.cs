using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float barValue;

    private static Stamina instance;

    private RectTransform barBackgroundTrs;
    private RectTransform barValueTrs;

    private float maxWidth;
    private float maxBkgWidth;

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            barBackgroundTrs = transform.GetChild(0).GetComponent<RectTransform>();
            barValueTrs = transform.GetChild(1).GetComponent<RectTransform>();

            maxWidth = barValueTrs.sizeDelta.x;
            maxBkgWidth = barBackgroundTrs.sizeDelta.x;
            Debug.Log(maxWidth);

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError($"Múltiplas instâncias de Stamina encontrados deletando {gameObject.name}");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        barValue = Mathf.Clamp(barValue, 0f, 100f);
        value = Mathf.Clamp(value, 0f, barValue);

        barValueTrs.sizeDelta = new Vector2(value / 100f * maxWidth, barValueTrs.sizeDelta.y);
		barBackgroundTrs.sizeDelta = new Vector2(barValue / 100f * maxBkgWidth, barBackgroundTrs.sizeDelta.y);
	}
}
