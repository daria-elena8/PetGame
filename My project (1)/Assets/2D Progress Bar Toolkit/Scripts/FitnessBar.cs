
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitnessBar : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color m_MainColor = Color.white;
    [SerializeField] private Color m_FillColor = Color.green;

    [Header("General")]
    [SerializeField] private int m_NumberOfSegments = 1;
    [SerializeField] private float m_SizeOfNotch = 20;
    [Range(0, 1f)][SerializeField] private float m_FillAmount = 1f;

    [Header("Happiness Settings")]
    [SerializeField] private float increaseAmount = 0.1f; // Valoarea cu care crește happiness
    [SerializeField] private float decayRate = 0.01f; // Rata de scădere a happiness pe secundă

    private RectTransform m_RectTransform;
    private Image m_Image;
    private List<Image> m_ProgressToFill = new List<Image>();
    private float m_SizeOfSegment;

    public void Awake()
    {
        // get rect transform
        m_RectTransform = GetComponent<RectTransform>();

        // get image
        m_Image = GetComponentInChildren<Image>();
        m_Image.color = m_MainColor;
        m_Image.gameObject.SetActive(false);

        // count size of segments
        m_SizeOfSegment = m_RectTransform.sizeDelta.x / m_NumberOfSegments;
        for (int i = 0; i < m_NumberOfSegments; i++)
        {
            GameObject currentSegment = Instantiate(m_Image.gameObject, transform.position, Quaternion.identity, transform);
            currentSegment.SetActive(true);

            Image segmentImage = currentSegment.GetComponent<Image>();
            segmentImage.fillAmount = m_SizeOfSegment;

            RectTransform segmentRectTransform = segmentImage.GetComponent<RectTransform>();
            segmentRectTransform.sizeDelta = new Vector2(m_SizeOfSegment, segmentRectTransform.sizeDelta.y);
            segmentRectTransform.position += (Vector3.right * i * m_SizeOfSegment) - (Vector3.right * m_SizeOfSegment * (m_NumberOfSegments / 2)) + (Vector3.right * i * m_SizeOfNotch);

            Image segmentFillImage = segmentImage.transform.GetChild(0).GetComponent<Image>();
            segmentFillImage.color = m_FillColor;
            m_ProgressToFill.Add(segmentFillImage);
            segmentFillImage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(m_SizeOfSegment, segmentFillImage.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public void Update()
    {
        // Scăderea treptată a valorii happiness
        m_FillAmount -= decayRate * Time.deltaTime;
        m_FillAmount = Mathf.Clamp(m_FillAmount, 0, 1f);

        for (int i = 0; i < m_NumberOfSegments; i++)
        {
            m_ProgressToFill[i].fillAmount = Mathf.Clamp01(m_NumberOfSegments * m_FillAmount - i);
        }
    }

    public void IncreaseFitness()
    {
        m_FillAmount += increaseAmount;
        m_FillAmount = Mathf.Clamp(m_FillAmount, 0, 1f);

    }
    public void MaxFitness() { m_FillAmount = 1f; }

    // Funcția care va fi apelată de la Animator
    public void OnAnimationTrigger()
    {
        IncreaseFitness();
    }

    private float ConvertFragmentToWidth(float fragment)
    {
        return m_RectTransform.sizeDelta.x * fragment;
    }
}
