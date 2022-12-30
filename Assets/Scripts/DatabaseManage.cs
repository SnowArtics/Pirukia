using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseManage : MonoBehaviour
{

    void Start()
    {
        DBCreate();
        DBConnectionCheck();
        DatabaseAllRead("SELECT * from build");
    }

    void Awake() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DBCreate() {
        string filepath = string.Empty;                                                     // 파일 경로

        filepath = Application.dataPath + "/StreamingAssets/Building_Database.db";          // 경로 설정
        if (!File.Exists(filepath)) {                                                       // 파일이 없을 때
            File.Copy(Application.streamingAssetsPath + "/StreamingAssets/Building_Database.db", filepath);      // 해당 경로로 파일 복사
        }

        Debug.Log("DB 생성 완료");
    }

    public string GetDBFilePath() {
        string str = string.Empty;

        str = "URI=file:" + Application.dataPath + "/StreamingAssets/Building_Database.db";

        return str;
    }

    public void DBConnectionCheck() {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();

        if(dbConnection.State == ConnectionState.Open) {
            Debug.Log("DB 연결 성공");
        }
        else {
            Debug.Log("연결 실패(에러)");
        }
    }

    public void DatabaseRead(string query) {

    }

    public void DatabaseAllRead(string query) {        // 인자로 쿼리문을 받는다.
        // DB 열기
        IDbConnection dbConn = new SqliteConnection(GetDBFilePath());
        dbConn.Open();
        
        // 쿼리 입력
        IDbCommand dbCommand = dbConn.CreateCommand();
        dbCommand.CommandText = query;

        // 쿼리 실행
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read()) {
            // 0번, 1번, 2번 필드 읽기
            Debug.Log(dataReader.GetInt32(0) + ", " + dataReader.GetString(1) + ", " + dataReader.GetString(2));
        }

        // 생성 순서와 반대로 닫아준다.
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConn.Close();
        dbConn = null;
    }
}
