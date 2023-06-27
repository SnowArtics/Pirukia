using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Data.Common;
using System.Linq.Expressions;
using System;
using Mono.Cecil;

public class DatabaseManage : MonoBehaviour
{
    private string filepath;
    private IDbConnection DBConnection;
    private IDbCommand DBCommand;
    private IDataReader dataReader;
    [SerializeField]
    private Dictionary<int, int> produceResource = new Dictionary<int, int>();

    public int getResource(int id) { return produceResource[id]; }
    public void setResource(int id, int value) { produceResource[id] = value; } 


    private void Awake() {
        setResource(101, 0);
        setResource(102, 0);
    }

    public void DBCreate() {
        filepath = string.Empty;
        DBConnection = new SqliteConnection(GetDBFilePath());                               // DB파일을 연결하는 변수

        filepath = Application.dataPath + "/StreamingAssets/Building_DB.db";                // DB파일 경로 설정

        // 파일이 없을 경우, 설정한 경로로 DB 파일을 복사
        if (!File.Exists(filepath)) {
            File.Copy(Application.streamingAssetsPath + "/StreamingAssets/Building_DB.db", filepath);
        }

        // DB 파일을 연결
        DBConnection.Open();
    }

    // DB 파일 경로를 받아오는 함수
    public string GetDBFilePath() {
        string str = string.Empty;

        // DB 파일 경로를 str 변수에 저장
        str = "URI=file:" + Application.dataPath + "/StreamingAssets/Building_DB.db";

        return str;
    }

    // SQL 쿼리를 받아와 DB 내의 데이터를 한 가지만 반환하는 함수
    public string DBSelectOne(string item, int index) {
        string result;
        string query = "SELECT " + item + " from Building where Id=" + index.ToString();


        dataReader = ExecuteDB(query);                      // 명령문을 실행
        result = dataReader.GetValue(0).ToString();         // 찾아낸 데이터를 string으로 형 변환

        // DB 내에 존재하지 않는 경우(result가 빈칸인 경우)에는 DB 값이 아닌 -로 반환한다.
        if (result == "") { return "-"; }
        return result;
    }

    // 해당 BUILDING_CODE에 해당하는 열 전체를 받아오는 함수
    public IDataReader DBSelectLine(int index) {
        string query = "SELECT * from Building where Id=" + index.ToString();

        dataReader = ExecuteDB(query);                      // 명령문을 실행

        return dataReader;
    }

    // DB를 전부 읽어들이는 함수
    public IDataReader DBSelectAll() {        // 인자로 쿼리문을 받는다.        
        // 쿼리 입력 및 실행
        DBCommand = DBConnection.CreateCommand();
        DBCommand.CommandText = "SELECT * from Building";

        dataReader = DBCommand.ExecuteReader();

        return dataReader;

        //       while (dataReader.Read()) {
        // 실행 영역
        //       }
    }

    public IDataReader ExecuteDB(string query) {
        IDataReader result;

        DBCommand = DBConnection.CreateCommand();           // SQL 명령어 리스트를 불러옴
        DBCommand.CommandText = query;                      // 입력받은 쿼리를 입력
        result = DBCommand.ExecuteReader();                 // SQL 쿼리를 실행

        return result;
    }

    // 특정 건물에 대한 DB 값을 리스트에 정리해서 반환
    public List<string> DBSelectBuilding(string name) {
        List<string> result = new List<string>();

        DBCommand = DBConnection.CreateCommand();
        DBCommand.CommandText = "SELECT * from Building where Name=\"" + name + "\"";
        dataReader = DBCommand.ExecuteReader();

        while(dataReader.Read()) {
            for (int idx = 0; idx < 16; idx++) {
                result.Add((dataReader.GetValue(idx)).ToString());
            }
        }

        return result;
    }

    public List<string> DBSelectResource(string name) {
        List<string> result = new List<string>();

        DBCommand = DBConnection.CreateCommand();
        dataReader = ExecuteDB("SELECT Id from Building where Name=\"" + name + "\"");
        string getId = dataReader.GetValue(0).ToString();

        // Debug.Log(getId);

        DBCommand.CommandText = "SELECT * from Resource where Id=" + getId;
        dataReader = DBCommand.ExecuteReader();

        while(dataReader.Read()) {
            for (int idx = 0; idx < 3; idx++) {
                result.Add((dataReader.GetValue(idx)).ToString());
            }
        }

        // Debug.Log(result[2]);

        return result;
    }

    // 열린 DB를 닫는 함수
    // DB를 열 때 실행한 순서의 반대로 닫아준다.
    public void CloseDB() {
        dataReader.Dispose();
        dataReader = null;
        DBCommand.Dispose();
        DBCommand = null;
        DBConnection.Close();
        DBConnection = null;
    }
}
