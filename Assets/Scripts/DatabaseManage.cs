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
        DBConnection = new SqliteConnection(GetDBFilePath());                               // DB������ �����ϴ� ����

        filepath = Application.dataPath + "/StreamingAssets/Building_DB.db";                // DB���� ��� ����

        // ������ ���� ���, ������ ��η� DB ������ ����
        if (!File.Exists(filepath)) {
            File.Copy(Application.streamingAssetsPath + "/StreamingAssets/Building_DB.db", filepath);
        }

        // DB ������ ����
        DBConnection.Open();
    }

    // DB ���� ��θ� �޾ƿ��� �Լ�
    public string GetDBFilePath() {
        string str = string.Empty;

        // DB ���� ��θ� str ������ ����
        str = "URI=file:" + Application.dataPath + "/StreamingAssets/Building_DB.db";

        return str;
    }

    // SQL ������ �޾ƿ� DB ���� �����͸� �� ������ ��ȯ�ϴ� �Լ�
    public string DBSelectOne(string item, int index) {
        string result;
        string query = "SELECT " + item + " from Building where Id=" + index.ToString();


        dataReader = ExecuteDB(query);                      // ��ɹ��� ����
        result = dataReader.GetValue(0).ToString();         // ã�Ƴ� �����͸� string���� �� ��ȯ

        // DB ���� �������� �ʴ� ���(result�� ��ĭ�� ���)���� DB ���� �ƴ� -�� ��ȯ�Ѵ�.
        if (result == "") { return "-"; }
        return result;
    }

    // �ش� BUILDING_CODE�� �ش��ϴ� �� ��ü�� �޾ƿ��� �Լ�
    public IDataReader DBSelectLine(int index) {
        string query = "SELECT * from Building where Id=" + index.ToString();

        dataReader = ExecuteDB(query);                      // ��ɹ��� ����

        return dataReader;
    }

    // DB�� ���� �о���̴� �Լ�
    public IDataReader DBSelectAll() {        // ���ڷ� �������� �޴´�.        
        // ���� �Է� �� ����
        DBCommand = DBConnection.CreateCommand();
        DBCommand.CommandText = "SELECT * from Building";

        dataReader = DBCommand.ExecuteReader();

        return dataReader;

        //       while (dataReader.Read()) {
        // ���� ����
        //       }
    }

    public IDataReader ExecuteDB(string query) {
        IDataReader result;

        DBCommand = DBConnection.CreateCommand();           // SQL ��ɾ� ����Ʈ�� �ҷ���
        DBCommand.CommandText = query;                      // �Է¹��� ������ �Է�
        result = DBCommand.ExecuteReader();                 // SQL ������ ����

        return result;
    }

    // Ư�� �ǹ��� ���� DB ���� ����Ʈ�� �����ؼ� ��ȯ
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
}
