using UnityEngine;
using System.Text;
using System.Reflection;
using TMPro;

public class UIStatsDisplay : MonoBehaviour
{
    public PlayerManager player;
    TextMeshProUGUI statNames, statValues;

    private void OnEnable()
    {
        UpdateStatFields();
    }

    public void UpdateStatFields()
    {
        if (!player) return;

        if (!statNames) statNames = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (!statValues) statValues = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        StringBuilder names = new StringBuilder();
        StringBuilder values = new StringBuilder();
        FieldInfo[] fields = typeof(CharacterData.Stats).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            names.AppendLine(field.Name);

            object val = field.GetValue(player.Stats);
            float fval = val is int ? (int)val : (float)val; // Handle both int and float types

            PropertyAttribute attribute = (PropertyAttribute)PropertyAttribute.GetCustomAttribute(field, typeof(PropertyAttribute));
            if (attribute != null && field.FieldType == typeof(float))
            {
                float percentage = Mathf.Round(fval * 100 - 100);

                if (Mathf.Approximately(percentage, 0))
                {
                    values.Append('-').Append('\n');
                }
                else
                {
                    if (percentage > 0)
                    {
                        values.Append('+');
                    }
                    values.Append(percentage).Append('%').Append('\n');

                }
            }
            else
            {
                values.Append(fval).Append('\n');
            }

            statNames.text = names.ToString();
            statValues.text = values.ToString();
        }
    }
}
