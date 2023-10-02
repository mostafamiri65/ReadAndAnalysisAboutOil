using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReadAndAnalysis.App.DTOs.News;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Services.Interfaces;
using ReadAndAnalysis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Services.Implementations
{
    public class HomeService : IHomeService
    {
        private TxtPrcContext _context;
        public HomeService(TxtPrcContext context)
        {
            _context = context;
        }

        public async Task<long> GetAllNewsCount()
        {
            var reads = await _context.NewsCounts.SingleAsync(n => n.TableName == "NewsInRsses");
            return reads.Count;
        }

        public async Task<List<TestSpDto>> GetDataFromSp()
        {
            SqlConnection dbConnection = (SqlConnection)_context.Database.GetDbConnection();

            SqlCommand cmd = new SqlCommand("Sp_test", dbConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            //  cmd.CommandText = "exec Sp_test";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<TestSpDto> list = new List<TestSpDto>();
            foreach (DataRow item in dt.Rows)
            {
                var dte = item["dte"].ToString();
                var splited = dte.Split("-");
                var date = splited[0] + "-" + splited[1] + "-" + splited[2].Remove(2);
                var time = splited[2].Remove(0, 3);
                var spDate = Convert.ToDateTime(date + " " + time);
                TestSpDto dto = new TestSpDto()
                {
                    ShownDate = item["dte"].ToString(),
                    OilNewsCount = Convert.ToInt32(item["OilCnr"]),
                    NotOilNewsCount = Convert.ToInt32(item["NotOilCnr"])
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<int> GetEvaluatedNewsByBoos(string? startDate, string? endDate, int? relevanceId)
        {
            var start =startDate.ToMiladi();
            var end = endDate.ToMiladi();
            var evaluated = await _context.EvaluatedResults.Include(e => e.News)
                  .Where(e => e.RelevanceId == relevanceId && e.News.CreateDate >= start &&
                  e.News.CreateDate < end).ToListAsync();
            return evaluated.Count;
        }

        public async Task<int> GetNegativeOilNewsCount(string? startDate, string? endDate)
        {
            var start = startDate.ToMiladi();
            var end = endDate.ToMiladi();
            var evaluated = await _context.EvaluatedResults.Include(e => e.News)
                  .Where(e => e.EstimateId == 3 && e.News.CreateDate >= start &&
                  e.News.CreateDate < end).ToListAsync();
            return evaluated.Count;
        }

        public async Task<int> GetNeutralOilNewsCount(string? startDate, string? endDate)
        {
            var start = startDate.ToMiladi();
            var end = endDate.ToMiladi();
            var evaluated = await _context.EvaluatedResults.Include(e => e.News)
                  .Where(e => e.EstimateId == 1 && e.News.CreateDate >= start &&
                  e.News.CreateDate < end).ToListAsync();
            return evaluated.Count;
        }

        public async Task<List<NewsRelevance>> GetNewsRelevance()
        {
            return await _context.NewsRelevances.ToListAsync();
        }

        public async Task<long> GetOilNewsCount()
        {
            var reads = await _context.NewsCounts.SingleAsync(n => n.TableName == "OurNews");
            return reads.Count;
        }

        public async Task<int> GetPosetiveOilNewsCount(string? startDate, string? endDate)
        {
            var start = startDate.ToMiladi();
            var end = endDate.ToMiladi();
            var evaluated = await _context.EvaluatedResults.Include(e => e.News)
                  .Where(e => e.EstimateId == 2 && e.News.CreateDate >= start &&
                  e.News.CreateDate < end).ToListAsync();
            return evaluated.Count;
        }

        public async Task<int> GetProcessedNewsCount()
        {
            var evaluted = await _context.EvaluatedResults.ToListAsync();
            return evaluted.Count;
        }

        public async Task<string> GetRelevanceTitleById(int relevanceId)
        {
           var relevance = await _context.NewsRelevances.SingleAsync(r=>r.Id==relevanceId);
            return relevance.Title;
        }

        public async Task<int> GetUsersCount()
        {
            var users = await _context.TbUsers.ToListAsync();
            return users.Count;
        }

        public async Task<bool> IsBoss(long userId)
        {
            var userRoles = await _context.TbUserRoles.Where(u => u.UserId == userId)
               .Select(u => u.RoleId).ToListAsync();
            var bossRoles = await _context.TbRoles.Where(r => r.IsBoss).ToListAsync();
            foreach (var boss in bossRoles)
            {
                if (userRoles.Contains(boss.Id))
                    return true;
            }
            return false;
        }

        public async Task<bool> IsSecretory(long userId)
        {
            var userRoles = await _context.TbUserRoles.Where(u => u.UserId == userId)
                .Select(u => u.RoleId).ToListAsync();
            var secrotary = await _context.TbRoles.SingleAsync(r => r.IsSecretary);
            if (userRoles.Contains(secrotary.Id))
                return true;
            return false;
        }
    }
}
