using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class SkillStatements : MonoBehaviour {
    public SkillText[] skillTexts;
    public class SkillText {
        public string name;
        public string statement;
        public string consume;
        public SkillText(string name,string statement,string consume)
        {
            this.name = name;
            this.statement = statement;
            this.consume = consume;
        }
    }

	// Use this for initialization
	void Start () {
        int skillnum = GetComponent<EquipmentTable>().equipmentNameList.Length;
        skillTexts = new SkillText[skillnum];

        string url = Application.dataPath + "/SkillStatement.xml";
        XmlDocument XmlDoc = new XmlDocument();
        XmlDoc.Load(url);
        XmlNodeList XMllist = XmlDoc.GetElementsByTagName("skill");
        foreach(XmlNode node in XMllist)
        {
            string name = node.Attributes["name"].Value;
            string statement = node.InnerText;
            string consume = node.Attributes["cm"].Value;
            int index = int.Parse(node.Attributes["no"].Value);
            skillTexts[index]=new SkillText(name, statement,consume);
        }

        Debug.Log("在XML中有" + XmlDoc.GetElementsByTagName("skill").Count+"个单位");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
