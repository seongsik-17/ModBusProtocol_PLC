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
		//데이터 삽입
		public static void InsertData()
		{
			using (var conn = new SqliteConnection(connectionString))
			{
				conn.Open();
				string query = "";
				//Todo: 데이터 정보 확인 후 클래스 생성 필요
				//Boolean result = conn.Execute(,query);
				conn.Close();
			}
		}

	}
}
