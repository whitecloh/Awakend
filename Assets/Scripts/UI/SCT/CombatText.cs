using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private float _lifeTime;

    private void Awake()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y - transform.eulerAngles.y-180, 0);
        StartCoroutine(FadeOut());
    }
    private void Update()
    {
        Move();   
    }
    private void Move()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = _text.color.a;
        float rate = 1.0f/_lifeTime;

        float progress = 0f;
        while(progress<1.0)
        {
            Color tmp = _text.color;
            tmp.a = Mathf.Lerp(startAlpha, 0,progress);
            _text.color = tmp;
            progress += rate * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
