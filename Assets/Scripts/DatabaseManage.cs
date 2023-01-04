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
        DBConnection = new SqliteConnection(GetDBFilePath());                 // DB������ �����ϴ� ����

        filepath = Application.dataPath + "/StreamingAssets/Building_Database.db";          // DB���� ��� ����

        // ������ ���� ���, ������ ��η� DB ������ ����
        if (!File.Exists(filepath)) {
            File.Copy(Application.streamingAssetsPath + "/StreamingAssets/Building_Database.db", filepath);
        }

        // DB ������ ����
        DBConnection.Open();
    }

    // DB ���� ��θ� �޾ƿ��� �Լ�
    public string GetDBFilePath() {
        string str = string.Empty;

        // DB ���� ��θ� str ������ ����
        str = "URI=file:" + Application.dataPath + "/StreamingAssets/Building_Database.db";

        return str;
    }

    public void DatabaseRead(string query) {

    }

    // DB�� ���� �о���̴� �Լ�
    public void DatabaseAllRead(string query) {        // ���ڷ� �������� �޴´�.        
        // ���� �Է�
        DBCommand = DBConnection.CreateCommand();
        DBCommand.CommandText = query;

        // ���� ����
        dataReader = DBCommand.ExecuteReader();

        while (dataReader.Read()) {
            // 0��, 1��, 5�� �ʵ� �б�
            Debug.Log(dataReader.GetInt32(0) + ", " + dataReader.GetString(1) + ", " + dataReader.GetInt32(5));
        }
    }

    // ���� DB�� �ݴ� �Լ�
    public void CloseDatabase() {
        // ���� ������ �ݴ�� �ݾ��ش�.
        dataReader.Dispose();
        dataReader = null;
        DBCommand.Dispose();
        DBCommand = null;
        DBConnection.Close();
        DBConnection = null;
    }
}
