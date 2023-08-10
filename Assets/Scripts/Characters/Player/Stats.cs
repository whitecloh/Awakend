using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour // ui статы персонажей
{
    [SerializeField]
    private Text _statValue;
    [SerializeField]
    private float _lerpSpeed;

    private Image _content;

    private float _currentFill=1;
    private float _currentValue;
    private float _overflow;

    public float Overflow
    {
        get
        {
            float tmp = _overflow;
            _overflow = 0;
            return tmp;
        }
    }
    public float MyMaxValue { get; set; }
    public bool IsFull { get { return _content.fillAmount == 1; } }
    public float MyCurrentValue
    {
        get
        {

            return _currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                _overflow = value - MyMaxValue;
                _currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                _currentValue = 0;
            }
            else
            {
                _currentValue = value;
            }
            _currentFill = _currentValue / MyMaxValue;
            if (_statValue != null)
            {
                _statValue.text = _currentValue + " / " + MyMaxValue;
            }
        }
    }

    private void Awake()
    {
        _content = GetComponent<Image>();
    }
    private void Update()
    {
        if(_currentFill!=_content.fillAmount)
        {
            _content.fillAmount = Mathf.MoveTowards(_content.fillAmount, _currentFill, Time.deltaTime*_lerpSpeed);
        }
        _content.fillAmount = _currentFill;
    }

    public void Initialize(float currentVal, float maxVal)
    {
        if (_content == null)
        {
            _content = GetComponent<Image>(); 
        }
            MyMaxValue = maxVal;
        MyCurrentValue = currentVal;
        _content.fillAmount = MyCurrentValue / MyMaxValue;

    }
    public void SetMaxValue(float maxVal)
    {
        if (_content == null)
        {
            _content = GetComponent<Image>();
        }
            MyMaxValue = maxVal;
            MyCurrentValue = _currentValue;
    }
    public void Reset()
    {
        _content.fillAmount = 0;
    }
}
