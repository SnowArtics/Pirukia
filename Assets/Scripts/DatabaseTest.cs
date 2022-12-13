using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);

        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/Building_Database.db";

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT BUILDING_CODE, BUILDING_NAME FROM build";
        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            int buidlingCode = reader.GetInt32(0);
            string buildingName = reader.GetString(1);

            Debug.Log("Load buidlingCode =   " + buidlingCode);
            Debug.Log("Load buildingName  =   " + buildingName);


        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
