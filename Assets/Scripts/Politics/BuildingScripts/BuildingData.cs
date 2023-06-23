using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/Building")]
public class BuildingData : ScriptableObject
{
    [SerializeField] // 건물 고유 번호
    private int id;
    public int Id { get { return id; } set { id = value; } }
    [SerializeField] // 건물 이름(영문)
    private string name;
    public string Name { get { return name; } set { name = value; } }
    [SerializeField] // 견물 이름(국문)
    private string nameKr;
    public string NameKr { get { return nameKr; } set { nameKr = value; } }
    [SerializeField] // 건물 크기(n*m 형식)
    private string volume;
    public string Volume { get { return volume; } set { volume = value; } }
    [SerializeField] // 건설에 필요한 자원 종류
    private List<int> buildCtResource = new List<int>();
    public List<int> BuildCtResource { get { return buildCtResource; } set { buildCtResource = value; } }
    [SerializeField] // 건설에 필요한 자원의 양
    private List<int> buildCtResourceAmount = new List<int>();
    public List<int> BuildCtResourceAmount { get { return buildCtResourceAmount; } set { buildCtResourceAmount = value; } }
    [SerializeField] // 건물 건설 소요 시간
    private int buildCtTime;
    public int BuildCtTime { get { return buildCtTime; } set { buildCtTime = value; } }
    [SerializeField] // 건물 생성 자원 종류
    private List<int> productionResource = new List<int>();
    public List<int> ProductionResource { get { return productionResource; } set { productionResource = value; } }
    [SerializeField]// 배치 신도 1인당 자원 생산량
    private List<int> produtionOutputPerBeliever = new List<int>();
    public List<int> ProductionOutputPerBeliever { get { return produtionOutputPerBeliever; } set { produtionOutputPerBeliever = value; } }
    [SerializeField] // 최대 배치 가능 신도 수
    private int limitHuman;
    public int LimitHuman { get { return limitHuman; } set { limitHuman = value; } }
    [SerializeField] // 건설 중 애니메이션
    private string constructionAnim;
    public string ConstrutionAnim { get { return constructionAnim; } set { constructionAnim = value; } }
    [SerializeField] // 완성된 건물 이미지
    private string buildImage;
    public string BuildImage { get { return buildImage; } set { buildImage = value; } }
    [SerializeField] // 건물 개수 제한
    private int limitBuilding;
    public int LimitBuilding { get { return limitBuilding; } set { limitBuilding = value; } }
    [SerializeField] // 자원 생산에 필요한 자원 종류
    private List<int> consumeResource = new List<int>();
    public List<int> ConsumeResource { get { return consumeResource; } set { consumeResource = value; } }
    [SerializeField] // 자원 생산에 필요한 자원의 양
    private List<int> consumeResourceAmount = new List<int>();
    public List<int> ConsumeResourceAmount { get { return consumeResourceAmount; } set { consumeResourceAmount = value; } }
    [SerializeField] // 자원 생산에 걸리는 시간(초 단위)
    private int productionTime; 
    public int ProductionTime { get { return productionTime; } set { productionTime = value; } }


}


