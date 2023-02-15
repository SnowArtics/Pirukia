using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Data.Common;
using System.Linq.Expressions;

public class DatabaseManage : MonoBehaviour
{
    private string filepath;
    private IDbConnection DBConnection;
    private IDbCommand DBCommand;
    private IDataReader dataReader;

    public void DBCreate() {
        filepath = string.Empty;
        DBConnection = new SqliteConnection(GetDBFilePath());                               // DB������ �����ϴ� ����

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

    // SQL ������ �޾ƿ� DB ���� �����͸� �� ������ ��ȯ�ϴ� �Լ�
    public string DBSelectOne(string item, int index) {
        string result;
        string query = "SELECT " + item + " from build where BUILDING_CODE=" + index.ToString();


        dataReader = ExecuteDB(query);                      // ��ɹ��� ����
        result = dataReader.GetValue(0).ToString();         // ã�Ƴ� �����͸� string���� �� ��ȯ

        // DB ���� �������� �ʴ� ���(result�� ��ĭ�� ���)���� DB ���� �ƴ� -�� ��ȯ�Ѵ�.
        if (result == "") { return "-"; }
        return result;
    }

    // �ش� BUILDING_CODE�� �ش��ϴ� �� ��ü�� �޾ƿ��� �Լ�
    public IDataReader DBSelectLine(int index) {
        string query = "SELECT * from build where BUILDING_CODE=" + index.ToString();

        dataReader = ExecuteDB(query);                      // ��ɹ��� ����

        return dataReader;
    }

    // DB�� ���� �о���̴� �Լ�
    public IDataReader DBSelectAll() {        // ���ڷ� �������� �޴´�.        
        // ���� �Է� �� ����
        DBCommand = DBConnection.CreateCommand();
        DBCommand.CommandText = "SELECT * from build";

        dataReader = DBCommand.ExecuteReader();

        return dataReader;

        //       while (dataReader.Read()) {
        // ���� ����
        //       }
    }

    // ���� DB�� �ݴ� �Լ�
    // DB�� �� �� ������ ������ �ݴ�� �ݾ��ش�.
    public void CloseDB() {
        dataReader.Dispose();
        dataReader = null;
        DBCommand.Dispose();
        DBCommand = null;
        DBConnection.Close();
        DBConnection = null;
    }

    public IDataReader ExecuteDB(string query) {
        IDataReader result;

        DBCommand = DBConnection.CreateCommand();           // SQL ��ɾ� ����Ʈ�� �ҷ���
        DBCommand.CommandText = query;                      // �Է¹��� ������ �Է�
        result = DBCommand.ExecuteReader();                 // SQL ������ ����

        return result;
    }
}
