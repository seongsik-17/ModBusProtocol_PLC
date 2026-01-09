using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ModBusProtocol_PLC
{
    public static class DbController
    {
        private static string connectionString = @"Data Source=C:\ws_SQLite\testDatabase.sqlite";

        //config 파일 로드
        private static Config config = seongsiksUtils.getConfigData();

        //데이터 삽입
        public static void InsertData(ReceivedDataDto data)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = $"INSERT INTO {config.DbPath} (ip, count, runstop, receivedTimeStamp) VALUES(@ip, @count, @runstop, @recivedTimeStamp)";
                conn.Execute(query, data);
                //Todo: 데이터 정보 확인 후 클래스 생성 필요
                //Boolean result = conn.Execute(,query);
                conn.Close();
            }
        }

        public static List<ReceivedDataDto> SelectData()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM {config.DbPath}";
                var result = conn.Query<ReceivedDataDto>(query);
                List<ReceivedDataDto> dataList = new List<ReceivedDataDto>();
                foreach (var item in result)
                {
                    dataList.Add(item);
                }
                conn.Close();
				return dataList;
			}
        }
    }
}