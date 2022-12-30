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
        string filepath = string.Empty;                                                     // ���� ���

        filepath = Application.dataPath + "/StreamingAssets/Building_Database.db";          // ��� ����
        if (!File.Exists(filepath)) {                                                       // ������ ���� ��
            File.Copy(Application.streamingAssetsPath + "/StreamingAssets/Building_Database.db", filepath);      // �ش� ��η� ���� ����
        }

        Debug.Log("DB ���� �Ϸ�");
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
            Debug.Log("DB ���� ����");
        }
        else {
            Debug.Log("���� ����(����)");
        }
    }

    public void DatabaseRead(string query) {

    }

    public void DatabaseAllRead(string query) {        // ���ڷ� �������� �޴´�.
        // DB ����
        IDbConnection dbConn = new SqliteConnection(GetDBFilePath());
        dbConn.Open();
        
        // ���� �Է�
        IDbCommand dbCommand = dbConn.CreateCommand();
        dbCommand.CommandText = query;

        // ���� ����
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read()) {
            // 0��, 1��, 2�� �ʵ� �б�
            Debug.Log(dataReader.GetInt32(0) + ", " + dataReader.GetString(1) + ", " + dataReader.GetString(2));
        }

        // ���� ������ �ݴ�� �ݾ��ش�.
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConn.Close();
        dbConn = null;
    }
}
