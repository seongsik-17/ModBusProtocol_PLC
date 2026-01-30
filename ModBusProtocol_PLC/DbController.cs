using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ModBusProtocol_PLC
{
    public static class DbController
    {
        private static string connectionString = @"Data Source=C:\ws_SQLite\testDatabase.sqlite";

        //config 파일 로드
        private static Config config = seongsiksUtils.getConfigData();
        //오류가 발생하는 내용에 따라서 코드를 만들어 볼 예정

        //오류 발생 로그
        public static void WriteErrorLog(ErrorLogDto errorLog)
        {
			using (var conn = new SqliteConnection(connectionString))
			{
				try
				{
					conn.Open();
					string query = $@"INSERT INTO [{config.ErrorDB}] (logTime, errorMsg, errorCode) VALUES(@LogTime, @ErrorMsg, @ErrorCode)";
					conn.Execute(query, errorLog);
					//Todo: 데이터 정보 확인 후 클래스 생성 필요
					//Boolean result = conn.Execute(,query);
				}
				catch (Exception ex)
				{
					throw new Exception("데이터 삽입 중 오류가 발생했습니다. " + ex.Message);
				}
				finally
				{
					conn.Close();
				}
			}
		}

        //데이터 삽입
        public static void InsertData(ReceivedDataDto data)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if(SelectOne(data.ip) != null)
                    {
                        UpdateData(data);
                        return;
                    }
                    string query = $@"INSERT INTO [{config.DbPath}] (ip, count, runstop, receivedTimeStamp) VALUES(@ip, @count, @runstop, @receivedTimeStamp)";
                    conn.Execute(query, data);
                    //Todo: 데이터 정보 확인 후 클래스 생성 필요
                    //Boolean result = conn.Execute(,query);
                }
                catch (Exception ex)
                {
                    throw new Exception("데이터 삽입 중 오류가 발생했습니다. " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //데이터 업데이트
        public static void UpdateData(ReceivedDataDto data)
        {
            //MessageBox.Show("UpdateData의 데이터: " + data.count + data.ip + data.runstop);
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = $@"UPDATE  [{config.DbPath}] SET count = @count, runstop = @runstop, receivedTimeStamp = @receivedTimeStamp WHERE ip = @ip";
                try
                {
                    conn.Execute(query, data);
                }
                catch
                {
                    throw new Exception("데이터 업데이트 중 오류가 발생했습니다.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //데이터 전체 조회
        public static List<ReceivedDataDto> SelectData()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM [{config.DbPath}]";
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

        //데이터 개별 조회
        public static ReceivedDataDto SelectOne(string ip)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM [{config.DbPath}] WHERE ip = @ip LIMIT 1";
                var result = conn.QueryFirstOrDefault<ReceivedDataDto>(query, new { ip });
                conn.Close();
                return result;
            }
        }
    }
}