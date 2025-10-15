using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrewInventoryNode : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI classText;
    public TextMeshProUGUI bonusText;
    public Image crewIcon;
    public CrewMember crewMember;
    public Toggle crewToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCrewText(CrewMember member)
    {
        crewMember = member;
        nameText.text = crewMember.crewName;
        classText.text = crewMember.role.ToString();
        bonusText.text = "";
        foreach (StatBonus bonus in crewMember.statsBonuses)
        {
            if(bonusText.text.Length > 0)
            {
                bonusText.text += "\n";
            }
            string sign = bonus.bonusValue > 0 ? "+" : "-";
            bonusText.text += sign + Mathf.Abs(bonus.bonusValue) + " " + ShipStat.StringForType(bonus.type);
        }

        crewIcon.color = crewMember.color;
        //nusText.text = crew.crewName;
    }

    public void ToggleCrew()
    {
        if(crewToggle.isOn)
        {
            crewToggle.SetIsOnWithoutNotify(crewMember.crew.PassengerToCrew(crewMember));

        }
        else
        {
            crewMember.crew.CrewToPassenger(crewMember);
            crewToggle.SetIsOnWithoutNotify(false);

        }
    }
}
