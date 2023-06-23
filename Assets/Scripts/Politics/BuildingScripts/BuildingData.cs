using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/Building")]
public class BuildingData : ScriptableObject
{
    [SerializeField] // �ǹ� ���� ��ȣ
    private int id;
    public int Id { get { return id; } set { id = value; } }
    [SerializeField] // �ǹ� �̸�(����)
    private string name;
    public string Name { get { return name; } set { name = value; } }
    [SerializeField] // �߹� �̸�(����)
    private string nameKr;
    public string NameKr { get { return nameKr; } set { nameKr = value; } }
    [SerializeField] // �ǹ� ũ��(n*m ����)
    private string volume;
    public string Volume { get { return volume; } set { volume = value; } }
    [SerializeField] // �Ǽ��� �ʿ��� �ڿ� ����
    private List<int> buildCtResource = new List<int>();
    public List<int> BuildCtResource { get { return buildCtResource; } set { buildCtResource = value; } }
    [SerializeField] // �Ǽ��� �ʿ��� �ڿ��� ��
    private List<int> buildCtResourceAmount = new List<int>();
    public List<int> BuildCtResourceAmount { get { return buildCtResourceAmount; } set { buildCtResourceAmount = value; } }
    [SerializeField] // �ǹ� �Ǽ� �ҿ� �ð�
    private int buildCtTime;
    public int BuildCtTime { get { return buildCtTime; } set { buildCtTime = value; } }
    [SerializeField] // �ǹ� ���� �ڿ� ����
    private List<int> productionResource = new List<int>();
    public List<int> ProductionResource { get { return productionResource; } set { productionResource = value; } }
    [SerializeField]// ��ġ �ŵ� 1�δ� �ڿ� ���귮
    private List<int> produtionOutputPerBeliever = new List<int>();
    public List<int> ProductionOutputPerBeliever { get { return produtionOutputPerBeliever; } set { produtionOutputPerBeliever = value; } }
    [SerializeField] // �ִ� ��ġ ���� �ŵ� ��
    private int limitHuman;
    public int LimitHuman { get { return limitHuman; } set { limitHuman = value; } }
    [SerializeField] // �Ǽ� �� �ִϸ��̼�
    private string constructionAnim;
    public string ConstrutionAnim { get { return constructionAnim; } set { constructionAnim = value; } }
    [SerializeField] // �ϼ��� �ǹ� �̹���
    private string buildImage;
    public string BuildImage { get { return buildImage; } set { buildImage = value; } }
    [SerializeField] // �ǹ� ���� ����
    private int limitBuilding;
    public int LimitBuilding { get { return limitBuilding; } set { limitBuilding = value; } }
    [SerializeField] // �ڿ� ���꿡 �ʿ��� �ڿ� ����
    private List<int> consumeResource = new List<int>();
    public List<int> ConsumeResource { get { return consumeResource; } set { consumeResource = value; } }
    [SerializeField] // �ڿ� ���꿡 �ʿ��� �ڿ��� ��
    private List<int> consumeResourceAmount = new List<int>();
    public List<int> ConsumeResourceAmount { get { return consumeResourceAmount; } set { consumeResourceAmount = value; } }
    [SerializeField] // �ڿ� ���꿡 �ɸ��� �ð�(�� ����)
    private int productionTime; 
    public int ProductionTime { get { return productionTime; } set { productionTime = value; } }


}


