using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Data.Common;

public class DatabaseManage : MonoBehaviour
{
    private string filepath;
    private IDbConnection DBConnection;
    private IDbCommand DBCommand;
    private IDataReader dataReader;

    void Start()
    {
        DBCreate();
        DatabaseAllRead("SELECT * from build");
        CloseDatabase();
    }

    void Awake() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DBCreate() {
        filepath = string.Empty;
        DBConnection = new SqliteConnection(GetDBFilePath());                 // DB파일을 연결하는 변수

        filepath = Application.dataPath + "/StreamingAssets/Building_Database.db";          // DB파일 경로 설정

        // 파일이 없을 경우, 설정한 경로로 DB 파일을 복사
        if (!File.Exists(filepath)) {
            File.Copy(Application.streamingAssetsPath + "/StreamingAssets/Building_Database.db", filepath);
        }

        // DB 파일을 연결
        DBConnection.Open();
    }

    // DB 파일 경로를 받아오는 함수
    public string GetDBFilePath() {
        string str = string.Empty;

        // DB 파일 경로를 str 변수에 저장
        str = "URI=file:" + Application.dataPath + "/StreamingAssets/Building_Database.db";

        return str;
    }

    public void DatabaseRead(string query) {

    }

    // DB를 전부 읽어들이는 함수
    public void DatabaseAllRead(string query) {        // 인자로 쿼리문을 받는다.        
        // 쿼리 입력
        DBCommand = DBConnection.CreateCommand();
        DBCommand.CommandText = query;

        // 쿼리 실행
        dataReader = DBCommand.ExecuteReader();

        while (dataReader.Read()) {
            // 0번, 1번, 5번 필드 읽기
            Debug.Log(dataReader.GetInt32(0) + ", " + dataReader.GetString(1) + ", " + dataReader.GetInt32(5));
        }
    }

    // 열린 DB를 닫는 함수
    public void CloseDatabase() {
        // 생성 순서와 반대로 닫아준다.
        dataReader.Dispose();
        dataReader = null;
        DBCommand.Dispose();
        DBCommand = null;
        DBConnection.Close();
        DBConnection = null;
    }
}
