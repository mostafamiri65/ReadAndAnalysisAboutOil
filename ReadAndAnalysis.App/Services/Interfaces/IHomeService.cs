using ReadAndAnalysis.App.DTOs.News;
using ReadAndAnalysis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Services.Interfaces
{
    public interface IHomeService
    {
        Task<long> GetAllNewsCount();
        Task<int> GetUsersCount();
        Task<long> GetOilNewsCount();
        Task<int> GetProcessedNewsCount();

        Task<List<TestSpDto>> GetDataFromSp();
        Task<bool> IsSecretory(long userId);
        Task<bool> IsBoss(long userId);
        Task<int> GetNegativeOilNewsCount(string? startDate,string? endDate);
        Task<int> GetPosetiveOilNewsCount(string? startDate, string? endDate);
        Task<int> GetNeutralOilNewsCount(string? startDate, string? endDate);
        Task<int> GetEvaluatedNewsByBoos(string? startDate, string? endDate, int? relevanceId);
        Task<List<NewsRelevance>> GetNewsRelevance();
        Task<string> GetRelevanceTitleById(int relevanceId);
    }
}
